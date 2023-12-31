using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 2000f;
    public int damage {get; private set;}

    private Rigidbody m_Rigidbody = null;
    private bool isStopped = true;
    private bool alreadyHit = false;

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(isStopped)
            return;

        //Rotate
        if(m_Rigidbody.velocity != Vector3.zero)
            m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));
    }

    public void setDamage(int d) {this.damage = d;}
    
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Weapon")
            return;

        UnitController enemyController;
        if(alreadyHit == false && other.collider.tag == "EnemyUnit" && other.gameObject.TryGetComponent(out enemyController)) {
            alreadyHit = true;
            //On randomise les dégâts de plus ou moins 20% (à mettre ailleurs)
            int isCritAttack = Random.Range(0, 100);
            int randomDamage = Random.Range(Mathf.RoundToInt(this.damage - (this.damage*0.25f)), Mathf.RoundToInt(this.damage + this.damage*0.25f));
            if(isCritAttack >= 92)
                randomDamage = Mathf.RoundToInt(randomDamage*2);
                
            //On applique les dégâts
            enemyController.ReceiveDamage(randomDamage);

            //On destroy la flèche
            Destroy(gameObject);
            return;
        }

        //On destroy la flèche et on l'arrête
        Stop();
        Destroy(gameObject, 1f);
    }

    private void Stop() {
        isStopped = true;

        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;

        MeshCollider mesh = transform.GetComponent<MeshCollider>();
        mesh.enabled = false;
    }

    public void Fire(float pullValue) {
        isStopped = false;
        transform.parent = null;

        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * (pullValue * arrowSpeed));

        StartCoroutine(ActivateCollider(0.001f));
        Destroy(gameObject, 20f);
    }

    private IEnumerator ActivateCollider(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        MeshCollider mesh = transform.GetComponent<MeshCollider>();
        mesh.enabled = true;
    }
}
