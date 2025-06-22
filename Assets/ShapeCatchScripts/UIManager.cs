using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShapeCatcher
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] private Image _targetImage;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Transform _canvas;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // DontDestroyOnLoad(gameObject);
            Instance = this;
            _scoreText.text = "Score:0";
        }

        private void OnEnable()
        {
            ShapeGenrator.TargetImageUIHandler += TargetImageUIHandler;
            Player.UpdateScoreText += UpdateScoreText;
            Player.GameEnd += GameEnd;
        }


        private void OnDisable()
        {
            ShapeGenrator.TargetImageUIHandler -= TargetImageUIHandler;
            Player.UpdateScoreText -= UpdateScoreText;
            Player.GameEnd -= GameEnd;
        }

        private void TargetImageUIHandler(object sender, Sprite e)
        {
            _targetImage.sprite = e;
        }


        private void UpdateScoreText(object sender, string scoreText)
        {
            _scoreText.text = scoreText;
        }

        private void GameEnd(object sender, EventArgs e)
        {
            Debug.Log("OnGame End Called");
            GameObject popup = Instantiate(Resources.Load("Popups/ShapeCatchGameOverUI"), _canvas) as GameObject;
            popup.name = "GameOverUI";
            popup.SetActive(true);
        }

        public string GetScoreText => _scoreText.text;

    }

}

