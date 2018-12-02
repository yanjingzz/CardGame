﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    [RequireComponent(typeof(DragNDropBehaviour))]
    public class CardManager : MonoBehaviour
    {   
        RectTransform hand;
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
                }
            }
        }

        CardViz viz;
        private void Start()
        {
            GetComponent<DragNDropBehaviour>().OnDrop += OnDrop;
            viz = GetComponent<CardViz>();
            if(_card == null)
            {
                Debug.LogWarning("Card manager: missing card");
            }
            hand = GameObject.FindWithTag("Hand").GetComponent<RectTransform>();
            if(hand == null)
            {
                Debug.LogWarning("Card manager: missing hand area");
            }
        }

        private void OnDrop()
        {
            if(InPlayArea())
            {
                GameManager.Instance.PlayCard(_card);
                Destroy(gameObject);
            }
            else
            {
                //TODO: return to initial position
            }
        }

        private bool InPlayArea()
        {
            Vector3[] v = new Vector3[4];
            hand.GetWorldCorners(v);

            Debug.Log(v[0].x + ", " + v[0].y + ", " + v[2].x + ", " + v[2].y);
            Debug.Log(transform.position);
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

    }
}