using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance { get; private set; }
    public bool canProgress = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        Progress();
    }

    private void Progress()
    {
        if (canProgress)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                LoadNextScene();
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitDesktopApp()
    {
        Application.Quit();
    }
}
