using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace CardGame
{
    [RequireComponent(typeof(CardManager))]
    public class CardViz : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler
    {
        public Image image;
        public Text title;
        public Image points;
        public Image time;
        public RectTransform pointsMask, timeMask;
        public Text description;
        public float maskTotalLength = 182;
        public Image Shadow;
        public ParticleSystem particle;
        public LayoutElement le;
        CardManager cardManager;
        public void Awake()
        {
            cardManager = GetComponent<CardManager>();
        }
        private void Start()
        {
            le = GetComponent<LayoutElement>();
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

        private Color AccentColor(Card card)
        {
            switch (card.Type)
            {
                case CardType.Art:
                    return new Color32(0xE5, 0x3A, 0xAF, 0xff);
                case CardType.Tech:
                    return new Color32(0x60, 0xF2, 0xA8, 0xff);
                case CardType.Feel:
                    return new Color32(0xE7, 0xEA, 0x3C, 0xff);
                case CardType.Gameplay:
                    return new Color32(0xFC, 0xA0, 0x4F, 0xff);
                default:
                    return Color.white;
            }
        }

        Color BackgoundColor(Card card)
        {
            return whiteText(card) ? (Color) new Color32(0x55, 0x70, 0xFA, 0xff) : Color.white;
        }
        static CardViz dragging;
        static readonly Object draggingLock = new Object();
        public void OnPickup()
        {
            lock(draggingLock)
            {
                if(dragging == null)
                    dragging = this;
            }

            Shadow.DOFade(0, 0.1f);
        }

        public void OnDrop()
        {
            lock (draggingLock)
            {
                Debug.Log("Drop");
                dragging = null;
            }
        }
        public void ShowShadow()
        {
            Shadow.DOFade(1, 0.1f);
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

        public void PlaceAnimation(Card card)
        {
            var main = particle.main;
            var pointsColor = AccentColor(card);
            var backgoundColor = BackgoundColor(card);
            main.startColor = new ParticleSystem.MinMaxGradient(pointsColor, backgoundColor);
            particle.Emit(100);
            FadeEverything();
            Debug.Log("Place animation");
        }

        void FadeEverything()
        {
            image.DOFade(0, 1);
            title.DOFade(0, 1);
            description.DOFade(0, 1);
            points.DOFade(0, 1);
            time.DOFade(0, 1);

        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            lock(draggingLock)
            {
                if(dragging == null || dragging == this)
                {
                    Debug.Log("OnPointerEnter");
                    GameObject canvas = GameObject.FindWithTag("Canvas");
                    float scale = canvas.transform.localScale.x / transform.lossyScale.x * 1.1f;
                    DOTween.To(() => le.minWidth, x => le.minWidth = x, 350, 0.2f);
                    transform.DOScale(scale, 0.2f);
                }
            }

        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            lock (draggingLock)
            {
                if (dragging == null)
                {
                    Debug.Log("OnPointerExit");
                    DOTween.To(() => le.minWidth, x => le.minWidth = x, 200, 0.2f);
                    transform.DOScale(1, 0.2f);
                }
            }


        }
    }

}
