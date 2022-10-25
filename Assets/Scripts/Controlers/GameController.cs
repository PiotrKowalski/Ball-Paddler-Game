using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public int _level = 1;

    public int level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;

            EventManager.TriggerEvent(EventManager.onRefreshLevelInfo, _level);
        }
    }

    public static int points = 0;
    private int maxPointsOnLevel;

    public int _health = 0;
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            EventManager.TriggerEvent(EventManager.onRefreshHealthInfo, _health);
        }
    }

    public int _ballsToSpawn =1;

    public int ballsToSpawn
    {
        get
        {
            return _ballsToSpawn;
        }
        set
        {
            _ballsToSpawn = value;

            EventManager.TriggerEvent(EventManager.onRefreshBallsInfo, _ballsToSpawn);
        }
    }


    private UnityAction onBallSpawnListener;


    private static GameController gameController;
    public static GameController instance
    {
        get
        {
            if (!gameController)
            {
                gameController = FindObjectOfType(typeof(GameController)) as GameController;

                if (!gameController)
                {
                    Debug.LogError("There needs to be one active GameController script on a GameObject in your scene.");
                }

                else
                {
                    gameController.Init();
                }
            }
            return gameController;
        }
    }

    void Init() { }

    void Awake()
    {
        onBallSpawnListener = new UnityAction(OnBallSpawn);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onPointDisabled, OnPointDisabled);
        EventManager.StartListening(EventManager.onBallSpawn, onBallSpawnListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onPointDisabled, OnPointDisabled);
        EventManager.StopListening(EventManager.onBallSpawn, onBallSpawnListener);
    }

    private void OnPointDisabled(object data)
    {
        PointTypes type = (PointTypes)data;

        points += 1;

        if (points == level*maxPointsOnLevel)
        {
            level++;
            // Add level and enable all points again trigger
            Debug.Log("All points hit");
        }

        if (type == PointTypes.Health)
        {
            health += 1;
        }

        if (type == PointTypes.Multiplier)
        {
            ballsToSpawn += 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxPointsOnLevel = FindObjectsOfType(typeof(Point)).Length;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnBallSpawn()
    {
        ballsToSpawn -= 1;
    }

}
