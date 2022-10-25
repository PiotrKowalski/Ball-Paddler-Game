using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private Vector2 velocity;
    public float speed;

    private Rigidbody2D rb;

    private UnityAction onMouseLeftDownListener;

    void Awake()
    {
        onMouseLeftDownListener = new UnityAction(OnMouseLeftDown);

        velocity = new Vector2(0, speed);

    }

    private void Start()
    {
        EventManager.TriggerEvent(EventManager.onBallSpawn);
    }

    void OnEnable()
    {
        EventManager.StartListening(EventManager.onMouseLeftDown, onMouseLeftDownListener);
    }

    void OnDisable()
    {
        EventManager.StopListening(EventManager.onMouseLeftDown, onMouseLeftDownListener);
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            //Debug.Log(rb.velocity.magnitude);
            rb.velocity.Normalize();
            rb.velocity *= speed;

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        }

    }

    void OnMouseLeftDown()
    {
        if (gameObject.transform.parent == null) return;

        //if (gameObject.transform.parent.GetType() == typeof(Paddle2)) return;

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
        rb.angularDrag = 0;
        rb.velocity = velocity;

        rb.angularVelocity = 0;

        gameObject.transform.parent = Paddle2.instance.GetComponent<Transform>().parent;

        EventManager.TriggerEvent(EventManager.onBallShot);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
