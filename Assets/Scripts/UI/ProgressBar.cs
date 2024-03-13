using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;
    public Player player;

    void Update()
    {
        progressBar.fillAmount = player.time / 100.0f;
    }
}
