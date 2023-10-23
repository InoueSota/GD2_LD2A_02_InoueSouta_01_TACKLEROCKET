using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNumber : MonoBehaviour
{
    public Text scoreText;
    public float score;

    void Start()
    {
        score = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().halfWidth;
    }

    void Update()
    {
        if (score < 10)
        {
            scoreText.text = string.Format("{0:0}", score);
        }
        else if (score < 100)
        {
            scoreText.text = string.Format("{0:00}", score);
        }
        else if (score < 1000)
        {
            scoreText.text = string.Format("{0:000}", score);
        }
        else if (score < 10000)
        {
            scoreText.text = string.Format("{0:0000}", score);
        }
        else if (score < 100000)
        {
            scoreText.text = string.Format("{0:00000}", score);
        }
    }

    public void ChangeScore(float changeValue)
    {
        score = changeValue;
    }

}
