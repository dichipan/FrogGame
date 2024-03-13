using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoadData : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.LoadPlayer();
    }
}
