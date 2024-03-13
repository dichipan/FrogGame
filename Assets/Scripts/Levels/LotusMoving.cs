using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusMoving : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector3 target;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            FindNewSpot();
        }
    }

    void FindNewSpot()
    {
        float rndX = Random.Range(-1.0f, 1.0f);
        float rndZ = Random.Range(-1.0f, 1.0f);
        target = new Vector3(transform.position.x + rndX, transform.position.y, transform.position.z + rndZ);
    }
}
