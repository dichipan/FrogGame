using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColorShift : MonoBehaviour
{
    public Color newColor;
    public Player player;
    public GameObject water;
    private Color oldColor;
    Material waterMat;

    void Start()
    {
        waterMat = water.GetComponent<Renderer>().material;
        oldColor = waterMat.GetColor("_WaterColor");
    }

    void UpdateColor()
    {
        oldColor = Color.Lerp(oldColor, newColor, player.points / 100.0f);
        waterMat.SetColor("_WaterColor", oldColor);
    }
}
