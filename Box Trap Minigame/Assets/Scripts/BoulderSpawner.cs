using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public bool spawn = true;

    public float minSpawnTime;
    public float maxSpawnTime;

    public GameObject boulder;

    public GameObject ceiling;
    public float ceilingSpeed;
    public float waitTime;
    public float boulderWaitTime;

    public Transform[] spawnPoints;
    public Transform[] movePoints;
    public int mMin, mMax;

    private ShakeBehavior shake;


    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ShakeBehavior>();
    }

    public IEnumerator SpawnBoulder()
    {
        while (spawn == true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            int spot = Random.Range(mMin, mMax);

            StartCoroutine(MoveCeiling(spot));

            yield return new WaitForSeconds(boulderWaitTime);

            Instantiate(boulder, spawnPoints[spot].position, transform.rotation);
        }
    }

    IEnumerator MoveCeiling(int spot)
    {
        bool destination = false;

        Vector2 StartPos = new Vector2(ceiling.transform.position.x, ceiling.transform.position.y);

        while (!destination)
        {
            ceiling.transform.position = Vector2.MoveTowards(ceiling.transform.position, movePoints[spot].position, ceilingSpeed * Time.deltaTime);

            shake.TriggerShake(.05f, .0001f);

            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().pitch = Random.Range(.5f, 1.5f);

            if (ceiling.transform.position == movePoints[spot].position)
            {
                yield return new WaitForSeconds(waitTime);

                destination = true;
            }

            yield return null;
        }

        while (destination)
        {
            ceiling.transform.position = Vector2.MoveTowards(ceiling.transform.position, StartPos, ceilingSpeed * Time.deltaTime);

            shake.TriggerShake(.05f, .0001f);

            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().pitch = Random.Range(.5f, 1.5f);

            if (Vector2.Distance(ceiling.transform.position, StartPos) == 0)
            {
                destination = false;

                StopCoroutine("MoveCeiling");
            }

            yield return null;
        }
    }
}
