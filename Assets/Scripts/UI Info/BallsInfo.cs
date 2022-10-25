using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsInfo : MonoBehaviour
{
    private TextMeshProUGUI text;


    void Awake()
    {
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onRefreshBallsInfo, OnRefreshBallsInfo);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onRefreshBallsInfo, OnRefreshBallsInfo);
    }

    private void OnRefreshBallsInfo(object data)
    {
        int balls = (int)data;
        UpdateText(balls);
    }


    // Start is called before the first frame update
    private void UpdateText(int balls)
    {
        Debug.Log(balls);
        text.text = balls.ToString().PadLeft(3, '0');
    }

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
        UpdateText(GameController.instance.ballsToSpawn);
    }
}
