using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWater : MonoBehaviour
{
    public GameObject waterPlane;
    public int numToSpawn;
    public GameObject[] decorations;
    public int decorationsMin;
    public int decorationsMax;
    public Queue<GameObject> waterPlanes;
    public GameObject playerCamera;
    public Color shiftColor;
    private Color oldColor;
    private GameObject lastWaterPlane;
    private int planesToDequeue;
    private float lerpAmount;
    void Start()
    {
        oldColor = waterPlane.GetComponent<Renderer>().material.GetColor("_WaterColor");
        waterPlanes = new Queue<GameObject>();
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnWaterPlane();
        }
    }

    private void Update()
    {
        foreach (var plane in waterPlanes)
        {
            if (plane.transform.position.z < playerCamera.transform.position.z - 75)
            {
                planesToDequeue++;
            }
        }
        DeleteWaterPlane();
    }

    void SpawnWaterPlane()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        if (waterPlanes.Count > 0)
        {
            spawnPosition = new Vector3(0, 0, lastWaterPlane.transform.position.z + 100);
        }
        lastWaterPlane = Instantiate(waterPlane, spawnPosition, Quaternion.identity);
        SpawnDecorations();
        waterPlanes.Enqueue(lastWaterPlane);
    }

    void DeleteWaterPlane()
    {
        while (planesToDequeue > 0)
        {
            GameObject removedPlane = waterPlanes.Dequeue();
            Object.Destroy(removedPlane);
            SpawnWaterPlane();
            lerpAmount += 0.025f;
            UpdateColor();
            planesToDequeue--;
        }
    }

    void UpdateColor()
    {
        foreach (GameObject plane in waterPlanes)
        {
            Material waterMat = plane.GetComponent<Renderer>().material;
            waterMat.SetColor("_WaterColor", Color.Lerp(oldColor, shiftColor, lerpAmount));
        }
    }

    void SpawnDecorations()
    {
        int rnd = Random.Range(decorationsMin, decorationsMax);
        for (int i = 0; i < rnd; i++)
        {
            Vector3 spawnLocation = new Vector3(lastWaterPlane.transform.position.x + Random.Range(-10.0f, 10.0f), 0, lastWaterPlane.transform.position.z + Random.Range(-100.0f, 100.0f));
            GameObject decoration = Instantiate(decorations[Random.Range(0, decorations.Length)], spawnLocation, Quaternion.identity);
            float randomRotation = Random.Range(0.0f, 360.0f);
            decoration.transform.Rotate(new Vector3(0, randomRotation, 0));
        }
    }

}
