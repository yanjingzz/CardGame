using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardGame
{
    public class DragNDropBehaviour : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {

        bool dragging;
        public Action OnDrop;
        public Action OnPickup;

        Vector3 lastPos;
        Vector3 offSet;
        // Use this for initialization

        #region MonoBehaviour Messages

        void Start()
        {
            dragging = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (dragging)
            {
                transform.position = lastPos + offSet;
                //Debug.Log("" + transform.position + " " + BoardManager.Instance.HexAtPoint(transform.position));
            }

        }

        #endregion

        #region OnDrag Messages

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            dragging = true;
            offSet = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);
            offSet.z = -1;
            if (OnPickup != null)
                OnPickup();

        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            lastPos = Camera.main.ScreenToWorldPoint(eventData.position);
            lastPos.z = 0;
            //Debug.Log(Camera.main.WorldToViewportPoint(lastPos));
        }


        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            dragging = false;
            if (OnDrop != null)
                OnDrop();
        }


        #endregion
    }

}