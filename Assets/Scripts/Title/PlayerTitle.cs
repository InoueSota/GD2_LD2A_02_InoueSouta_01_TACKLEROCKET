using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTitle : MonoBehaviour
{
    // ゲーム開始フラグ
    private bool isStartMove = false;
    // 重力辺に下がる速度
    private float backSpeed = 1.2f;
    // カメラの下辺取得
    private CameraManager cameraManager;

    // チャージ煙
    [SerializeField] private GameObject smokePrefab;
    private float smokeInterval = 0f;

    // 爆発エフェクト
    [SerializeField] private GameObject explosionCirclePrefab;

    void Start()
    {
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    void Update()
    {
        // ゲーム開始フラグを立てる
        if (!isStartMove && Input.GetKeyDown(KeyCode.Space))
        {
            isStartMove = true;
        }

        // 重力辺に向かって少しずつ近づく
        if (isStartMove)
        {
            float deltaBackSpeed = backSpeed * Time.deltaTime;
            transform.position = new(transform.position.x, transform.position.y - deltaBackSpeed, transform.position.z);

            CheckBottom();
        }

        Smoke();
    }

    void CheckBottom()
    {
        if (cameraManager)
        {
            float cameraBottom = -cameraManager.halfHeight;
            float thisBottom = transform.position.y - transform.localScale.y * 0.5f;
            if (cameraBottom > thisBottom)
            {
                GameObject explosionCircle = Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void Smoke()
    {
        // 煙のエフェクトを作成する
        smokeInterval += Time.deltaTime;
        if (smokeInterval >= 0.05f)
        {
            GameObject smoke = Instantiate(smokePrefab, new(transform.position.x + Random.Range(-0.15f, 0.15f), transform.position.y - Random.Range(0.45f, 0.6f), transform.position.z), Quaternion.identity);
            smoke.transform.RotateAround(transform.position, Vector3.forward, 0f);
            if (Input.GetKey(KeyCode.Space))
            {
                smoke.GetComponent<SmokeManager>().Initialize(transform.position, 0.5f);
            }
            else
            {
                smoke.GetComponent<SmokeManager>().Initialize(transform.position, 1.5f);
            }
            smokeInterval = 0f;
        }
    }
}
