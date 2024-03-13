using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIText : MonoBehaviour
{
    public Player player;
    public TMP_Text text;

    void Update()
    {
        text.text = player.points.ToString();
    }
}
