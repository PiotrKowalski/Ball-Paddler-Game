using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Paddle : MonoBehaviour
{
    public Vector3 mousePosition;
    private static Paddle paddle;
    private Ball ball;

    private Spawner spawner;

    public static Paddle instance
    {
        get
        {
            if (!paddle)
            {
                paddle = FindObjectOfType(typeof(Paddle)) as Paddle;

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


    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (ball == null && GameController.instance.ballsToSpawn > 0)
        {
            SpawnBall();
        }


        if (Input.GetMouseButtonDown(0))
        {
            HandleLeftMouseButtonDown();
        }

        HandleMovement();


    }

    private void HandleMovement()
    {
        //Vector3 screenPos = camera.WorldToViewportPoint(Input.mousePosition);
        //Debug.Log("target is " + screenPos.x + " pixels from the left");

        //if (screenPos.x < 7)
        //{
        //    return;
        //}
        //if (screenPos.x > 40)
        //{
        //    return;
        //}

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = new Vector3(mousePosition.x, transform.position.y, 0);

        gameObject.transform.position = newPos;
    }

    private void HandleLeftMouseButtonDown()
    {

        if (gameObject.GetComponentInChildren<Ball>() != null) EventManager.TriggerEvent(EventManager.onMouseLeftDown);

    }


    private void SpawnBall()
    {
        spawner.SpawnObject();
    }
}
