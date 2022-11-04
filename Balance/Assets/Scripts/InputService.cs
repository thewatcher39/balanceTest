using Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputService : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 _startPos;
    private Vector2 _startOffset;

    private bool _isNegativeRotateDirection;

    float last;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = eventData.pressPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _startOffset = eventData.position - _startPos;
        var temp = _startOffset.x + _startOffset.y;

        _isNegativeRotateDirection = temp > last;


        last = temp;
        LevelHandler.pickedObject.transform.Rotate(new Vector3(0, 0, _isNegativeRotateDirection ? 1 : -1));
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }
}
