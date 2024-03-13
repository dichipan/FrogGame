using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadMovement : MonoBehaviour
{
    public float speed = 0.1f;
    private bool playerOn;
    private GameObject player;
    private Vector3 target;
    private Vector3 playerTarget;
    private void Start()
    {
        FindNewSpot();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (playerOn)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerTarget, speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            FindNewSpot();
        }
    }

    void FindNewSpot()
    {
        float rndX = Random.Range(-1.0f, 1.0f);
        float rndZ = Random.Range(-1.0f, 1.0f);
        if (playerOn) playerTarget = new Vector3(player.transform.position.x + rndX, player.transform.position.y, player.transform.position.z + rndZ);
        target = new Vector3(transform.position.x + rndX, transform.position.y, transform.position.z + rndZ);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = true;
            player = collision.gameObject;
            FindNewSpot();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = false;
        }
    }
}
