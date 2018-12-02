using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace CardGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        GameStatusViz viz;
		CardSpawner spawner;
		BuffManager buffManager;
        public List<Card> initialHand = new List<Card>();
        public int Time 
        { 
            get { return _time; } 
            private set
            {
                _time = value;
                if(_time <= 0)
                {
                    GameOver();
                }
            }
        }
        private int _time;
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
			buffManager = GetComponent<BuffManager>();
            if(viz == null)
            {
                Debug.LogWarning("Game manager: missing visualizer");
            }
            viz.DisplayStatus();

            foreach(Card card in initialHand)
            {
                spawner.Spawn(card);
            }

        }

        public bool PlayCard(Card card)
        {
			KeyValuePair<int, int> newTimePoint = buffManager.BuffCard(card);
			int cardTime = newTimePoint.Key;
			int cardPoint = newTimePoint.Value;
            if(cardTime > Time)
            {
                Debug.Log("Game manager: Trying to do " + card + " but don't have enough time");
                return false;
            }
            else
            {
                Debug.Log("Game manager: Played card: " + card);
                Time -= cardTime;
                spawner.RandomlyGetOne();
				buffManager.ChangingBuff(card);
				if (Points.ContainsKey(card.Type))
                {
                    Points[card.Type] += cardPoint;
                }
                viz.DisplayStatus();
                return true;
            }

        }

        public void Discard(Card card)
        {

        }

        public void GameOver()
        {
            viz.DisplayGameOver();
        }

        public void Restart()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

