using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour
{
    private Scene scene;
    public Canvas canvas;


    void Awake()
    {
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        canvas = gameObject.GetComponent<Canvas>();

        canvas.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscapeKey();
        }
    }

    private void HandleEscapeKey()
    {
        if (canvas.enabled)
        {
            ResumeGame();

        }
        else
        {
            PauseGame();
        }
    }


    private void PauseGame()
    {
        canvas.enabled = true;
        EventManager.TriggerEvent(EventManager.onGamePaused);

    }
    private void ResumeGame()
    {
        canvas.enabled = false;
        EventManager.TriggerEvent(EventManager.onGameResumed);
    }


    

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        EventManager.TriggerEvent(EventManager.onGameResumed);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(scene.name);
        EventManager.TriggerEvent(EventManager.onGameResumed);
    }
}
