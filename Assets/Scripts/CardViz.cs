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
        public Image points;
        public Image time;
        public RectTransform pointsMask, timeMask;
        public Text description;
        public float maskTotalLength = 182;
        public GameObject Shadow;
        CardManager cardManager;
        public void Awake()
        {
            cardManager = GetComponent<CardManager>();
        }

        public void DisplayCard ()
        {
            if(cardManager == null)
            {
                Debug.LogWarning("CardViz: missing card manager");
				return;
            }
            Card card = cardManager.Card;
            if(card != null)
            {
                image.sprite = card.Image;
                title.text = card.Title;
                description.text = card.Description;
                title.color = whiteText(card) ? Color.white : Color.black;
                description.color = whiteText(card) ? Color.white : Color.black;
                time.color = ShowTime(card) ? Color.white : Color.clear;
                points.color = PointsColor(card);
                ChangeValues(card.Time, card.Points);

            }
        }

        private void OnValidate()
        {
            cardManager = GetComponent<CardManager>();
            DisplayCard();

        }

        private bool ShowTime(Card card)
        {
            return card.Type != CardType.Choice;
        }

        private Color PointsColor(Card card)
        {
            switch (card.Type)
            {
                case CardType.Art:
                    return new Color32(0xE5, 0x3A, 0xAF, 0x77);
                case CardType.Tech:
                    return new Color32(0x60, 0xF2, 0xA8, 0x77);
                case CardType.Feel:
                    return new Color32(0xE7, 0xEA, 0x3C, 0x77);
                case CardType.Gameplay:
                    return new Color32(0xFC, 0xA0, 0x4F, 0x77);
                default:
                    return Color.clear;

            }

        }

        private bool whiteText(Card card)
        {
            return card.Type == CardType.Choice || card.Type == CardType.Event;
        }

        public void HideShadow()
        {
            Shadow.SetActive(false);
        }

        public void ShowShadow()
        {
            Shadow.SetActive(true);
        }

        public void ChangeValues(int time, int points)
        {
            var size = timeMask.sizeDelta;
            size.x = maskTotalLength / 10 * time / 60;
            timeMask.sizeDelta = size;

            size = pointsMask.sizeDelta;
            size.x = maskTotalLength / 10 * points;
            pointsMask.sizeDelta = size;
        }
    }

}
