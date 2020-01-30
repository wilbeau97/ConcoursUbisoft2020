using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform camPosition;
    private float angle;

    void FixedUpdate()
    {
        PerformRotationAroundPlayer();
    }

    public void Rotate(float _angle)
    {
        angle = _angle;
    }

    private void PerformRotationAroundPlayer()
    {
        Vector3 position = player.position;
        cam.transform.RotateAround(position, Vector3.up, angle);
        angle = 0f;
    }

    public void ReplaceCam()
    {
        Transform camTransform = cam.transform;
        camTransform.position = camPosition.position;
        camTransform.rotation = camPosition.rotation;
    }
}
