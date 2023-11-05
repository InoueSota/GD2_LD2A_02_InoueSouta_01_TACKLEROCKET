using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // �X�R�A
    private Text scoreText;
    public static int score;

    // �R���{
    [SerializeField] private Slider comboSlider;
    private float comboLeftTime;
    private float comboDuration = 8f;
    private int comboMagnification;

    // �d��
    private GravityManager gravityManager;

    void Start()
    {
        scoreText = GetComponent<Text>();
        comboMagnification = 0;
        score = 0;
        gravityManager = GameObject.FindGameObjectWithTag("Gravity").GetComponent<GravityManager>();
    }

    void Update()
    {
        // �X�R�A
        scoreText.text = score.ToString("D8");

        // �R���{
        comboLeftTime -= Time.deltaTime;
        comboSlider.value = comboLeftTime / comboDuration;
        if (comboLeftTime < 0f)
        {
            comboMagnification = 0;
        }
    }

    public void AddScore(int addValue)
    {
        score += (int)((float)addValue * (1 + comboMagnification) * (1 + (gravityManager.GetGravityLevel() - 1) * 0.1f));

        if (comboLeftTime > 0f)
        {
            comboMagnification++;
        }
        comboLeftTime = comboDuration;
        comboSlider.value = comboLeftTime / comboDuration;
    }
}
