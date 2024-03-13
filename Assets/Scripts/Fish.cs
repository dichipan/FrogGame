using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = -25.0f;
    public float lowerBounds = -20.0f;
    public float upperBounds = 20.0f;
    public float timeRemoval = 10.0f;
    private bool activated = false;
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x < lowerBounds)
        {
            transform.position = new Vector3(upperBounds, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !activated)
        {
            activated = true;
            other.gameObject.GetComponent<Player>().time -= timeRemoval;
        }
    }
}
