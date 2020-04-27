using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class UiScript : Photon.MonoBehaviour
    {
        [SerializeField] private float energyUsed = 0.01f;
        public Image lifeImage;
        public Image energyImage;
        public Sprite[] sprites;
        public int nbPiece = 0;
        public static UiScript Instance;
        private static PhotonView _photonView;
        private Coroutine _coroutine;
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
            _photonView.RPC("RoutineGainLife",PhotonTargets.All);
        }
        
        public void UpdateEnergy()
        {
            _photonView.RPC("RoutineSpendEnergy",PhotonTargets.All, energyUsed);
        }
        
        [PunRPC]
        public void RoutineSpendEnergy(float _energyUsed)
        {
            Instance._restoreEnergy = false;
            if (Instance._coroutine != null)
            {
                StopCoroutine(Instance._coroutine);
            }
            Instance.energyImage.fillAmount -= _energyUsed;
            if (Instance.enabled)
            {
                Instance._coroutine = StartCoroutine(RoutineEnergy()); 
            }
        }
        
        [PunRPC]
        public void RoutineGainLife()
        {
            if (Instance.nbPiece != Instance.sprites.Length-1)
            {
                Instance.nbPiece++;
            }
        }

        private IEnumerator RoutineEnergy()
        {
            yield return new WaitForSeconds(3);
            _photonView.RPC("RestoreEnergyPunRpc",PhotonTargets.All);
        }

        [PunRPC]
        public void RestoreEnergyPunRpc()
        {
            Instance._restoreEnergy = true;
        }

        public bool HasEnoughEnergy()
        {
            return Instance.energyImage.fillAmount >= energyUsed;
        }

        public void FixedUpdate()
        {
            if (!_photonView.isMine)
            {
                return;
            }
            Instance.lifeImage.sprite = Instance.sprites[nbPiece];
            if (Instance._restoreEnergy)
            {
                Instance.energyImage.fillAmount += 0.02f;
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown("k"))
            {
                UpdateLife();
            }
            
            if (Input.GetKey("l"))
            {
                UpdateEnergy();
            }
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

        }
    }
}
