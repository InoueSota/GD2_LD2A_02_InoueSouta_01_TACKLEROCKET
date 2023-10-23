using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParticleManager : MonoBehaviour
{
    // �����������
    [SerializeField] private GameObject basePrefab;

    // �����Ԋu
    [SerializeField] private float interval;
    private float makeTimer;

    // ��ʓ������_������������
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
            GameObject newObj = Instantiate(basePrefab, new(Random.Range(-cameraManager.halfWidth, cameraManager.halfWidth), Random.Range(-cameraManager.halfHeight, cameraManager.halfHeight), 0f), Quaternion.identity);
            newObj.GetComponent<StarManager>().Create();
            makeTimer = 0f;
        }
    }
}
