
using System;
using System.Collections.Generic;
using UnityEngine;


namespace ShapeCatcher
{

    public class ShapeGenrator : MonoBehaviour
    {
        private Camera _mainCamera;
        private float _xOffset = 0.5f;
        private float _yOffset = 0.5f;
        private float _ypos;
        private float _width;
        private float timer;
        private int _targetIndex;
        private Sprite _targetSprite;
        private bool _isGameEnd = false;
        [SerializeField] private ShapePooler _pooler;
        [SerializeField] private List<GameObject> allActiveShapes = new List<GameObject>();


        public float spawnInterval = 1f;
        public float spawnXMin = -2f;
        public float spawnXMax = 2f;
        public float spawnY = 5f;
        public static EventHandler<Sprite> TargetImageUIHandler;
        public static EventHandler<int> SetTargetIndex;


        private void Awake()
        {
            _mainCamera = Camera.main;
            _ypos = _mainCamera.orthographicSize + _yOffset;
            _width = _mainCamera.aspect * _mainCamera.orthographicSize;
            Player.GameEnd += GameEnd;
        }

        private void Start()
        {
            _pooler.GetTargetIndexAndShape(out _targetSprite, out _targetIndex);
            TargetImageUIHandler?.Invoke(this, _targetSprite);
            SetTargetIndex?.Invoke(this, _targetIndex);
        }

        private void Update()
        {
            if (_isGameEnd)
                return;
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                timer = 0f;
                SpawnShape();
            }

            MoveShapeToPool();
        }

        private void OnDisable()
        {
            Player.GameEnd -= GameEnd;

        }

        private void GameEnd(object sender, EventArgs e)
        {
            _isGameEnd = true;
            OnGameEnd();
        }

        private void SpawnShape()
        {
            GameObject shape = _pooler.GetRandomShape();
            shape.transform.localScale = Vector3.one;
            allActiveShapes.Add(shape);
            if (shape.transform.TryGetComponent<Collider2D>(out Collider2D collider2D))
            {
                collider2D.isTrigger = false;
            }
            shape.transform.position = GetRandomPosition();
        }


        private Vector3 GetRandomPosition()
        {
            float xRandomVal = UnityEngine.Random.Range(-_width, _width + 1);
            float addorSub = UnityEngine.Random.Range(-1, 1) == 0 ? -1 * UnityEngine.Random.value : 1 * UnityEngine.Random.value;
            float newXPos = xRandomVal + addorSub;
            newXPos = Mathf.Clamp(newXPos, -_width + _xOffset, _width - _xOffset);
            return new Vector3(newXPos, _ypos, 0);
        }

        private void MoveShapeToPool()
        {
            if (allActiveShapes.Count == 0)
                return;
            for (int i = 0; i < allActiveShapes.Count; i++)
            {
                GameObject item = allActiveShapes[i];
                if (item.transform.parent != null && item.transform.parent.CompareTag("Player"))
                {
                    Debug.LogError("item Remove From List");
                    allActiveShapes.Remove(item);
                    continue;
                }

                if (item.transform.position.y < -_mainCamera.orthographicSize - _yOffset)
                {
                    ReturnToPool(item);
                }
            }
        }


        private void OnGameEnd()
        {
            if (allActiveShapes.Count == 0)
                return;
            for (int i = 0; i < allActiveShapes.Count; i++)
            {
                GameObject item = allActiveShapes[i];
                if (item.activeInHierarchy)
                {
                    ReturnToPool(item);
                }
            }
        }


        private void ReturnToPool(GameObject item)
        {
            int index = 0;
            if (item.TryGetComponent<Shape>(out Shape shapeScript))
            {
                index = shapeScript.getandsetIndex;
            }
            allActiveShapes.Remove(item);
            _pooler.ReturnToPool(item, index);
        }

    }



}
