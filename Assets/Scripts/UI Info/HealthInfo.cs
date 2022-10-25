using System.Runtime.ExceptionServices;
using TMPro;
using UnityEngine;

public class HealthInfo : MonoBehaviour
{
    private TextMeshProUGUI text;


    void Awake()
    {
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onRefreshHealthInfo, OnRefreshHealthInfo);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onRefreshHealthInfo, OnRefreshHealthInfo);
    }

    private void OnRefreshHealthInfo(object data)
    {
        int hps = (int)data;
        UpdateText(hps);
    }


    // Start is called before the first frame update
    private void UpdateText(int health)
    {
        text.text = health.ToString().PadLeft(3, '0');
    }

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        UpdateText(GameController.instance.health);
    }

}
