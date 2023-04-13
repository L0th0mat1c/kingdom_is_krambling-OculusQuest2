using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayManager : MonoBehaviour
{
    public XRRayInteractor[] rayInteractorsList;

    // Création des listeners sur les Ray Interactors
    private void OnEnable() {
        foreach(XRRayInteractor rayInteractor in rayInteractorsList) {
            rayInteractor.selectEntered.AddListener(objectGrabbed);
            rayInteractor.selectExited.AddListener(resetRay);
        }
    }
    // Désactivation des listeners sur les Ray Interactors
    private void OnDisable() {
        foreach(XRRayInteractor rayInteractor in rayInteractorsList) {
            rayInteractor.selectEntered.RemoveListener(objectGrabbed);
            rayInteractor.selectExited.RemoveListener(resetRay);
        }
    }

    // Change le mode du Ray Interactor quand on intéragi avec un objet
    private void objectGrabbed(SelectEnterEventArgs args) {
        // Si on sélectionne une carte
        if(args.interactableObject.transform.tag == "Card") {
            // On set le raycast en Projectile Mode
            args.interactorObject.transform.GetComponent<XRRayInteractor>().lineType = XRRayInteractor.LineType.ProjectileCurve;
            
            // Visuel de la ligne du raycast
            XRInteractorLineVisual lineVisual = args.interactorObject.transform.GetComponent<XRInteractorLineVisual>();
            lineVisual.stopLineAtSelection = false;
            lineVisual.lineLength = 100;

            // Visuel du réticule
            args.interactorObject.transform.GetComponent<XRInteractorReticleVisual>().enabled = true;

            setVisibityReticles(true);
        }
    }

    // Reset les valeurs par défaut du Ray Interactor
    private void resetRay(SelectExitEventArgs args) {
        // On remet le Raycast en ligne droite
        args.interactorObject.transform.GetComponent<XRRayInteractor>().lineType = XRRayInteractor.LineType.StraightLine;

        // Visuel de la ligne du raycast
        XRInteractorLineVisual lineVisual = args.interactorObject.transform.GetComponent<XRInteractorLineVisual>();
        lineVisual.stopLineAtSelection = true;
        lineVisual.lineLength = 10;

        // Visuel du réticule
        args.interactorObject.transform.GetComponent<XRInteractorReticleVisual>().enabled = false;

        setVisibityReticles(false);
    }

    // Permet d'activer ou de désactiver tous les réticules
    private void setVisibityReticles(bool visible) {
        foreach(GameObject reticle in GameObject.FindGameObjectsWithTag("Reticles")) {
            reticle.SetActive(visible);
        }
    }
}
