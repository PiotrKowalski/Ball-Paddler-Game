using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBall : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        //gameObject.transform.parent = Paddle.instance.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(0, 7), Random.Range(0, 7));
        rb.angularVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
