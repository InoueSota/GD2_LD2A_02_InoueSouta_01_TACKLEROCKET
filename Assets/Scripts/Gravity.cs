using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public GravityManager gravityManager;
    [SerializeField] private bool isMeteorite;

    void Awake()
    {
        gravityManager = GameObject.FindGameObjectWithTag("Gravity").GetComponent<GravityManager>();
    }

    void Update()
    {
        if (isMeteorite )
        {
            Vector2 deltaGravity = gravityManager.gravity * 3f * Time.deltaTime;
            transform.position = new(transform.position.x + deltaGravity.x, transform.position.y + deltaGravity.y, transform.position.z);
        }
        else
        {
            Vector2 deltaGravity = gravityManager.gravity * Time.deltaTime;
            transform.position = new(transform.position.x + deltaGravity.x, transform.position.y + deltaGravity.y, transform.position.z);
        }
    }
}
