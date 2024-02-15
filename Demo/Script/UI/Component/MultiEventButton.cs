using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    /// <summary>
    /// 右击双击
    /// </summary>
    public class MultiEventButton:MonoBehaviour,IPointerClickHandler
    {
       
            public Action<GameObject>  leftClick;
            public Action<GameObject> middleClick;
            public Action<GameObject> rightClick;
            
            public void OnPointerClick(PointerEventData eventData)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                    leftClick?.Invoke(gameObject);
                else if (eventData.button == PointerEventData.InputButton.Middle)
                    middleClick?.Invoke(gameObject);
                else if (eventData.button == PointerEventData.InputButton.Right)
                    rightClick?.Invoke(gameObject);
            }

    }
}