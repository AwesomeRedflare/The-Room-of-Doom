using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawblade : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }
}
