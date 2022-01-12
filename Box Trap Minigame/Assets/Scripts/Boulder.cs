using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    public float speed;

    public int bounces;

    public Rigidbody2D rb;

    public Rotater rotater;

    public GameObject destroyEffect;

    private void FixedUpdate()
    {
        if(bounces >= 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("boom");

            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeBehavior>().TriggerShake(.2f, .15f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall") || col.gameObject.CompareTag("Boulder"))
        {
            FindObjectOfType<AudioManager>().Play("thump");

            bounces--;
            speed = -speed;

            rotater.speed *= -1;
        }
    }
}
