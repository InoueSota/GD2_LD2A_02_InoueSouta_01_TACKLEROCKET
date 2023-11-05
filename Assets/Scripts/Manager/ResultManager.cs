using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    // 全ての文字をフェードインで出す
    [SerializeField] private GameObject resultText;
    private Animator resultAnimator;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject scoreNumberText;
    private Text scoreNumber;
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject retryText;

    // 時間を計測する
    private float timeElapse;

    // スコア
    private float scoreTime;
    private float scoreLeftTime;
    private int endScore;

    // シーン遷移先指定
    [SerializeField] private GameObject fadeInObj;
    private ChangeScene changeScene;
    [SerializeField] private GameObject selectAnimationObj;
    enum SceneName
    {
        TITLE,
        RETRY
    }
    SceneName sceneName = SceneName.RETRY;

    void Start()
    {
        resultAnimator = resultText.GetComponent<Animator>();
        scoreNumber = scoreNumberText.GetComponent<Text>();
        scoreTime = 3f;
        scoreLeftTime = scoreTime;
        endScore = ScoreManager.score;
        changeScene = fadeInObj.GetComponent<ChangeScene>();
    }

    void Update()
    {
        timeElapse += Time.deltaTime;

        if (timeElapse > 6f)
        {
            switch (sceneName)
            {
                case SceneName.TITLE:
                    if (timeElapse > 6.3f)
                    {
                        selectAnimationObj.SetActive(true);
                        selectAnimationObj.transform.position = new(0f, titleText.transform.position.y + 0.1f, 0f);
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        changeScene.ChangeSceneName("TitleScene");
                        fadeInObj.SetActive(true);
                    }
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        sceneName = SceneName.RETRY;
                    }
                    break;
                case SceneName.RETRY:
                    if (timeElapse > 6.3f)
                    {
                        selectAnimationObj.SetActive(true);
                        selectAnimationObj.transform.position = new(0f, retryText.transform.position.y + 0.1f, 0f);
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        changeScene.ChangeSceneName("GameScene");
                        fadeInObj.SetActive(true);
                    }
                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        sceneName = SceneName.TITLE;
                    }
                    break;
            }
            titleText.SetActive(true);
            retryText.SetActive(true);
        }
        else if (timeElapse > 2f)
        {
            scoreText.SetActive(true);
            scoreNumberText.SetActive(true);

            scoreLeftTime -= Time.deltaTime;
            if (scoreLeftTime < 0) { scoreLeftTime = 0f; }
            float t = scoreLeftTime / scoreTime;
            int displayScore = (int)Mathf.Lerp(endScore, 0, t * t * t);
            scoreNumber.text = displayScore.ToString("D8");
        }
        else if (timeElapse > 1f)
        {
            resultAnimator.SetBool("Move", true);
        }
    }
}
