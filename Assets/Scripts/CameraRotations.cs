using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotations : MonoBehaviour
{
    [SerializeField] Camera myCamera;

    [SerializeField] private float sensitivity = 0.07f;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private float maxVerticalAngle = 60f;  // Maximum look up angle
    [SerializeField] private float minVerticalAngle = -60f; // Maximum look down angle

    private float verticalRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 vp = myCamera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, myCamera.nearClipPlane));
        vp.x -= 0.5f;
        vp.y -= 0.5f;
        vp.x *= sensitivity * Time.deltaTime * 10;
        vp.y *= sensitivity * Time.deltaTime * 10;
        vp.x += 0.5f;
        vp.y += 0.5f;

        Vector3 sp = myCamera.ViewportToScreenPoint(vp);
        Vector3 v = myCamera.ScreenToWorldPoint(sp);

        // Calculate the vertical angle using the dot product
        Vector3 forward = myCamera.transform.forward;
        float currentVerticalAngle = Vector3.Dot(forward, Vector3.up);

        //Camera angle restriction to prevent spinning when looking directly up/down
        if (currentVerticalAngle < 0.9f && currentVerticalAngle > -0.9f)
        {
            transform.LookAt(v, Vector3.up);
        } else
        {
            if (currentVerticalAngle >= 0.9f)
            {
                transform.Rotate(0.1f, 0.0f, 0.0f);
            } else
            {
                transform.Rotate(-0.1f, 0.0f, 0.0f);
            }
        }

    }
}
