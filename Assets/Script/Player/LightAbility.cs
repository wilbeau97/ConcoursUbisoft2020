using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbility : Ability
{
    public override void Pressed()
    {
        
    }

    public override void Interact()
    {
        
    }

    public override void SetValue(float _angleZ, Vector3 _playerPosition)
    {
        
    }

    public override void Release()
    {
        
    }

    public override void ActivateTutorial()
    {
        transform.GetChild(0).gameObject.GetComponent<PlayerHUD>().setText(
            "Vous pouvez utilisez le bouton A pour sauté jusqu'à deux fois");
    }

    public override void DesactivateTutorial()
    {
        transform.GetChild(0).gameObject.GetComponent<PlayerHUD>().DeactivateAbilityText();
    }
}
