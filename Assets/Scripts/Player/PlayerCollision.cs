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
            if (playerMove.chargeCount == 0)
            {
                knockBackLeftTime = knockBackTime;
                knockBackStartPosition = transform.position;
                Vector3 direction = Vector3.Normalize(collision.transform.position - transform.position) * knockBackDistance;
                knockBackEndPosition = knockBackStartPosition - direction;
                playerMove.HitMeteoriteInitialize();
                collision.GetComponent<MeteoriteManager>().HitPlayerInitialze(Vector3.Normalize(collision.transform.position - transform.position));
                isKnockBack = true;
            }
            // �_�b�V����
            else
            {
                // �q�b�g�X�g�b�v
                HitStopManager.instance.StartHitStop(0.1f);

                collision.GetComponent<MeteoriteManager>().CreateExplosionEffect();
                Destroy(collision.gameObject);
            }
        }
    }
}
