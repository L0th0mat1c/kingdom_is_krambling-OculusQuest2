using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed = 2000f;

    private Rigidbody m_Rigidbody = null;
    private bool isStopped = true;

    private void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(isStopped)
            return;

        //Rotate
        m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_Rigidbody.velocity, transform.up));
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Weapon")
            return;

        Stop();
    }

    private void Stop() {
        isStopped = true;

        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
    }

    public void Fire(float pullValue) {
        isStopped = false;
        transform.parent = null;

        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Rigidbody.AddForce(transform.forward * (pullValue * arrowSpeed));

        StartCoroutine(ActivateCollider(0.1f));
        Destroy(gameObject, 10f);
    }

    private IEnumerator ActivateCollider(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        MeshCollider mesh = transform.GetComponent<MeshCollider>();
        mesh.enabled = true;
    }
}