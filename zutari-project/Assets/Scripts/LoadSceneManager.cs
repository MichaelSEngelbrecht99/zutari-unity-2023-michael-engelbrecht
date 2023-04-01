using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    #region Scene Loading Methods

    // Quits app if user presses ESC
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    // Loads a scene based on Index, assuming that scene is added into build settings
    public void LoadSceneWithIndex(int _sceneIndex)
    {
       SceneManager.LoadScene(_sceneIndex);
    }

    // Loads a scene based on Scene Name, assuming that scene is added into build settings
    public void LoadSceneWithString(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Quits app
    public void ExitApplication()
    {
        Application.Quit();
    }

    #endregion
}
