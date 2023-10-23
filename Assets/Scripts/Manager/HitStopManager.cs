using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    // どこからでも呼び出せるようにする
    public static HitStopManager instance;

    private void Start()
    {
        instance = this;
    }

    // ヒットストップを開始する関数
    public void StartHitStop(float duration)
    {
        instance.StartCoroutine(instance.HitStopCoroutine(duration));
    }

    // コルーチンの内容
    private IEnumerator HitStopCoroutine(float duration)
    {
        // ヒットストップの開始
        Time.timeScale = 0f;

        // 指定した時間だけ停止
        yield return new WaitForSecondsRealtime(duration);

        // ヒットストップの終了
        Time.timeScale = 1f;
    }
}
