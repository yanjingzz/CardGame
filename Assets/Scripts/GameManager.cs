using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        GameStatusViz viz;
		CardSpawner spawner;
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
            viz = GetComponent<GameStatusViz>();
			spawner = GetComponent<CardSpawner>();
            if(viz == null)
            {
                Debug.LogWarning("Game manager: missing visualizer");
            }
            viz.DisplayStatus();
        }

        public void PlayCard(Card card)
        {
            Debug.Log("Game manager: Played card: " + card);
            Time -= card.Time;
			spawner.RandomlyGetOne();
            if(Points.ContainsKey(card.Type))
            {
                Points[card.Type] += card.Points;
            }
            viz.DisplayStatus();
        }

        public void Discard(Card card)
        {

        }
    }
}

