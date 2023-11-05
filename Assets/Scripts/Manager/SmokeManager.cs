using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour
{
    // ˆÚ“®•ûŒü
    private Vector3 direction = Vector3.zero;
    // ˆÚ“®‘¬“x
    private float speed = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Initialize(Vector3 playerPosition, float minSpeed)
    {
        direction = Vector3.Normalize(transform.position - playerPosition);
        speed = Random.Range(minSpeed, minSpeed + 0.5f);
    }
}
