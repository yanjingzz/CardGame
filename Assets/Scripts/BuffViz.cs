using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CardGame
{
    public class BuffViz : MonoBehaviour 
    {
        Buff _buff;
        public Buff Buff 
        { 
            get { return _buff; }
            set
            {
                _buff = value;
                DisplayBuff();
            }
        }
        public Image image;
        public Text text;
        public Text description;
        public void DisplayBuff()
        {
            text.text = _buff.Title;
            image.sprite = _buff.Image;
            description.text = _buff.Description;
        }

        private void OnValidate()
        {
            DisplayBuff();
        }




    }
}