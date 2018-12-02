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

        public override string ToString()
        {
            return Title;
        }

        public List<Card> UnlockCards;
		public List<AddingBuffs> AddBuffs;
    }

	[System.Serializable]
	public class AddingBuffs
	{
		public Buff buff;
		public float probability = 1.0f;
		public bool come = true;
		public int overrideLength = 0;
	}

    public enum CardType
    {
        Art = 0,
        Gameplay,
        Feel,
        Tech,
        Event,
        Choice
    }
    
}

