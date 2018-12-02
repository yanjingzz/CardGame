using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipBehaviour : MonoBehaviour, IPointerExitHandler,IPointerEnterHandler {

    public GameObject tooltip;

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
