using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    void Change()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void ChangeSceneName(string changeName)
    {
        nextSceneName = changeName;
    }
}
