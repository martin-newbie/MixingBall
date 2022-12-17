using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBallsEffect : MonoBehaviour
{
    public void Init()
    {
        transform.localScale = Vector2.zero;
        transform.DOScale(2f, 1f).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            PlayEffect(collision.GetComponent<Ball>());
            Destroy(collision.gameObject);
        }

        void PlayEffect(Ball ball)
        {
            var effect = Instantiate(InGameManager.Instance.ballEffect);
            effect.transform.position = ball.transform.position;
            var main = effect.main;

            main.startColor = ball.thisColor;
            effect.Play();

            Destroy(effect.gameObject, 1f);
        }
    }
}
