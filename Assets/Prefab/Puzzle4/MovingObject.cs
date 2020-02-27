using System;
using UnityEngine;

namespace Prefab.Puzzle4
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private bool state;
        [SerializeField] public MoveOfObject moveOfObjectX;
        [SerializeField] public MoveOfObject moveOfObjectY;
        [SerializeField] public MoveOfObject moveOfObjectZ;
        private Vector3 _move = Vector3.zero;

        // Update is called once per frame
        void Update()
        {
            Vector3 position = gameObject.transform.localPosition;
            _move += moveOfObjectX.CalculateMove(state, position.x);
            _move += moveOfObjectY.CalculateMove(state, position.y);
            _move += moveOfObjectZ.CalculateMove(state, position.z);
            gameObject.transform.Translate(_move);
            _move = Vector3.zero;
        }

        [PunRPC]
        public void StateChange()
        {
            state = !state;
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Contains("Player"))
            {
                other.gameObject.transform.SetParent(gameObject.transform);
            }
        }
        
        public void OnCollisionExit(Collision other)
        {
            if (other.gameObject.tag.Contains("Player"))
            {
                other.gameObject.transform.SetParent(null);
            }
        }
    }
}
