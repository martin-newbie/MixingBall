using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ItemType
{
    BARRIER,
    FEVER,
    BOMB,
}

public class MainCircle : MonoBehaviour
{

    [SerializeField] ParticleSystem pointEffect;
    [SerializeField] ParticleSystem ringEffect;
    [SerializeField] ParticleSystem barrierEffect;
    [SerializeField] ParticleSystem ignoreEffect;

    [SerializeField] ClearBallsEffect clearBalls;

    SpriteRenderer sprite;
    Animator anim;

    Color curColor;
    public Color[] randomColors;

    [SerializeField] AudioClip ball_clip;
    [SerializeField] AudioClip shield_clip;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void ChangeColor(Color color)
    {
        curColor = color;
        if (isFever) return;

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
    bool isBarrier = false;
    [HideInInspector] public bool isFever = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            StartCoroutine(BarrierTime());
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(FeverTime());
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ClearAllBalls();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            if (isFever)
            {
                getBall();
                return;
            }

            if (collision.GetComponent<Ball>().thisColor != curColor)
            {
                if (isBarrier)
                {
                    playIgnoreEffect(collision.GetComponent<Ball>().thisColor);
                    getHp();
                    return;
                }

                comboCount = 0;
                isCombo = false;
                getHp();
                return;
            }

            getBall();

            if (isRandomEvent(20f))
                StartCoroutine(BarrierTime());
            else if (isRandomEvent(5f))
                StartCoroutine(FeverTime());
            else if (isRandomEvent(10f))
                ClearAllBalls();
        }

        bool isRandomEvent(float chance)
        {
            return Random.Range(0f, 100f) < chance;
        }
        void playIgnoreEffect(Color color)
        {
            var main = ignoreEffect.main;
            main.startColor = color;

            ignoreEffect.Play();
        }
        void playEffect()
        {
            CirclePop();

            if (!isFever)
                PointEffectColor(curColor);
            else
                PointEffectColor(sprite.color);
        }
        void getBall()
        {
            SoundManager.instance.SFXPlay("ball", ball_clip);
            comboCount++;
            isCombo = true;
            playEffect();
        }
        void getHp()
        {
            InGameManager.Instance.curHp += InGameManager.Instance.decreasingSpeed / 3f;
        }
    }

    IEnumerator BarrierTime()
    {
        SoundManager.instance.SFXPlay("shield", shield_clip);
        isBarrier = true;

        barrierEffect.Play();
        yield return new WaitForSeconds(0.6f);

        isBarrier = false;
        yield return null;
    }

    IEnumerator FeverTime()
    {
        isFever = true;

        sprite.color = Color.black;

        float timer, duration;
        timer = duration = 5f;

        while (timer >= 0f)
        {
            InGameManager.Instance.SetGauge(timer / duration, true).SetGaugeColor(randomColors[Random.Range(0, randomColors.Length)]);
            timer -= Time.deltaTime;
            yield return null;
        }
        isFever = false;

        ChangeColor(curColor);
        yield break;
    }

    void ClearAllBalls()
    {
        var effect = Instantiate(clearBalls, Vector3.zero, Quaternion.identity);
        effect.Init();
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
