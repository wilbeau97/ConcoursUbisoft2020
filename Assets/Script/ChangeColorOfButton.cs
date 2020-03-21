using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColorOfButton : MonoBehaviour, ISelectHandler,IDeselectHandler
{
    [SerializeField] private Image button;

    public void OnSelect(BaseEventData eventData)
    {
        button.color = Color.cyan;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        button.color = Color.white;
    }
}
