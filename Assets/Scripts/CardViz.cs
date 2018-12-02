using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    [RequireComponent(typeof(CardManager))]
    public class CardViz : MonoBehaviour
    {
        public Image image;
        public Text title;
        public Text points;
        public Text time;
        public Text description;
        CardManager cardManager;
        public void Start()
        {
            cardManager = GetComponent<CardManager>();
        }

        public void DisplayCard ()
        {
            if(cardManager == null)
            {
                Debug.LogWarning("CardViz: missing card manager");
            }
            Card card = cardManager.Card;
            if(card != null)
            {
                image.sprite = card.Image;
                title.text = card.Title;
                description.text = card.Description;
                time.text = (card.Time / 60).ToString();
                points.text = (card.Points).ToString();

            }
        }

        private void OnValidate()
        {
            cardManager = GetComponent<CardManager>();
            DisplayCard();

        }
    }

}
