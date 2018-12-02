using System.Collections;
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
        private void Start()
        {
            Time = 48 * 60;
        }
        public void PlayCard(Card card)
        {
            Time -= card.Time;
        }

        public void Discard(Card card)
        {

        }
    }
}

