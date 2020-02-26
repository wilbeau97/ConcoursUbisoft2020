using System;
using System.Collections;
using UnityEngine;

namespace Script.AnimationGrabItem
{
    public class DiamondPath : MonoBehaviour
    {
        [SerializeField] private Vector3[] diamondMarker;
        private int _nextMaker = 0;

        // Start is called before the first frame update
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Player1"))
            {
                if (_nextMaker < diamondMarker.Length)
                {
                    Transform parent = gameObject.transform.parent;
                    gameObject.GetPhotonView().RPC("MoveDiamondPun", PhotonTargets.All, diamondMarker[_nextMaker]);
                    _nextMaker++;
                }
                else
                {
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }
        }

        [PunRPC]
        public void MoveDiamondPun(Vector3 move)
        {
            StartCoroutine(MoveDiamond(move));
        } 

        public IEnumerator MoveDiamond(Vector3 move)
        {
            gameObject.transform.parent.position = move;
            yield return null;
        }
    }
}
