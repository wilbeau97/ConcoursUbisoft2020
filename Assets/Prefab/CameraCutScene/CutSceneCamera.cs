using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Prefab.CameraCutScene
{
    public class CutSceneCamera : MonoBehaviour
    {
        public Animation _animation;
        private AnimationClip _animationClip;
        public List<Transform> positions = new List<Transform>();
        public float travelSpeed;

        public void Start()
        {
            _animation = GetComponent<Animation>();
        }
        public void AddPosition()
        {
            positions.Add(gameObject.transform);
        }

        public void Awake()
        {
            _animationClip = new AnimationClip {legacy = true};
            Keyframe[] keysPositionX = new Keyframe[positions.Count];
            Keyframe[] keysPositionY = new Keyframe[positions.Count];
            Keyframe[] keysPositionZ = new Keyframe[positions.Count];
            Keyframe[] keysRotationX = new Keyframe[positions.Count];
            Keyframe[] keysRotationY = new Keyframe[positions.Count];
            Keyframe[] keysRotationZ = new Keyframe[positions.Count];
            for (int i = 0; i < positions.Count; i++)
            {
                print(positions[i].position.x);
                keysPositionX[i] = new Keyframe(travelSpeed * i, positions[i].position.x);
                keysPositionY[i] = new Keyframe(travelSpeed * i, positions[i].position.y);
                keysPositionZ[i] = new Keyframe(travelSpeed * i, positions[i].position.z);
                //keysRotationX[i] = new Keyframe(travelSpeed * i, positions[i].rotation.x);
                //keysRotationY[i] = new Keyframe(travelSpeed * i, positions[i].rotation.y);
                //keysRotationZ[i] = new Keyframe(travelSpeed * i, positions[i].rotation.z);
            }
            AnimationCurve animationCurvePositionsX = new AnimationCurve(keysPositionX);
            AnimationCurve animationCurvePositionsY = new AnimationCurve(keysPositionY);
            AnimationCurve animationCurvePositionsZ = new AnimationCurve(keysPositionZ);
            /*AnimationCurve animationCurveRotationsX = new AnimationCurve(keysRotationX);
            AnimationCurve animationCurveRotationsY = new AnimationCurve(keysRotationY);
            AnimationCurve animationCurveRotationsZ = new AnimationCurve(keysRotationZ);*/
            _animationClip.SetCurve("",typeof(Transform),"Position.x",animationCurvePositionsX);
            _animationClip.SetCurve("",typeof(Transform),"Position.y",animationCurvePositionsY);
            _animationClip.SetCurve("",typeof(Transform),"Position.z",animationCurvePositionsZ);
            /*_animationClip.SetCurve("",typeof(Transform),"Rotation.x",animationCurveRotationsX);
            _animationClip.SetCurve("",typeof(Transform),"Rotation.y",animationCurveRotationsY);
            _animationClip.SetCurve("",typeof(Transform),"Rotation.z",animationCurveRotationsZ);*/
            _animation.AddClip(_animationClip, _animationClip.name);
            _animation.Play(_animationClip.name);
        }
    }
}
