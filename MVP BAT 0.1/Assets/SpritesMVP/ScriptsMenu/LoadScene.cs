﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{

    Scene Men;

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadAdd(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        Men = SceneManager.GetActiveScene();
        Invoke("Unload", 8.5f);
    }

    public void Unload()
    {
        SceneManager.UnloadSceneAsync(Men);
    }
}