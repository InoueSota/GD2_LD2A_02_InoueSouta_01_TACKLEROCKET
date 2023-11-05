using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMove playerMove;

    // 覐΂Ɏ�_�b�V�����ɏՓ˂�����m�b�N�o�b�N����
    public bool isKnockBack;
    // �m�b�N�o�b�N�C�[�W���O�ɕK�v�ȃp�����[�^
    [SerializeField] private float knockBackTime;
    private float knockBackLeftTime;
    private Vector3 knockBackStartPosition;
    private Vector3 knockBackEndPosition;
    [SerializeField] private float knockBackDistance;

    [SerializeField] private GameObject smokePrefab;

    [SerializeField] private ScoreManager scoreManager;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        knockBackLeftTime = knockBackTime;
    }

    void Update()
    {
        HitMeteorite();
    }

    void HitMeteorite()
    {
        if (isKnockBack)
        {
            knockBackLeftTime -= Time.deltaTime;
            if (knockBackLeftTime < 0f) { knockBackLeftTime = 0f; }
            float t = knockBackLeftTime / knockBackTime;
            transform.position = Vector3.Lerp(knockBackEndPosition, knockBackStartPosition, t * t);
            if (knockBackLeftTime == 0f) { isKnockBack = false; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteorite"))
        {
            if (playerMove.chargeCount == 0 && !collision.GetComponent<MeteoriteManager>().GetIsHitActive())
            {
                knockBackLeftTime = knockBackTime;
                knockBackStartPosition = transform.position;
                Vector3 direction = Vector3.Normalize(collision.transform.position - transform.position);
                knockBackEndPosition = knockBackStartPosition - direction * knockBackDistance;
                playerMove.HitMeteoriteInitialize();
                collision.GetComponent<MeteoriteManager>().HitPlayerInitialze(Vector3.Normalize(collision.transform.position - transform.position));

                // ���Ԓn�_�Ńp�[�e�B�N�����o��
                Vector3 particleCenterPosition = transform.position + direction * (Vector3.Distance(transform.position, collision.transform.position) * 0.5f);
                for (int i = 0; i < 8; i++)
                {
                    GameObject smoke = Instantiate(smokePrefab, new(particleCenterPosition.x + Random.Range(-0.15f, 0.15f), particleCenterPosition.y + Random.Range(-0.15f, 0.15f), particleCenterPosition.z), Quaternion.identity);
                    smoke.GetComponent<SmokeManager>().Initialize(particleCenterPosition, Random.Range(0.6f, 1f));
                }

                isKnockBack = true;
            }
            // �_�b�V����
            else
            {
                // �X�R�A�����Z����
                scoreManager.AddScore((int)(1000 * collision.transform.localScale.x));

                // �q�b�g�X�g�b�v
                HitStopManager.instance.StartHitStop(0.1f);

                collision.GetComponent<MeteoriteManager>().CreateExplosionEffect();
                Destroy(collision.gameObject);
            }
        }
    }
}
