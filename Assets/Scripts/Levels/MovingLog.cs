using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLog : MonoBehaviour
{
    GameObject player;
    bool playerOn;
    public float speed;
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x + (speed * Time.deltaTime), transform.position.y, transform.position.z);
        BorderCheck();
        if (playerOn)
        {
            player.transform.position = new Vector3(player.transform.position.x + (speed * Time.deltaTime), player.transform.position.y, player.transform.position.z);
        }
    }

    void BorderCheck()
    {
        if (speed > 0)
        {
            if (transform.position.x > 20)
            {
                transform.position = new Vector3(-20, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.x < -20) transform.position = new Vector3(20, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOn = false;
        }
    }

    public MovingLog SpawnPartnerLog()
    {
        int randomize = Random.Range(0, 2);
        if (randomize == 1) speed = speed * -1;
        MovingLog clonedLog = Instantiate(gameObject, new Vector3(transform.position.x + 20.0f, transform.position.y, transform.position.z), Quaternion.identity).GetComponent<MovingLog>();
        clonedLog.speed = speed;
        return clonedLog;
    }
}
