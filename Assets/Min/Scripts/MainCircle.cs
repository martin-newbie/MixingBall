using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCircle : MonoBehaviour
{

    [SerializeField] ParticleSystem pointEffect;
    [SerializeField] ParticleSystem ringEffect;

    SpriteRenderer sprite;
    Animator anim;

    Color curColor;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void ChangeColor(Color color)
    {
        curColor = color;

        CirclePop();
        sprite.color = curColor;
    }

    public void CirclePop()
    {
        anim.SetTrigger("pop");
    }

    public void GameOver()
    {
        StartCoroutine(spriteFadeout(2f));
        PointEffectColor(Color.black);

        IEnumerator spriteFadeout(float duration)
        {
            float timer = 0f;
            Vector2 originSize = sprite.transform.localScale;

            while (timer <= duration)
            {
                sprite.transform.localScale = Vector2.Lerp(originSize, Vector2.zero, 1 - Mathf.Pow(1 - (timer / duration), 5));

                timer += Time.deltaTime;
                yield return null;
            }
        }
    }

    int comboCount = 0;
    bool isCombo = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {

            if(collision.GetComponent<Ball>().thisColor != curColor)
            {
                comboCount = 0;
                isCombo = false;
                InGameManager.Instance.curHp -= 1f;
                return;
            }

            comboCount++;
            isCombo = true;

            InGameManager.Instance.GetScore(5);
            PlayEffect();
        }


        void PlayEffect()
        {
            CirclePop();

            PointEffectColor(curColor);
        }

    }


    void PointEffectColor(Color targetColor)
    {
        var pointMain = pointEffect.main;
        var ringMain = ringEffect.main;

        pointMain.startColor = targetColor;
        ringMain.startColor = targetColor;

        pointEffect.Play();
        ringEffect.Play();
    }
}
