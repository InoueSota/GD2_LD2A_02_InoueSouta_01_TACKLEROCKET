using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTitle : MonoBehaviour
{
    private Animator animator;

    private bool change = false;
    private float changeTimeElapse;
    [SerializeField] private float changeTime;
    [SerializeField] private GameObject fadeInObj;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!change && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("zoom", true);
            changeTimeElapse = 0f;
            change = true;
        }

        if (change)
        {
            changeTimeElapse += Time.deltaTime;
            if (changeTimeElapse > changeTime)
            {
                fadeInObj.SetActive(true);
            }
        }
    }
}
