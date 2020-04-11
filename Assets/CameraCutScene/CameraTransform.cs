using System;
using UnityEngine;

namespace CameraCutScene
{
    [Serializable]
    public class CameraTransform
    {
        public Vector3 position;
        public Vector3 rotation;

        public CameraTransform(Transform transform)
        {
            position = transform.position;
            rotation = transform.eulerAngles;
        }
    }
}
