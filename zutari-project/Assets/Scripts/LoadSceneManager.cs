using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Loads a scene based on Index in build settings
    public void LoadSceneWithIndex(int _sceneIndex)
    {
       SceneManager.LoadScene(_sceneIndex);
    }

    // Loads a scene based on Scene Name
    public void LoadSceneWithString(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}