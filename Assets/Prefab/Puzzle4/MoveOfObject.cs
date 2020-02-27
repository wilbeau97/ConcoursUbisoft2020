using System;
using UnityEngine;

namespace Prefab.Puzzle4
{
    [Serializable]
    public class MoveOfObject
    {
        [SerializeField] private Vector3 direction = Vector3.zero;
        [SerializeField] private float min;
        [SerializeField] private float max;

        public Vector3 CalculateMove(bool state, float currentPosition)
        {
            if (state && currentPosition < max)
            {
                return direction / 10;
            }
            else if (!state && currentPosition > min)
            {
                return -(direction / 10);
            }
            return Vector3.zero;
        }
    }
}