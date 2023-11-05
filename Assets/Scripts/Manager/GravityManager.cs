using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityManager : MonoBehaviour
{
    public enum GravityPattern
    {
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }
    public GravityPattern gravityPattern = GravityPattern.BOTTOM;

    public float gravityPower;
    public Vector2 gravity;

    // 同じ重力が続いている時間
    private float noChangeTime;
    // 重力を強くする時間間隔
    [SerializeField] private float changeInterval;

    // 重力レベル
    private int gravityLevel;
    [SerializeField] private GameObject gravityLevelObj;
    private Text gravityLevelText;
    private Animator gravityLevelAnimator;

    private GameManager gameManager;
    [SerializeField] private bool isTitleScene = false;

    void Start()
    {
        gravityPower = 0.2f;
        gravity = Vector2.zero;
        gravityLevel = 1;
        if (gravityLevelObj)
        {
            gravityLevelText = gravityLevelObj.GetComponent<Text>();
            gravityLevelAnimator = gravityLevelObj.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (!isTitleScene && !gameManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        }

        if (gameManager && !gameManager.GetIsStart())
        {
            gravity = Vector2.zero;
        }

        if (!isTitleScene && gameManager && gameManager.GetIsStart())
        {
            switch (gravityPattern)
            {
                case GravityPattern.LEFT:
                    gravity.x = -gravityPower;
                    gravity.y = 0f;
                    break;
                case GravityPattern.RIGHT:
                    gravity.x = gravityPower;
                    gravity.y = 0f;
                    break;
                case GravityPattern.TOP:
                    gravity.x = 0f;
                    gravity.y = gravityPower;
                    break;
                case GravityPattern.BOTTOM:
                    gravity.x = 0f;
                    gravity.y = -gravityPower;
                    break;
            }

            noChangeTime += Time.deltaTime;
            if (noChangeTime > changeInterval)
            {
                gravityPower *= 2f;
                gravityLevel++;
                noChangeTime = 0f;
                gravityLevelAnimator.SetTrigger("Scaling");
            }
        }
        else if (isTitleScene)
        {
            gravityPower = 2f;
            gravity.y = -gravityPower;
        }

        if (gravityLevelText)
        {
            gravityLevelText.text = "Lv." + gravityLevel.ToString();
        }
    }

    public void GravityInitialize(GravityPattern newPattern)
    {
        if (gravityPattern == newPattern)
        {
            // 制限時間を減らす
            gameManager.SubtractionOfTimeLimit(3f);
        }
        else
        {
            gravityPattern = newPattern;
        }
        noChangeTime = 0f;
        gravityLevel = 1;
        gravityPower = 0.2f;
        gravityLevelAnimator.SetTrigger("Scaling");
    }

    public int GetGravityLevel()
    {
        return gravityLevel;
    }
}
