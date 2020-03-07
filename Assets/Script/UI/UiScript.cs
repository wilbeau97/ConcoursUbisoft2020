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
        private Coroutine _coroutine;


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
            _photonView.RPC("RoutineSpendEnergy",PhotonTargets.All);
        }
        
        [PunRPC]
        public void RoutineSpendEnergy()
        {
            if (Instance._coroutine != null)
            {
                StopCoroutine(Instance._coroutine);
            }
            Instance.energyImage.fillAmount -= 0.01f;
            Instance._coroutine = StartCoroutine(RoutineEnergy());
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
            while (true)
            {
                Instance.energyImage.fillAmount += 0.01f;
                yield return null;
            }
        }

        public void FixedUpdate()
        {
            Instance.lifeImage.sprite = Instance.sprites[nbPiece];
        }

        public void Update()
        {
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
