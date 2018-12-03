using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(DragNDropBehaviour))]
    public class CardManager : MonoBehaviour
    {
        GameObject hand;
		CardViz viz;
		[SerializeField] Card _card;
        public Card Card 
        { 
            get { return _card; }
            set
            {
                _card = value;
				if(viz != null)
                {
                    viz.DisplayCard();
                    UpdateValues();
                }
            }
        }
        
        private void Awake()
        {
            GetComponent<DragNDropBehaviour>().OnDrop += OnDrop;
            GetComponent<DragNDropBehaviour>().OnPickup+= OnPickUp;
            viz = GetComponent<CardViz>();
			//Debug.Log("viz", viz);
            if(_card == null)
            {
                Debug.LogWarning("Card manager: missing card");
            }
            hand = GameObject.FindWithTag("Hand");
            if(hand == null)
            {
                Debug.LogWarning("Card manager: missing hand area");
            }
        }

        private void OnDrop()
        {
            viz.OnDrop();
            if(InPlayArea())
            {

                bool canPlay = GameManager.Instance.CanPlayCard(this);
                if(canPlay)
                {
                    //Debug.Log("reset parent");
                    transform.SetParent(GameObject.FindWithTag("Canvas").transform, true);
                    GameManager.Instance.PlayCard(this);

                    viz.PlaceAnimation(Card);
                    Destroy(gameObject, 3);
                    return;
                }
                //TODO: some cool animation
            }
            viz.ShowShadow();
            hand.GetComponent<HandViz>().LayoutContent();
        }

        private bool InPlayArea()
        {
            Vector3[] v = new Vector3[4];
            hand.GetComponent<RectTransform>().GetWorldCorners(v);

            //Debug.Log(v[0].x + ", " + v[0].y + ", " + v[2].x + ", " + v[2].y);
            //Debug.Log(transform.position);
            // Check to see if the point is in the calculated bounds
            if (transform.position.x >= v[0].x &&
                transform.position.x <= v[2].x &&
                transform.position.y >= v[0].y &&
                transform.position.y <= v[2].y)
            {
                return false;
            }
            return true;
        }

        private void OnPickUp()
        {
            viz.OnPickup();
        }

        public void UpdateValues()
        {
            var pair = BuffManager.Instance.BuffCard(Card);
            viz.ChangeValues(pair.Key, pair.Value);
        }


    }

}