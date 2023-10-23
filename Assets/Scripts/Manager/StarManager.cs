using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    private float spawnLeftTime;
    [SerializeField] private float baseScale;
    [SerializeField] private float intervalTime;
    [SerializeField] private float disapperTime;
    private float disapperLeftTime;

    void Update()
    {
        if (spawnLeftTime > 0)
        {
            spawnLeftTime -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(
                new(baseScale, baseScale, baseScale),
                Vector3.zero,
                spawnLeftTime / spawnTime
            );
        }
        else if (spawnLeftTime <= 0 && intervalTime > 0)
        {
            intervalTime -= Time.deltaTime;
        }
        else if (intervalTime <= 0)
        {
            disapperLeftTime -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(
                Vector3.zero,
                new(baseScale, baseScale, baseScale),
                disapperLeftTime / disapperTime
            );
            if (disapperLeftTime <= 0) { Destroy(gameObject); }
        }
    }

    public void Create()
    {
        spawnLeftTime = spawnTime;
        disapperLeftTime = disapperTime;
        transform.localScale = Vector3.zero;
    }
}
