using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camPosition;
    [SerializeField] private float minHeight = -0.35f;
    [SerializeField] private float maxHeight = 0.5f;
    [SerializeField] private InGameMenu inGameMenu;
    private Transform camTransform;
    private float angleY;
    private float angleZ;


    void Start()
    {
        camTransform = cam.transform;
    }
    
    void FixedUpdate()
    {
        if(!inGameMenu.menuShown)
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

        if (camRotation.x < maxHeight && camRotation.x >minHeight)
        {
            cam.transform.RotateAround(position, -camTransform.right, angleZ);
        }
        else if (angleZ > 0 && camRotation.x >= maxHeight)
        {
            cam.transform.RotateAround(position, -camTransform.right, angleZ);
        }
        else if (angleZ < 0 && camRotation.x <= minHeight)
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
