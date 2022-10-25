using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PointTypes
{
    Regular,
    Health,
    Multiplier,
}

public class Point : MonoBehaviour
{

    public PointTypes type;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D colider;
    [SerializeField] public AudioClip deathEffect;

    private UnityAction onRefreshLevelInfoListener;

    private void Awake()
    {
        //onRefreshLevelInfoListener = new UnityAction(OnRefreshLevelInfo);

    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colider = GetComponent<BoxCollider2D>();

        switch (type) {
            case PointTypes.Regular:
                spriteRenderer.color = new Color32(178, 190, 195, 255);
                break;
            case PointTypes.Health:
                spriteRenderer.color = new Color32(0, 184, 148, 255);
                break;
            case PointTypes.Multiplier:
                spriteRenderer.color = new Color32(9, 132, 227, 255);
                break;
        }

    }

    private void OnDestroy()
    {

    }

    private void OnEnable()
    {
        EventManager.StartListening(EventManager.onRefreshLevelInfo, OnRefreshLevelInfo);

    }

    private void OnDisable()
    {
    }

    private void OnCollisionEnter2D()
    {
        AudioController.Instance.PlaySound(deathEffect);

        colider.enabled = false;
        spriteRenderer.enabled = false;
        EventManager.TriggerEvent(EventManager.onPointDisabled, type);
        //gameObject.SetActive(false);
        // Destroy(gameObject);
    }

    void OnRefreshLevelInfo(object data)
    {
        colider.enabled = true;
        spriteRenderer.enabled = true;
    }


}
