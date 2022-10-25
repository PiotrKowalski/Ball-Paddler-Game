using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseStateController : MonoBehaviour
{

    private static PauseStateController pauseStateController;

    private UnityAction onGamePausedListener;
    private UnityAction onGameResumedListener;


    public static PauseStateController Instance
    {
        get
        {
            if (!pauseStateController)
            {
                pauseStateController = FindObjectOfType(typeof(PauseStateController)) as PauseStateController;

                if (!pauseStateController)
                {
                    Debug.LogError("There needs to be one active PauseStateController script on a GameObject in your scene.");
                }

                else
                {
                    pauseStateController.Init();
                }
            }

            return pauseStateController;
        }
    }

    void Init()
    {
    }

    void Awake()
    {
        onGamePausedListener = new UnityAction(OnGamePaused);
        onGameResumedListener = new UnityAction(OnGameResumed);

    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onGamePaused, onGamePausedListener);
        EventManager.StartListening(EventManager.onGameResumed, onGameResumedListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onGamePaused, onGamePausedListener);
        EventManager.StopListening(EventManager.onGameResumed, onGameResumedListener);
    }

    private void OnGamePaused()
    {
        Time.timeScale = 0;
    }
    private void OnGameResumed()
    {
        Time.timeScale = 1;
    }


}
