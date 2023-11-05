using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParticleManager : MonoBehaviour
{
    // ¶¬‚·‚é‚à‚Ì
    [SerializeField] private GameObject basePrefab;

    // ¶¬ŠÔŠu
    [SerializeField] private float interval;
    private float makeTimer;

    // ‰æ–Ê“àƒ‰ƒ“ƒ_ƒ€”­¶‚³‚¹‚é
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
