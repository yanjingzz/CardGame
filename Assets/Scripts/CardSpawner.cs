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
		public void RandomlyGetOne()
		{
			Card card = cardPool[Random.Range(0, cardPool.Count)];
			Debug.Log("Spawning: " + card);
			GameObject handCard = Instantiate(cardPrefab);
			handCard.GetComponent<CardManager>().Card = card;
            hands.AddCard(handCard);
		}
	}
}
