using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CardGame
{
    public class HandViz : MonoBehaviour
    {

        int screenHeight, screenWidth;
        public float percentageOfHeight = 0.5f;
        public GameObject layout;
        RectTransform rt;
        private void Start()
        {
            rt = GetComponent<RectTransform>();
            if (rt == null)
            {
                Debug.Log("HandViz : Missing RectTransform.");
            }
        }

        private void Update()
        {
            if(screenHeight != Screen.height || screenWidth != Screen.width)
            {
                screenHeight = Screen.height;
                screenWidth = Screen.width;
                var size = rt.sizeDelta;
                size.y = screenHeight * percentageOfHeight;
                rt.sizeDelta = size;
                LayoutContent();
            }
        }

        public void LayoutContent()
        {

            var layoutRT = layout.GetComponent<RectTransform>();
            var lh = layoutRT.rect.height;
            var lw = layoutRT.rect.width;

            var h = rt.rect.height;
            var w = rt.rect.width;

            //Debug.Log("layout: " + layoutRT.rect + " hand: " + h + ", " + w);s

            if(lh > 0 && lw > 0)
            {
                var scale = Mathf.Min(Mathf.Min(w / lw, h / lh), 1);

                layout.transform.localScale = new Vector2(scale, scale);
            } else
            {
                screenHeight = -1;
                screenWidth = -1;
            }
            foreach(Transform child in layout.transform)
            {
                child.localScale = new Vector3(1, 1, 1);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRT);


        }

        public void AddCard(GameObject card)
        {
            card.transform.SetParent(layout.transform);
            LayoutContent();
        }

    }
}


