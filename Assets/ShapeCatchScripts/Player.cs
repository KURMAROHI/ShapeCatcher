using System;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace ShapeCatcher
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Player : MonoBehaviour
    {
        private float _screenWidth;
        private Camera _mainCamera;
        private float _offsetHeight = 1f;
        private BoxCollider2D _boxCollider;
        // private float _xExtent;
        private int _targetIndex;
        private int _currentScore = 0;
        private bool _isGameEnd = false;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _xoffset = 0.5f;
        public static EventHandler<string> UpdateScoreText;
        public static EventHandler GameEnd;
        private void Start()
        {
            _currentScore = 0;
            _mainCamera = Camera.main;
            _boxCollider = transform.GetComponent<BoxCollider2D>();
            _screenWidth = _mainCamera.aspect * _mainCamera.orthographicSize;
            transform.position = new Vector3(0, -_mainCamera.orthographicSize + _offsetHeight, 0);
            ShapeGenrator.SetTargetIndex += SetTargetIndex;
        }


        private void Update()
        {
            if (_isGameEnd)
                return;
            if (Input.GetMouseButton(0))
            {
                Vector2 targetposition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                float xPosition = Mathf.Clamp(targetposition.x, -_screenWidth + _xoffset, _screenWidth - _xoffset);
                targetposition = new Vector3(xPosition, transform.position.y, 0);
                if (Mathf.Abs(xPosition - transform.position.x) >= 0.5f)
                {
                    transform.position = Vector3.Lerp(transform.position, targetposition, Time.deltaTime * _speed);
                }
                else
                {
                    transform.position = targetposition;
                }

            }

        }

        private void OnDisable()
        {
            ShapeGenrator.SetTargetIndex -= SetTargetIndex;
        }

        private void SetTargetIndex(object sender, int e)
        {
            _targetIndex = e;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_isGameEnd && !collision.transform.CompareTag("Player"))
                return;

            if (!collision.transform.TryGetComponent<Shape>(out Shape shape))
                return;


            Debug.Log("getandsetIndex|" + shape.getandsetIndex + "|" + _targetIndex);
            if (shape.getandsetIndex == _targetIndex)
            {
                _currentScore++;
                string newString = "Score:" + _currentScore.ToString();
                UpdateScoreText?.Invoke(this, newString);
                Transform collidedobject = collision.transform;
                Rigidbody2D rb = collidedobject.GetComponent<Rigidbody2D>();
                rb.gravityScale = 0;
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.linearVelocity = Vector2.zero;
                collidedobject.transform.GetComponent<Collider2D>().enabled = false;
                collidedobject.SetParent(this.transform);


            }
            else
            {
                _isGameEnd = true;
                GameEnd?.Invoke(this, EventArgs.Empty);
                _currentScore = 0;
                UpdateScoreText?.Invoke(this, "Score:0");
            }

        }


    }
}
