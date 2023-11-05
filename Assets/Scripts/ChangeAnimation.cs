using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimation : MonoBehaviour
{
    private float timeElapse;
    [SerializeField] private float changeTime;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        timeElapse = 0f;
    }

    void Update()
    {
        timeElapse += Time.deltaTime;

        if (timeElapse > changeTime)
        {
            animator.SetBool("next", true);
        }
    }
}
