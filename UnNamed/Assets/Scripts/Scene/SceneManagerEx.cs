using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : IManager
{
    public int SceneIndex = 0;

    public void Clear()
    {

    }

    public void Init()
    {

    }

    public void OnUpdate()
    {

    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
