using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParticleManager : MonoBehaviour
{
    // 生成するもの
    [SerializeField] private GameObject basePrefab;

    // 生成間隔
    [SerializeField] private float interval;
    private float makeTimer;

    // 画面内ランダム発生させる
    private CameraManager cameraManager;

    void Start()
    {
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    void Update()
    {
        makeTimer += Time.deltaTime;
        if (makeTimer > interval)
        {
            GameObject newObj = Instantiate(basePrefab, new(Random.Range(-cameraManager.halfWidth - 1.5f, cameraManager.halfWidth + 1.5f), Random.Range(-cameraManager.halfHeight - 1.5f, cameraManager.halfHeight + 1.5f), 0f), Quaternion.identity);
            newObj.GetComponent<StarManager>().Create();
            makeTimer = 0f;
        }
    }
}
