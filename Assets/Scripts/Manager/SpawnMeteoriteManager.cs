using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteoriteManager : MonoBehaviour
{
    private GravityManager gravityManager;
    private CameraManager cameraManager;

    [SerializeField] private GameObject meteoritePrefab;
    [SerializeField] private float makeIntervalMin;
    [SerializeField] private float makeIntervalMax;
    private float makeInterval;
    private float makeTimer;

    void Start()
    {
        gravityManager = GameObject.FindGameObjectWithTag("Gravity").GetComponent<GravityManager>();
        cameraManager = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        makeInterval = Random.Range(makeIntervalMin, makeIntervalMax);
    }

    void Update()
    {
        makeTimer += Time.deltaTime;
        if (makeTimer > makeInterval)
        {
            if (gravityManager.gravityPattern == GravityManager.GravityPattern.LEFT)
            {
                GameObject meteorite = Instantiate(meteoritePrefab, new(0f, 0f, 0f), Quaternion.identity);
                meteorite.transform.position = new(cameraManager.halfWidth + meteorite.transform.localScale.x * 0.5f, Random.Range(-cameraManager.halfHeight + meteorite.transform.localScale.y * 0.5f, cameraManager.halfHeight - meteorite.transform.localScale.y * 0.5f));
            }
            else if (gravityManager.gravityPattern == GravityManager.GravityPattern.RIGHT)
            {
                GameObject meteorite = Instantiate(meteoritePrefab, new(0f, 0f, 0f), Quaternion.identity);
                meteorite.transform.position = new(-cameraManager.halfWidth - meteorite.transform.localScale.x * 0.5f, Random.Range(-cameraManager.halfHeight + meteorite.transform.localScale.y * 0.5f, cameraManager.halfHeight - meteorite.transform.localScale.y * 0.5f));
            }
            else if (gravityManager.gravityPattern == GravityManager.GravityPattern.TOP)
            {
                GameObject meteorite = Instantiate(meteoritePrefab, new(0f, 0f, 0f), Quaternion.identity);
                meteorite.transform.position = new(Random.Range(-cameraManager.halfWidth + meteorite.transform.localScale.x * 0.5f, cameraManager.halfWidth - meteorite.transform.localScale.x * 0.5f), -cameraManager.halfHeight - meteorite.transform.localScale.y * 0.5f);
            }
            else if (gravityManager.gravityPattern == GravityManager.GravityPattern.BOTTOM)
            {
                GameObject meteorite = Instantiate(meteoritePrefab, new(0f, 0f, 0f), Quaternion.identity);
                meteorite.transform.position = new(Random.Range(-cameraManager.halfWidth + meteorite.transform.localScale.x * 0.5f, cameraManager.halfWidth - meteorite.transform.localScale.x * 0.5f), cameraManager.halfHeight + meteorite.transform.localScale.y * 0.5f);
            }
            makeInterval = Random.Range(makeIntervalMin, makeIntervalMax);
            makeTimer = 0f;
        }
    }
}
