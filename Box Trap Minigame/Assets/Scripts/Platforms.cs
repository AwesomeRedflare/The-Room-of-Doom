using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public bool active;
    private bool atCenter;

    public float speed;
    public float minMoveTime;
    public float maxMoveTime;

    public Vector2 moveSpot;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;

        StartCoroutine("MovePlatform");
    }

    IEnumerator MovePlatform()
    {
        while (active == true)
        {
            yield return new WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));

            Vector2 destination;

            if(atCenter == false)
            {
                destination = moveSpot;
                
                while(atCenter == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                    if(Vector2.Distance(transform.position, destination) == 0)
                    {
                        atCenter = true;
                    }

                    yield return null;
                }
            }

            yield return new WaitForSeconds(Random.Range(minMoveTime, maxMoveTime));

            if (atCenter == true)
            {
                destination = startPos;

                while (atCenter == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                    if (Vector2.Distance(transform.position, destination) == 0)
                    {
                        atCenter = false;
                    }

                    yield return null;
                }
            }
        }
    }
}
