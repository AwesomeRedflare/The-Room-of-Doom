using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public bool spawn;

    public float delay;
    public float fireTime;
    public float offTime;
    public float signTime;

    private GameObject fire;

    private void Start()
    {
        fire = transform.GetChild(0).gameObject;

        StartCoroutine("SpawnFire");
    }

    IEnumerator SpawnFire()
    {
        Animator anim = GetComponentInChildren<Animator>();

        yield return new WaitForSeconds(delay - signTime);

        anim.SetTrigger("Sign");

        yield return new WaitForSeconds(signTime);

        //Color color = fire.GetComponent<SpriteRenderer>().color;

        while (spawn)
        {
            //color.a = 1f;
            //fire.GetComponent<SpriteRenderer>().color = color;

            anim.SetBool("isOn", true);

            fire.GetComponent<BoxCollider2D>().enabled = true;

            yield return new WaitForSeconds(fireTime);

            //color.a = 0.2f;
            //fire.GetComponent<SpriteRenderer>().color = color;

            anim.SetBool("isOn", false);

            fire.GetComponent<BoxCollider2D>().enabled = false;

            yield return new WaitForSeconds(offTime - signTime);

            anim.SetTrigger("Sign");

            yield return new WaitForSeconds(signTime);
        }
    }
}
