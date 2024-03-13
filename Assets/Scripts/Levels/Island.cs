using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public Player player;
    public bool scalable;
    public bool rotatable;
    public bool triggered = false;
    public bool log = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !triggered && collision.gameObject.GetComponent<Player>().inPlay)
        {
            player = collision.gameObject.GetComponent<Player>();
            AddPoint();
            player.time = Mathf.Clamp(player.time += player.landRegen, 0.0f, 100.0f);
            triggered = true;
        }
    }

    public void AddPoint()
    {
        player.points++;
        player.timeDrain += 0.0015f;
    }

}
