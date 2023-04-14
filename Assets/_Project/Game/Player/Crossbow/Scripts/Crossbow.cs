using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Crossbow : MonoBehaviour
{
    [Header("Assets")]
    public GameObject arrowPrefab;

    [Header("Crossbow")]
    public Transform m_Socket = null;
    private Arrow m_CurrentArrow = null;
    private bool isCreatingArrow = false;
    private Rigidbody m_Rigidbody = null;
    [SerializeField] private float defaultPullValue = 2f;
    private AudioSource crossbowSound;
    private Animator m_Animator;
    
    public float creationArrowTime = 0.5f;

    [SerializeField] XRGrabInteractable grabInteractable;

    private void Awake() {
        m_Rigidbody = transform.GetComponent<Rigidbody>();
        crossbowSound = transform.GetComponent<AudioSource>();
        m_Animator = transform.GetComponent<Animator>();
    }

    public void NewArrow() {
        if(!m_CurrentArrow && !isCreatingArrow) {
            StartCoroutine(CreateArrow(creationArrowTime));
            isCreatingArrow = true;
        }
    }

    private IEnumerator CreateArrow(float waitTime) {
        yield return new WaitForSeconds(waitTime);

        //Create
        GameObject arrowObject = Instantiate(arrowPrefab, m_Socket);
        
        //Rigid
        Rigidbody arrowRigid = arrowObject.GetComponent<Rigidbody>();
        arrowRigid.isKinematic = true;
        arrowRigid.useGravity = false;

        //Orient
        arrowObject.transform.localPosition = Vector3.zero;
        arrowObject.transform.localEulerAngles = Vector3.zero;

        //Set
        m_CurrentArrow = arrowObject.GetComponent<Arrow>();
        isCreatingArrow = false;
    }

    public void FireArrow() {
        if(m_CurrentArrow != null) {
            if(m_Animator != null)
                m_Animator.SetTrigger("TrShoot");
            if(crossbowSound != null)
                crossbowSound.Play();
            m_CurrentArrow.Fire(defaultPullValue);
            m_CurrentArrow = null;
            NewArrow();
        }
        else if(!m_CurrentArrow && !isCreatingArrow) {
            StartCoroutine(CreateArrow(creationArrowTime));
            isCreatingArrow = true;
        }
    }

    // A VERIFIER POUR IMPULSION MANETTE
    // private void OnEnable() {
    //     grabInteractable.activated.AddListener(hapticCrossbow);
    // }
    // private void OnDisable() {
    //     grabInteractable.activated.RemoveListener(hapticCrossbow);
    // }
    // private void hapticCrossbow(ActivateEventArgs args) {
    //     args.interactorObject.transform.GetComponent<XRBaseController>().SendHapticImpulse(.5f, .25f);
    // }
}
