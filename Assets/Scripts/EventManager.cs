using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

[System.Serializable]
public class TypedEvent : UnityEvent<object> { }

public class EventManager : MonoBehaviour
{
    

    private Dictionary<string, UnityEvent> eventDictioanry;
    private Dictionary<string, TypedEvent> typedEventDictionary;

    private static EventManager eventManager;

    public static string onPointDisabled = "Point.Disabled";

    public static string onGamePaused= "Game.Paused";
    public static string onGameResumed = "Game.Resumed";

    public static string onMouseLeftDown= "Mouse.LeftDown";

    public static string onBallMultiply= "Ball.Multiply";
    public static string onBallSpawn= "Ball.Spawn";
    public static string onBallShot= "Ball.Shot";


    public static string onRefreshHealthInfo= "Info.RefreshHealth";
    public static string onRefreshBallsInfo = "Info.RefreshBalls";
    public static string onRefreshLevelInfo = "Info.RefreshLevel";

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {   
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }

                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            eventManager = this;
        }
    }

    void Init()
    {
        if (eventDictioanry == null)
        {
            typedEventDictionary = new Dictionary<string, TypedEvent>();
            eventDictioanry = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictioanry.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictioanry.Add(eventName, thisEvent);
        }
    }
    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        TypedEvent thisEvent = null;
        if (Instance.typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TypedEvent();
            thisEvent.AddListener(listener);
            Instance.typedEventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventName == null) return;

        UnityEvent thisEvent = null;

        if (Instance.eventDictioanry.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }

    }

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (eventName == null) return;

        TypedEvent thisEvent = null;

        if (Instance.typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }

    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictioanry.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void TriggerEvent(string eventName, object data)
    {
        TypedEvent thisEvent = null;
        if (Instance.typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(data);
        }
    }
}
