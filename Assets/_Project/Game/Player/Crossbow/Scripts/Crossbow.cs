using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Crossbow : MonoBehaviour
{
    // Parameter
    [Header("Reference")]
    private ActionBasedController actionBasedRef = null;

    [Header("Assets")]
    public GameObject arrowPrefab;
    public GameObject pinPrefab;

    [Header("Crossbow")]
    public Transform m_Socket = null;
    public Transform m_PinSocket = null;
    private Arrow m_CurrentArrow = null;
    private bool isCreatingArrow = false;
    private Rigidbody m_Rigidbody = null;
    [SerializeField] private float defaultPullValue = 2f;
    private AudioSource crossbowSound;
    private Animator m_Animator;
    private GameObject m_Pin;
    
    public float creationArrowTime = 0.5f;
    public int defaultDamage = 20;

    [SerializeField] XRGrabInteractable grabInteractable;

    private void Awake() {
        m_Rigidbody = transform.GetComponent<Rigidbody>();
        crossbowSound = transform.GetComponent<AudioSource>();
        m_Animator = transform.GetComponent<Animator>();
        m_Pin = Instantiate(pinPrefab, m_PinSocket.transform.position, m_PinSocket.transform.rotation);
        m_Pin.transform.parent = m_PinSocket;
    }

    private void Update() {
        m_Pin.transform.position = new Vector3(m_PinSocket.transform.position.x, m_PinSocket.transform.position.y + 0.15f, m_PinSocket.transform.position.z);
    
          //Rotation for objects
          if(actionBasedRef != null) {
            float shootValue = actionBasedRef.activateActionValue.action.ReadValue<float>();
            if(shootValue > 0f) {
                FireArrow();
            }
          }
    }

     private void OnEnable() {
        grabInteractable.selectEntered.AddListener(onGrabCrossbow);
        grabInteractable.selectExited.AddListener(onUnGrabCrossbow);
        //grabInteractable.activated.AddListener(FireArrow);
    }
    private void OnDisable() {
        grabInteractable.selectEntered.RemoveListener(onGrabCrossbow);
        grabInteractable.selectExited.RemoveListener(onUnGrabCrossbow);
        //grabInteractable.activated.RemoveListener(FireArrow);
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
        m_CurrentArrow.setDamage(defaultDamage);
        isCreatingArrow = false;
    }

    public void FireArrow() {
        //If arrow is already created
        if(m_CurrentArrow != null) {
            //Animations
            if(m_Animator != null)
                m_Animator.SetTrigger("TrShoot");

            //Sounds
            if(crossbowSound != null)
                crossbowSound.Play();

            //Actions
            m_CurrentArrow.Fire(defaultPullValue);
            m_CurrentArrow = null;
            NewArrow();
        }
        //Create a new arrow
        else if(!m_CurrentArrow && !isCreatingArrow) {
            StartCoroutine(CreateArrow(creationArrowTime));
            isCreatingArrow = true;
        }
    }

    public void onGrabCrossbow(SelectEnterEventArgs args) {
        ActionBasedController controller;
        args.interactorObject.transform.parent.transform.TryGetComponent<ActionBasedController>(out controller);
        if(controller != null)
            actionBasedRef = controller;

        NewArrow();

        if(m_Pin != null)
            m_Pin.SetActive(false);
    }
    public void onUnGrabCrossbow(SelectExitEventArgs args) {
        actionBasedRef = null;
        if(m_Pin != null)
            m_Pin.SetActive(true);
    }
}
