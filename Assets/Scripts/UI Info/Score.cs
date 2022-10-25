using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class Score : MonoBehaviour
{
    private int Points = 0;
    private TextMeshProUGUI text;

    //private UnityAction onPointDestroyedListener;

    void Awake()
    {
        //onPointDestroyedListener = new UnityAction(OnPointDestroyed);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onPointDisabled, OnPointDestroyed);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onPointDisabled, OnPointDestroyed);
    }

    private void OnPointDestroyed(object data)
    {
        this.Points += 1;
        UpdateText(this.Points);
    }


    // Start is called before the first frame update
    private void UpdateText(int points)
    {
        text.text = points.ToString().PadLeft(3, '0');
    }

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
        this.UpdateText(Points);
    }



}
