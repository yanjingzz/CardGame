using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
	public class CardSpawner : MonoBehaviour
	{
		public HandViz hands;
		public List<Card> cardPool;
		public GameObject cardPrefab;
		public Card RandomlyGetOne()
		{
			Card card = cardPool[Random.Range(0, cardPool.Count)];

            Spawn(card);

            return card;
        }

        public void Spawn(Card card)
        {
            if (card.Type == CardType.Art ||
                card.Type == CardType.Gameplay ||
                card.Type == CardType.Tech ||
                card.Type == CardType.Feel)
            {
                if (card.Title != "Debug" && card.Title != "Playtest")
                {
                    cardPool.Remove(card);
                }
            }

            Debug.Log("Spawning: " + card);
            GameObject handCard = Instantiate(cardPrefab);
            handCard.GetComponent<CardManager>().Card = card;
            hands.AddCard(handCard);
        }

    }
}
