using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTitle : MonoBehaviour
{
    // �Q�[���J�n�t���O
    private bool isStartMove = false;
    // �d�͕ӂɉ����鑬�x
    private float backSpeed = 1.2f;
    // �J�����̉��ӎ擾
    private CameraManager cameraManager;

    // �`���[�W��
    [SerializeField] private GameObject smokePrefab;
    private float smokeInterval = 0f;

    // �����G�t�F�N�g
    [SerializeField] private GameObject explosionCirclePrefab;

    void Start()
    {
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
    }

    void Update()
    {
        // �Q�[���J�n�t���O�𗧂Ă�
        if (!isStartMove && Input.GetKeyDown(KeyCode.Space))
        {
            isStartMove = true;
        }

        // �d�͕ӂɌ������ď������߂Â�
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
        // ���̃G�t�F�N�g���쐬����
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
