using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardGame
{
    public class CardViz : MonoBehaviour
    {
        [SerializeField] Card _card;
        public Image image;
        public Text title;
        public Text points;
        public Text time;
        public Text description;
        public Card Card
        {
            get { return _card; }
            set
            {
                _card = value;
                DisplayCard();
            }
        }

        public void DisplayCard ()
        {
            if(_card != null)
            {
                image.sprite = _card.Image;
                title.text = _card.Title;
                description.text = _card.Description;
                time.text = (_card.Time / 60).ToString();
                points.text = (_card.Points).ToString();

            }
        }
        private void OnValidate()
        {
            DisplayCard();
        }
    }

}
