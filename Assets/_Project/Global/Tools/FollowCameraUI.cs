using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraUI : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(cam.transform.position.x + 2, cam.transform.position.y, cam.transform.position.z + 2);
        transform.rotation = cam.transform.rotation;
        transform.LookAt(cam.transform);
    }
}
