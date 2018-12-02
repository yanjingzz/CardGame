using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CardGame
{
    public class BuffViz : MonoBehaviour {

        public Buff buff;
        public Image image;
        public Text text;
        public Text description;
        public void DisplayBuff()
        {
            text.text = buff.Title;
            image.sprite = buff.Image;
            description.text = buff.Description;
        }

        private void OnValidate()
        {
            DisplayBuff();
        }




    }
}