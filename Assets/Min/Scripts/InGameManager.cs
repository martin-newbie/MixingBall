using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    RED,
    BLUE,
    GREEN,
    MAGENTA,
    YELLOW,
    TEAL,
    WHITE,
    BLACK
}

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance = null;
    public static InGameManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public MainCircle mainCircle;
    public HpCanvas hpCanvas;
    public GameOverCanvas gameoverCanvas;
    public Ball ballPrefab;

    public ParticleSystem ballEffect;

    public bool isGameActive;
    bool red, blue, green;
    float score;

    float ballDuration = 4f;

    Coroutine mainLogic;

    void Start()
    {
        mainLogic = StartCoroutine(GameMainLogic());
    }

    public float curHp;

    public HpCanvas SetGauge(float fill, bool isHp = false)
    {
        return hpCanvas.SetHPGauge(fill, isHp);
    }
    public float decreasingSpeed => (((int)(score / 30f) + 1) * 1f) * Time.deltaTime;
    IEnumerator GameMainLogic()
    {
        red = false;
        blue = false;
        green = false;

        List<bool> randList;

        float timer = 0f;
        while (timer <= 100f)
        {
            SetGauge(timer / 100f);
            timer += (100f / 3f) * Time.deltaTime;
            yield return null;
        }

        yield return null;
        isGameActive = true;
        mainCircle.ChangeColor(Color.white);
        curHp = 100f;
        float delay = 0f;

        while (isGameActive)
        {
            if (!mainCircle.isFever)
                SetGauge(curHp / 100f);

            delay += Time.deltaTime;

            if (getBallSpawnChance() && delay >= 0.1f)
            {
                delay = 0f;

                randList = new List<bool>(3)
                {
                    true,
                    (bool)ChooseOne(true, false),
                    (bool)ChooseOne(true, false),
                };

                var combined = GetCombinedColor(randBool(randList), randBool(randList), randBool(randList));
                SpawnBall(combined, (bool)ChooseOne(true, false));
            }

            score += Time.deltaTime;

            if (!mainCircle.isFever)
                curHp -= decreasingSpeed;

            if (curHp > 100f) curHp = 100f;

            UIManager.Instance.ChangeScore(score);

            if (curHp <= 0) break;
            yield return null;
        }

        mainCircle.GameOver();
        gameoverCanvas.GameOver(score);
        removeAllBalls();

        yield break;

        void removeAllBalls()
        {
            var balls = FindObjectsOfType<Ball>();
            foreach (var item in balls)
            {
                Destroy(item.gameObject);
            }
        }
        bool getBallSpawnChance()
        {
            return Random.Range(0f, 100f) <= 3f;
        }
        bool randBool(List<bool> list)
        {
            int rand = Random.Range(0, list.Count);
            bool result = list[rand];
            list.RemoveAt(rand);

            return result;
        }
    }

    public void ChangeColor(bool _red, bool _blue, bool _green)
    {
        if (_red) red = !red;
        if (_blue) blue = !blue;
        if (_green) green = !green;

        mainCircle.ChangeColor(GetCombinedColor(red, blue, green));
    }

    Ball SpawnBall(Color color, bool isHorizontal)
    {
        if (color == Color.black) return null;

        float yHalf = Camera.main.orthographicSize;
        float xHalf = yHalf * Camera.main.aspect;
        Vector2 spawnPos;

        if (isHorizontal)
        {
            spawnPos = new Vector2(Random.Range(-xHalf, xHalf), (float)ChooseOne(-yHalf, yHalf));
        }
        else
        {
            spawnPos = new Vector2((float)ChooseOne(-xHalf, xHalf), Random.Range(-yHalf, yHalf));
        }

        float degree = Mathf.Atan2(spawnPos.y, spawnPos.x) * (180f / Mathf.PI);
        float cos = Mathf.Cos(degree * Mathf.Deg2Rad);
        float sin = Mathf.Sin(degree * Mathf.Deg2Rad);
        Vector2 addPos = new Vector2(cos * 1.25f, sin * 1.25f);

        Ball ballTmp = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        ballTmp.Init(mainCircle.transform.position + (Vector3)addPos, color, ballDuration);
        return ballTmp;

    }
    object ChooseOne(params object[] contents)
    {
        int rand = Random.Range(0, contents.Length);
        return contents[rand];
    }
    Color GetCombinedColor(bool _red, bool _blue, bool _green)
    {
        Color result = Color.black;

        result += _red ? Color.red : Color.black;
        result += _blue ? Color.blue : Color.black;
        result += _green ? Color.green : Color.black;
        result.a = 1f;

        if (result == Color.black) return Color.white;

        return result;
    }
}
