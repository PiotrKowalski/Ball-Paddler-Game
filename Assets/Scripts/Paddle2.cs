using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Paddle2 : MonoBehaviour
{
    public int speed = 0;
    public Vector2 velocity = new(0, 0);
    public Vector3 mousePosition;
    public float clampMagnitude;

    private Ball ball;
    private Spawner spawner;

    private UnityAction onBallShotListener;


    private static Paddle2 paddle;
    public static Paddle2 instance
    {
        get
        {
            if (!paddle)
            {
                paddle = FindObjectOfType(typeof(Paddle2)) as Paddle2;

                if (!paddle)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }

                else
                {
                    paddle.Init();
                }
            }

            return paddle;
        }
    }

    private void Init()
    {

    }

    private void Awake()
    {
        onBallShotListener = new UnityAction(OnBallShot);

    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onRefreshBallsInfo, OnRefreshBallsInfo);
        EventManager.StartListening(EventManager.onBallShot, onBallShotListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onRefreshBallsInfo, OnRefreshBallsInfo);
        EventManager.StartListening(EventManager.onBallShot, onBallShotListener);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Spawner>();
        if (GameController.instance.ballsToSpawn > 0) SpawnBall();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftMouseButtonDown();
        }
    }

    private void FixedUpdate()
    {

        HandleMovement();

    }

    private void HandleMovement()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Mathf.Abs(mousePosition.x - transform.position.x) < 0.25) return;

        int directionFlag = 0;
        if (mousePosition.x - transform.position.x > 0)
        {
            directionFlag = 1;
        }
        else
        {
            directionFlag = -1;
        }



        GetComponent<Rigidbody2D>().AddRelativeForce((speed * new Vector2(directionFlag, 0)));
        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, clampMagnitude);
    }

    public void HandleLeftMouseButtonDown()
    {
        //gameObject.transform.GetChild(0).GetComponentInChildren<Ball>().ShootMyself();
        if (ball != null)
        {
            Debug.Log(222222);
            EventManager.TriggerEvent(EventManager.onMouseLeftDown);
        }
    }


    private void SpawnBall()
    {
        ball = spawner.SpawnObject().GetComponent<Ball>();
    }

    private void OnBallShot()
    {
        ball = null;
        Debug.Log(111);
        Debug.LogFormat("{0} {1}", ball, GameController.instance.ballsToSpawn);
        if (GetComponentInChildren<Ball>() == null && GameController.instance.ballsToSpawn > 0)
        {
            
            SpawnBall();
        }
    }

    private void OnRefreshBallsInfo(object data)
    {
        if (ball == null && (int)data > 0)
        {
            SpawnBall();
        }
    }
}

