using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelInfo : MonoBehaviour
{
    private TextMeshProUGUI text;


    void Awake()
    {
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onRefreshLevelInfo, OnRefreshLevelInfo);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onRefreshLevelInfo, OnRefreshLevelInfo);
    }

    private void OnRefreshLevelInfo(object data)
    {
        int levels = (int)data;
        UpdateText(levels);
    }


    // Start is called before the first frame update
    private void UpdateText(int levels)
    {
        text.text = levels.ToString().PadLeft(3, '0');
    }

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
        UpdateText(GameController.instance.level);
    }
}
