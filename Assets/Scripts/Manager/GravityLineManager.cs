using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLineManager : MonoBehaviour
{
    // �c�ɐL�тĂ���d�͐����ǂ���
    [SerializeField] private bool isVertical;
    // �o�����G�t�F�N�g
    [SerializeField] private GameObject lineEffectObj;

    public void Create()
    {
        if (isVertical)
        {
            GameObject line = Instantiate(lineEffectObj, new(transform.position.x, 0f, transform.position.z), Quaternion.Euler(new Vector3(0f, 0f, 90f)));
        }
        else
        {
            GameObject line = Instantiate(lineEffectObj, new(0f, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
