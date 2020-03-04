using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public abstract void Pressed();
    public abstract void Interact();
    public abstract void SetValue(float _angleZ, Vector3 _playerPosition);
    public abstract void Release();
    public abstract void IncreaseAbility();
}
