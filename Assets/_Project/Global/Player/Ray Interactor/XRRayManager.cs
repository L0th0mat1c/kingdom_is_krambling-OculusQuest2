using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRRayManager : MonoBehaviour
{
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    private void OnEnable() {
        leftRayInteractor.selectEntered.AddListener(objectGrabbed);
        rightRayInteractor.selectEntered.AddListener(objectGrabbed);
        leftRayInteractor.selectExited.AddListener(resetRay);
        rightRayInteractor.selectExited.AddListener(resetRay);
    }
    private void OnDisable() {
        leftRayInteractor.selectEntered.RemoveListener(objectGrabbed);
        rightRayInteractor.selectEntered.RemoveListener(objectGrabbed);
        leftRayInteractor.selectExited.RemoveListener(resetRay);
        rightRayInteractor.selectExited.RemoveListener(resetRay);
    }
    private void objectGrabbed(SelectEnterEventArgs args) {
        Debug.Log("grabbed");
        if(args.interactableObject.transform.tag == "Card") {
            args.interactorObject.transform.GetComponent<XRRayInteractor>().lineType = XRRayInteractor.LineType.ProjectileCurve;
            XRInteractorLineVisual lineVisual = args.interactorObject.transform.GetComponent<XRInteractorLineVisual>();
            lineVisual.stopLineAtSelection = false;
            lineVisual.lineLength = 100;
            XRInteractorReticleVisual reticleVisual = args.interactorObject.transform.GetComponent<XRInteractorReticleVisual>();
            reticleVisual.enabled = true;
        }
    }

    private void resetRay(SelectExitEventArgs args) {
        args.interactorObject.transform.GetComponent<XRRayInteractor>().lineType = XRRayInteractor.LineType.StraightLine;
        XRInteractorLineVisual lineVisual = args.interactorObject.transform.GetComponent<XRInteractorLineVisual>();
        lineVisual.stopLineAtSelection = true;
        lineVisual.lineLength = 10;
        XRInteractorReticleVisual reticleVisual = args.interactorObject.transform.GetComponent<XRInteractorReticleVisual>();
        reticleVisual.enabled = false;
    }
}
