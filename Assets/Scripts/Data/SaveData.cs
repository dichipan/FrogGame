using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int highScore;
    public int coins;

    public SaveData(Player player)
    {
        highScore = player.points;
        coins = player.coins;
    }
}
