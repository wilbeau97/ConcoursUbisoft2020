using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class UiScript : MonoBehaviour
    {
        public Image lifeImage;
        public Image energyImage;
        public Sprite[] sprites;
        public int nbPiece = 0;
        public static UiScript Instance;
        private static PhotonView _photonView;
        private bool _restoreEnergy = true;


        public void Awake()
        {
            if (Instance == null && gameObject.activeSelf)
            {
                Instance = this;
                _photonView = gameObject.GetPhotonView();
            }
        }

        public void UpdateLife()
        {
            if (nbPiece != sprites.Length-1)
            {
                nbPiece++;
            }
            lifeImage.sprite = sprites[nbPiece];
        }
        
        public void UpdateEnergy()
        {
            _photonView.RPC("RoutineSpendEnergy",PhotonTargets.All);
            //energyImage.fillAmount -= 0.01f;
        }
        
        [PunRPC]
        public void RoutineSpendEnergy()
        {
            //Instance._restoreEnergy = !Instance._restoreEnergy;
            Instance.energyImage.fillAmount -= 0.01f;
            Debug.LogWarning("test");
        }

        private IEnumerator RoutineEnergy()
        {
            
            yield return null;
        }

        public void Update()
        {
            if (Instance._restoreEnergy)
            {
                //energyImage.fillAmount += 0.01f;    
            }
            else
            {
                //energyImage.fillAmount -= 0.01f;
            }
            if (Input.GetKeyDown("k"))
            {
                UpdateLife();
            }
            
            if (Input.GetKeyDown("l"))
            {
                UpdateEnergy();
            }
        }
    }
}
