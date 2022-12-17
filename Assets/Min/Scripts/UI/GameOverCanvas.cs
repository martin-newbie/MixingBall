using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameOverCanvas : MonoBehaviour
{
    public Image background;
    public Text resultText;
    public Text scoreText;

    public Button restartBtn;
    public Button mainBtn;

    public void GameOver(float score)
    {
        gameObject.SetActive(true);

        background.DOColor(Color.black, 0.25f).SetDelay(1f).OnComplete(() =>
        {
            resultText.DOText("Result", 0.5f).OnComplete(() =>
            {
                resultText.rectTransform.DOAnchorPosY(530, 0.5f).SetEase(Ease.OutQuint).OnComplete(() =>
                {
                    StartCoroutine(scoreCount(2f, score));
                });
            });
        });


        IEnumerator scoreCount(float duration, float score)
        {
            float timer = 0f;
            float offset = score / duration;
            float value = 0f;

            while (timer <= duration)
            {
                if (score <= 0f) break;

                value += offset * Time.deltaTime;
                scoreText.text = string.Format("{0:#,##0}", value);

                timer += Time.deltaTime;
                yield return null;
            }
            scoreText.text = string.Format("{0:#,##0}", score);

            yield return new WaitForSeconds(1f);

            restartBtn.GetComponent<RectTransform>().DOAnchorPosX(-150f, 0.5f).SetEase(Ease.OutCubic);
            mainBtn.GetComponent<RectTransform>().DOAnchorPosX(150f, 0.5f).SetEase(Ease.OutCubic);

            yield break;
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene("InGame");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }

}
