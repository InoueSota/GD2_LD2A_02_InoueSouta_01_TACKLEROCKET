using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    // 弱ダッシュ時のプレイヤーに衝突後はプレイヤーの進行方向に吹っ飛ばされる
    private bool isHitActive;
    [SerializeField] private float hitSpeed;
    private Vector3 direction;

    // 重力関係
    private GravityManager gravityManager;
    private Gravity gravity;
    private CameraManager cameraManager;
    private ScoreManager scoreManager;

    // 爆発エフェクト
    [SerializeField] private GameObject explosionCirclePrefab;

    private GameManager gameManager;

    void Start()
    {
        gravityManager = GameObject.FindGameObjectWithTag("Gravity").GetComponent<GravityManager>();
        gravity = GetComponent<Gravity>();
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager && gameManager.GetIsStart())
        {
            // 重力辺に当たったら消滅させ、スコアを加算する
            float cameraLeft = -cameraManager.halfWidth;
            float cameraRight = cameraManager.halfWidth;
            float cameraTop = cameraManager.halfHeight;
            float cameraBottom = -cameraManager.halfHeight;
            switch (gravityManager.gravityPattern)
            {
                case GravityManager.GravityPattern.LEFT:
                    float thisLeft = transform.position.x - transform.localScale.x * 0.5f;
                    if (cameraLeft > thisLeft)
                    {
                        DestroySelf();
                    }
                    break;
                case GravityManager.GravityPattern.RIGHT:
                    float thisRight = transform.position.x + transform.localScale.x * 0.5f;
                    if (cameraRight < thisRight)
                    {
                        DestroySelf();
                    }
                    break;
                case GravityManager.GravityPattern.TOP:
                    float thisTop = transform.position.y + transform.localScale.y * 0.5f;
                    if (cameraTop < thisTop)
                    {
                        DestroySelf();
                    }
                    break;
                case GravityManager.GravityPattern.BOTTOM:
                    float thisBottom = transform.position.y - transform.localScale.y * 0.5f;
                    if (cameraBottom > thisBottom)
                    {
                        DestroySelf();
                    }
                    break;
            }

            if (isHitActive)
            {
                Vector3 deltaMove = direction * hitSpeed * Time.deltaTime;
                transform.position += deltaMove;
            }

            if (Vector3.Distance(transform.position, new(0f, 0f, transform.position.z)) > 10f)
            {
                Destroy(gameObject);
            }
        }
    }

    void DestroySelf()
    {
        scoreManager.AddScore(100 * (int)transform.localScale.x);
        CreateExplosionEffect();
        Destroy(gameObject);
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

    public bool GetIsHitActive()
    {
        return isHitActive;
    }

    public void CreateExplosionEffect()
    {
        GameObject explosionCircle = Instantiate(explosionCirclePrefab, transform.position, Quaternion.identity);
    }
}
