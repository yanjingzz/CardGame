using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardGame
{
    [CreateAssetMenu]
    public class Card : ScriptableObject
    {
        public string Title;
        public string Description;
        public Sprite Image;
        public CardType Type;
        public int Time; // in minutes
        public int Points; 
    }

    public enum CardType
    {
        Art = 0,
        Gameplay,
        Feel,
        Tech,
        Event
    }
}

