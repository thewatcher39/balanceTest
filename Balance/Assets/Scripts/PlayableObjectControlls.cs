using Handlers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ObjectBehaviour
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayableObjectControlls : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _startPosition;
        private Vector3 _objectPosition;
        private bool _inPlayzone;
        private bool _isNotPlaceable;

        private void Start()
        {
            CacheVars();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LevelHandler.pickedObject = gameObject;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_inPlayzone)
                SetStartState();
            else if (_isNotPlaceable)
                _rb.MovePosition(new Vector2(_objectPosition.x, _objectPosition.y + gameObject.transform.localScale.y));
        }

        public void OnDrag(PointerEventData eventData)
        {
            _objectPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rb.MovePosition(_objectPosition);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("NotPlaceable"))
            {
                _isNotPlaceable = true;
                _spriteRenderer.color = new Color(1, 1, 1, 0.7f);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("ObjectPickZone"))
            {
                _inPlayzone = false;
                SetPlayState();
            }
            else if (collision.gameObject.CompareTag("NotPlaceable"))
            {
                _isNotPlaceable = false;
                _spriteRenderer.color = new Color(1, 1, 1, 1f);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("ObjectPickZone"))
            {
                _inPlayzone = true;
            }
        }

        private void CacheVars()
        {
            _startPosition = gameObject.transform.position;
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void SetStartState()
        {
            gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
            LevelHandler.pickedObject.transform.position = _startPosition;
        }

        private void SetPlayState()
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
    }
}