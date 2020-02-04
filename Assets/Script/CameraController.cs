using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camPosition;
    private Transform camTransform;
    private float angleY;
    private float angleZ;

    void Start()
    {
        camTransform = cam.transform;
    }
    void FixedUpdate()
    {
        PerformRotationAroundPlayer();
    }

    public void RotateY(float _angleY)
    {
        angleY = _angleY;
    }

    private void PerformRotationAroundPlayer()
    {
        Vector3 position = player.position;
        Quaternion camRotation = camTransform.localRotation;
        cam.transform.RotateAround(position, Vector3.up, angleY);

        if (camRotation.x < 0.70 && camRotation.x > -0.35)
        {
            cam.transform.RotateAround(position, -camTransform.right, angleZ);
        }
        else if (angleZ > 0 && camRotation.x >= 0.70)
        {
            cam.transform.RotateAround(position, -camTransform.right, angleZ);
        }
        else if (angleZ < 0 && camRotation.x <= -0.35)
        {
            cam.transform.RotateAround(position, -camTransform.right, angleZ);
        }
        
        angleY = 0f;
        angleZ = 0f;
    }

    public void ReplaceCam()
    {
        camTransform.position = camPosition.position;
        camTransform.rotation = camPosition.rotation;
    }

    public void RotateZ(float _angleZ)
    {
        angleZ = _angleZ;
    }
}
