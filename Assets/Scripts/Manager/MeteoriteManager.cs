using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    // 弱ダッシュ時のプレイヤーに衝突後はプレイヤーの進行方向に吹っ飛ばされる
    private bool isHitActive;
    [SerializeField] private float hitSpeed;
    private Vector3 direction;

    private Gravity gravity;

    // 爆発エフェクト
    [SerializeField] private GameObject explosionCirclePrefab;
    [SerializeField] private GameObject explosionStarPrefab;

    void Start()
    {
        gravity = GetComponent<Gravity>();
    }

    void Update()
    {
        if (isHitActive)
        {
            Vector3 deltaMove = direction * hitSpeed * Time.deltaTime;
            transform.position += deltaMove;
        }
    }

    public void HitPlayerInitialze(Vector3 direction_)
    {
        direction = direction_;
        gravity.enabled = false;
        isHitActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HitMeteorite(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HitMeteorite(collision);
    }

    void HitMeteorite(Collider2D collision)
    {
        if (isHitActive && collision.CompareTag("Meteorite"))
        {
            Vector3 newScale = Vector3.one;
            if (transform.localScale.x > collision.gameObject.transform.localScale.x)
            {
                newScale = transform.localScale;
            }
            else
            {
                newScale = collision.gameObject.transform.localScale;
            }
            collision.gameObject.transform.localScale = newScale + Vector3.one * 0.2f;
            CreateExplosionEffect();
            Destroy(gameObject);
        }
    }

    public void CreateExplosionEffect()
    {
        GameObject explosionCircle = Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity);
    }
}
