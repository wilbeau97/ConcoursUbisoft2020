using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class UiLife : MonoBehaviour
    {
        private Image currentLevel;
        public Sprite[] sprites;
        public int nbPiece = 0;

        public void Start()
        {
            currentLevel = gameObject.GetComponent<Image>();
        }

        public void UpdateSprite()
        {
            if (nbPiece != sprites.Length-1)
            {
                nbPiece++;
            }
            currentLevel.sprite = sprites[nbPiece];
        }

        public void Update()
        {
            if (Input.GetKeyDown("k"))
            {
                UpdateSprite();
            }
        }
    }
}
