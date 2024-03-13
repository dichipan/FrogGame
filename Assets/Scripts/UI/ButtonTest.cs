using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    private bool paused = false;
    // Start is called before the first frame update
    public void ButtonPressed()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            Time.timeScale = 0;
            paused = true;
        }
        Debug.Log("Success");
    }
}
