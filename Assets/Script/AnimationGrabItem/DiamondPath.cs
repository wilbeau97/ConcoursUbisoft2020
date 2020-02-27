using System;
using System.Collections;
using UnityEngine;

namespace Script.AnimationGrabItem
{
    public class DiamondPath : MonoBehaviour
    {
        [SerializeField] private Vector3[] diamondMarker;
        private int _nextMaker = 0;
        private Animation _animation;
        private AnimationClip _animationClip;

        public void Start()
        {
            _animation = GetComponent<Animation>();
        }

        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Player1"))
            {
                if (_nextMaker < diamondMarker.Length)
                {
                    Transform parent = gameObject.transform.parent;
                    if (PhotonNetwork.connected)
                    {
                        gameObject.GetPhotonView().RPC("MoveDiamondPun", PhotonTargets.All, diamondMarker[_nextMaker]);
                    }
                    else
                    {
                        gameObject.SendMessage("MoveDiamondPun", diamondMarker[_nextMaker]);
                    }
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

        private IEnumerator MoveDiamond(Vector3 move)
        {
            Vector3 moveToDo = move - gameObject.transform.parent.transform.localPosition;
            const float speed = 3.0f;
            _animationClip = new AnimationClip {legacy = true};
            AnimationCurve animationCurveX = AnimationCurve.Linear(0.0f,0.0f,speed,moveToDo.x);
            AnimationCurve animationCurveY = AnimationCurve.Linear(0.0f,0.0f,speed,moveToDo.y);
            AnimationCurve animationCurveZ = AnimationCurve.Linear(0.0f,0.0f,speed,moveToDo.z);
            _animationClip.SetCurve("",typeof(Transform),"localPosition.x",animationCurveX);
            _animationClip.SetCurve("",typeof(Transform),"localPosition.y",animationCurveY);
            _animationClip.SetCurve("",typeof(Transform),"localPosition.z",animationCurveZ);
            AnimationEvent animationEvent = new AnimationEvent
            {
                time = _animationClip.length, functionName = "DiamondNewPlace"
            };
            //Section pour l'event
            _animationClip.AddEvent(animationEvent);
            _animation.AddClip(_animationClip, _animationClip.name);
            _animation.Play(_animationClip.name);
            yield return null;
        }

        public void DiamondNewPlace()
        {
            gameObject.transform.parent.localPosition = diamondMarker[_nextMaker];
            _nextMaker++;
        }
    }
}
