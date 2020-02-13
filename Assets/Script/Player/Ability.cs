using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract void Pressed();
    public abstract void test();
    public abstract void SetValue(float _angleZ, Vector3 _playerPosition);
}
