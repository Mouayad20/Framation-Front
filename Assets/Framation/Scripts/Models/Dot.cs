using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dot : MonoBehaviour , IDragHandler, IPointerClickHandler {

    public int id;
    public Action<Dot> onDragEvent;
    public Action<Dot> onDragMoveEvent;
    public Action<Dot> OnRightClickEvent;
    public Action<Dot> OnLeftClickEvent;

    public void OnDrag(PointerEventData eventData){
        if (eventData.pointerId == -1){
            onDragEvent?.Invoke(this);
            onDragMoveEvent?.Invoke(this);
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        if (eventData.pointerId == -2){
            OnRightClickEvent?.Invoke(this);
        }
        else if (eventData.pointerId == -1){
            OnLeftClickEvent?.Invoke(this);
        }
    }

}
