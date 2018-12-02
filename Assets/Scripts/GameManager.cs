﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int Time { get; private set; }
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;

            } else if(Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public Dictionary<CardType, int> Points { get; private set; } 
        private void Start()
        {
            Points = new Dictionary<CardType, int>(); 
            Time = 48 * 60;
            Points.Add(CardType.Art, 0);
            Points.Add(CardType.Feel, 0);
            Points.Add(CardType.Gameplay, 0);
            Points.Add(CardType.Tech, 0);
        }
        public void PlayCard(Card card)
        {
            Time -= card.Time;
            if(card.Type != CardType.Event)
            {
                Points[card.Type] += card.Points;
            }
        }

        public void Discard(Card card)
        {

        }
    }
}
