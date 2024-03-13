using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    Player player;
    private bool blowingPlayer;
    public float blowStrength;

    private void Update()
    {
        if (blowingPlayer)
        {
            player.transform.position = new Vector3(player.transform.position.x + (blowStrength * Time.deltaTime), player.transform.position.y, player.transform.position.z);
            player.controller.newPosition = new Vector3(player.controller.newPosition.x + (blowStrength * Time.deltaTime), player.controller.newPosition.y, player.controller.newPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            blowingPlayer = true;
            player = other.gameObject.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            blowingPlayer = false;
        }
    }
}
