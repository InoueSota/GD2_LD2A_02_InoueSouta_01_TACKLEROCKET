using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float timeElapse = 0f;
    [SerializeField] private float cutInObjCreateTime;

    // Startアニメーション
    private bool isCreateCutIn = false;
    [SerializeField] private GameObject cutInPrefab;
    [SerializeField] private GameObject startText;

    // Finishアニメーション
    [SerializeField] private GameObject finishText;
    [SerializeField] private GameObject fadeInObj;

    // 重力のレベル表記
    [SerializeField] private GameObject gravityLevelText;

    // 開始時間
    [SerializeField] private float startTime;
    // ゲームを始めるか
    private bool isStart = false;
    // ゲームを終わらせるか
    private bool isFinish = false;

    // 制限時間
    [SerializeField] private GameObject timeLimitObj;
    private Text timeLimitText;
    private Animator timeLimitAnimator;
    private float timeLimit;

    // スコア
    [SerializeField] private GameObject scoreLetterObj;
    [SerializeField] private GameObject scoreNumberObj;
    // コンボ
    [SerializeField] private GameObject comboSliderObj;

    void Start()
    {
        timeLimitText = timeLimitObj.GetComponent<Text>();
        timeLimitAnimator = timeLimitObj.GetComponent<Animator>();
        timeLimit = 90f;
    }

    void Update()
    {
        if (!isStart)
        {
            timeElapse += Time.deltaTime;

            if (!isFinish)
            {
                if (!isCreateCutIn && timeElapse > cutInObjCreateTime)
                {
                    startText.SetActive(true);
                    GameObject cutIn = Instantiate(cutInPrefab, new(0f, 0f, 0f), Quaternion.identity);
                    isCreateCutIn = true;
                }

                if (timeElapse > startTime - 0.5f)
                {
                    startText.SetActive(false);
                    gravityLevelText.SetActive(true);
                    timeLimitObj.SetActive(true);
                    scoreLetterObj.SetActive(true);
                    scoreNumberObj.SetActive(true);
                    comboSliderObj.SetActive(true);
                }
                if (timeElapse > startTime)
                {
                    isCreateCutIn = false;
                    isStart = true;
                    timeElapse = 0f;
                }
            }
            else
            {
                if (timeElapse > startTime - 2f)
                {
                    finishText.SetActive(false);
                    fadeInObj.SetActive(true);
                }
            }
        }
        // インゲーム
        else
        {
            timeLimit -= Time.deltaTime;
            timeLimit = Mathf.Clamp(timeLimit, 0f, 90f);
            int tmpTimeLimit = (int)Mathf.Ceil(timeLimit);
            timeLimitText.text = tmpTimeLimit.ToString();

            // ゲーム終了
            if (timeLimit <= 0f)
            {
                timeLimit = 0f;
                if (!isCreateCutIn)
                {
                    GameObject cutIn = Instantiate(cutInPrefab, new(0f, 0f, 0f), Quaternion.identity);
                    finishText.SetActive(true);
                    isCreateCutIn = true;
                }
                isStart = false;
                isFinish = true;
            }
        }
    }

    public void SubtractionOfTimeLimit(float subtractValue)
    {
        timeLimit -= subtractValue;
        timeLimit = Mathf.Clamp(timeLimit, 0f, 90f);
        timeLimitAnimator.SetTrigger("Scaling");
    }

    public bool GetIsStart()
    {
        return isStart;
    }
}
