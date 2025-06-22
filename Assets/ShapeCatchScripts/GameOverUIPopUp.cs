using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace ShapeCatcher
{
    public class GameOverUIPopUp : MonoBehaviour
    {

        private string _homeSceneName = "ShapeCatchHomeScreen"; // _homeSceneName 
        [SerializeField] private Text _scoreText;


        private void Awake()
        {
            _scoreText.text = UIManager.Instance.GetScoreText;
        }

        public void OnHomeButtonClick(Transform button)
        {
            Debug.Log("OnHomeButtonClick");
            AnimateButton(button, () =>
            {
                StartCoroutine(nameof(LoadHomeSceneAsync));
            });
        }



        private void AnimateButton(Transform button, Action onCompleteAction)
        {
            float currentScale = button.localScale.x;
            Vector2 upScale = new Vector2(currentScale + 0.1f, currentScale + 0.1f);
            Vector2 originalScale = Vector2.one * currentScale;

            button.DOScale(upScale, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                button.DOScale(originalScale, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    onCompleteAction?.Invoke();
                });
            });
        }




        private IEnumerator LoadHomeSceneAsync()
        {

            // Start loading the GameplayScreen scene
            AsyncOperation gameplaySceneOp = SceneManager.LoadSceneAsync(_homeSceneName);
            gameplaySceneOp.allowSceneActivation = false;

            // Update progress until the GameplayScreen is loaded
            while (!gameplaySceneOp.isDone)
            {
                float progress = Mathf.Clamp01(gameplaySceneOp.progress / 0.9f); // Normalize 0-0.9 to 0-1
                if (gameplaySceneOp.progress >= 0.9f)
                {
                    yield return new WaitForSeconds(1f); // Minimum 1-second display
                    gameplaySceneOp.allowSceneActivation = true;
                }
                yield return null;
            }

        }

    }
}

