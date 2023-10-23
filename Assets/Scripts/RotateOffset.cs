using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOffset : MonoBehaviour
{
    Vector3 def;

    void Start()
    {
        def = transform.localRotation.eulerAngles;
    }

    void LateUpdate()
    {
        transform.localRotation = Quaternion.Euler(def - transform.parent.localRotation.eulerAngles);
    }
}
