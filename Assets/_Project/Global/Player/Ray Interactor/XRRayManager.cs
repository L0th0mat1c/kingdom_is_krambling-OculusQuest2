using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class XRRayManager : MonoBehaviour
{
    // Parameter
    [Header("Reference")]
    public InputActionReference turnReference = null;

    // Parameter
    [Header("Interactors")]
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    [Header("Visual")]
    public Material reticleShader;

    [Header("Params")]
    public int RotationSpeed = 500;
    public bool isInPlayableZone {get; private set;}

    // Mode
    enum rayMode {
        NormalMode,
        GrabMode
    };
    private rayMode rayInteractorMode;

    //Grab informations
    public GameObject currentGrabbedObject {get; private set;}
    public GameObject currentReticleUnit {get; private set;}

    //Instance Singleton
    private static XRRayManager instance = null;
    public static XRRayManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
    }

    private void Update() {
        if(rayInteractorMode == rayMode.GrabMode && currentGrabbedObject != null && currentGrabbedObject.transform.tag == "Card") {
            RaycastHit hit;

            //Crée le reticle si il n'existe pas encore
            UICard card = currentGrabbedObject.GetComponent<UICard>(); 
            if(currentReticleUnit == null && card != null && card.unit != null) {
                currentReticleUnit = Instantiate(card.unit.transform.GetChild(0).gameObject);
                //On applique le shader visual reticle
                currentReticleUnit.GetComponent<MeshRenderer>().material = reticleShader;
                //On désactive tout les colliders
                foreach(Collider col in currentReticleUnit.GetComponents<Collider>()) {
                    col.enabled = false;
                }
                //Pour chaque enfant de l'objet
                foreach(Transform child in currentReticleUnit.transform) {
                    child.GetComponent<MeshRenderer>().material = reticleShader;
                    foreach(Collider col in child.GetComponents<Collider>()) {
                        col.enabled = false;
                    }
                }
            }

            // Main gauche
            if(leftRayInteractor.lineType == XRRayInteractor.LineType.ProjectileCurve) {
                leftRayInteractor.TryGetCurrent3DRaycastHit(out hit);

                //Update de la position du reticle
                leftRayInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);
                currentReticleUnit.transform.position = position;

                //Rotation for objects
                Vector2 rot = turnReference.action.ReadValue<Vector2>();
                currentReticleUnit.transform.Rotate(0, rot.x * RotationSpeed, 0, Space.World);
                
                if(hit.transform.tag == "Terrain" && card.isPlayableCard()) {
                    isInPlayableZone = true;
                    leftRayInteractor.GetComponent<XRInteractorLineVisual>().invalidColorGradient = ColorManager.Instance.validGradient;
                    updateReticleAndChildVisual(true);
                } else {
                    isInPlayableZone = false;
                    leftRayInteractor.GetComponent<XRInteractorLineVisual>().invalidColorGradient = ColorManager.Instance.invalidGradient;
                    updateReticleAndChildVisual(false);
                }
            }

            //Main droite
            if(rightRayInteractor.lineType == XRRayInteractor.LineType.ProjectileCurve) {
                rightRayInteractor.TryGetCurrent3DRaycastHit(out hit);

                //Update de la position du reticle
                rightRayInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);
                currentReticleUnit.transform.position = position;

                //Rotation for objects
                Vector2 rot = turnReference.action.ReadValue<Vector2>();
                currentReticleUnit.transform.Rotate(0, rot.x * RotationSpeed, 0, Space.World);

                if(hit.transform.tag == "Terrain" && card.isPlayableCard()) {
                    isInPlayableZone = true;
                    rightRayInteractor.GetComponent<XRInteractorLineVisual>().invalidColorGradient = ColorManager.Instance.validGradient;
                    updateReticleAndChildVisual(true);
                } else {
                    isInPlayableZone = false;
                    rightRayInteractor.GetComponent<XRInteractorLineVisual>().invalidColorGradient = ColorManager.Instance.invalidGradient;
                    updateReticleAndChildVisual(false);
                }
            }
        } 
        //On supprime le reticle en quittant le mode
        else if(currentReticleUnit != null) {
            isInPlayableZone = false;
            Destroy(currentReticleUnit);
            currentReticleUnit = null;
        }
    }

    // Création des listeners sur les Ray Interactors
    private void OnEnable() {
        leftRayInteractor.selectEntered.AddListener(objectGrabbed);
        leftRayInteractor.selectExited.AddListener(resetRay);
        rightRayInteractor.selectEntered.AddListener(objectGrabbed);
        rightRayInteractor.selectExited.AddListener(resetRay);
    }
    // Désactivation des listeners sur les Ray Interactors
    private void OnDisable() {
        leftRayInteractor.selectEntered.RemoveListener(objectGrabbed);
        leftRayInteractor.selectExited.RemoveListener(resetRay);
        rightRayInteractor.selectEntered.RemoveListener(objectGrabbed);
        rightRayInteractor.selectExited.RemoveListener(resetRay);
    }

    public void updateReticleAndChildVisual(bool isValid) {
        if(currentReticleUnit != null) {
            if(isValid) {
                ColorManager.changeShaderColorForObjectAndChild(currentReticleUnit, Color.cyan);
            } else {
                ColorManager.changeShaderColorForObjectAndChild(currentReticleUnit, Color.red);
            }
        }
    }

    // Change le mode du Ray Interactor quand on intéragi avec un objet
    private void objectGrabbed(SelectEnterEventArgs args) {
        // Si on sélectionne une carte
        if(args.interactableObject.transform.tag == "Card") {
            //On passe en mode Grab
            rayInteractorMode = rayMode.GrabMode;
            currentGrabbedObject = args.interactableObject.transform.gameObject;

            // On set le raycast en Projectile Mode
            XRRayInteractor xrRay = args.interactorObject.transform.GetComponent<XRRayInteractor>();
            xrRay.lineType = XRRayInteractor.LineType.ProjectileCurve;
            xrRay.additionalFlightTime = 5f;
            xrRay.velocity = 20f;
            xrRay.sampleFrequency = 100;
            xrRay.enableUIInteraction = false;
            
            // Visuel de la ligne du raycast
            XRInteractorLineVisual lineVisual = args.interactorObject.transform.GetComponent<XRInteractorLineVisual>();
            lineVisual.stopLineAtSelection = false;
            lineVisual.lineLength = 100;
        }
    }

    // Reset les valeurs par défaut du Ray Interactor
    private void resetRay(SelectExitEventArgs args) {
        //On passe en mode Normal
        rayInteractorMode = rayMode.NormalMode;
        currentGrabbedObject = null;

        // On remet le Raycast en ligne droite
        XRRayInteractor xrRay = args.interactorObject.transform.GetComponent<XRRayInteractor>();
        xrRay.lineType = XRRayInteractor.LineType.StraightLine;
        xrRay.enableUIInteraction = true;

        // Visuel de la ligne du raycast
        XRInteractorLineVisual lineVisual = args.interactorObject.transform.GetComponent<XRInteractorLineVisual>();
        lineVisual.stopLineAtSelection = true;
        lineVisual.lineLength = 5;
        lineVisual.invalidColorGradient = ColorManager.Instance.invalidGradient;
    }
}
