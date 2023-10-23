using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLineManager : MonoBehaviour
{
    // 縦に伸びている重力線かどうか
    [SerializeField] private bool isVertical;
    // 出現時エフェクト
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
