using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject player;
    public GameObject[] islandPrefabs;
    public GameObject[] obstaclePrefabs;
    public GameObject coin;
    public GameObject playerCamera;
    public int islandNum;
    Queue<Island> islandsQueue;
    private Island lastIsland;
    private int islandsToDequeue;
    
    void Start()
    {
        islandsQueue = new Queue<Island>();
        for (int i = 0; i < islandNum; ++i)
        {
            SpawnIsland();
        }
    }

    void Update()
    {
        foreach (var island in islandsQueue)
        {
            if (island.transform.position.z < playerCamera.transform.position.z - 20.0f)
            {
                islandsToDequeue++;
            }
        }
        DeleteIslands(islandsToDequeue);
    }

    void SpawnIsland()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        if (islandsQueue.Count > 0) // if we aren't spawning first island
        {
            float xPos = Mathf.Clamp(lastIsland.transform.position.x + Random.Range(-7.0f, 7.0f), -3.0f, 3.0f);
            float zPosOffset = Random.Range(8.0f, 16.0f);
            float zPos = lastIsland.transform.position.z + zPosOffset;
            SpawnObstacle(zPosOffset);
            spawnPosition = new Vector3(xPos, 0, zPos);
            int rnd = Random.Range(0, islandPrefabs.Length);
            lastIsland = Instantiate(islandPrefabs[rnd], spawnPosition, Quaternion.identity).GetComponent<Island>();
            if (lastIsland.log)
            {
                MovingLog clonedLog = lastIsland.GetComponent<MovingLog>().SpawnPartnerLog();
                islandsQueue.Enqueue(clonedLog.GetComponent<Island>());
            }
        } else // spawning first island
        {
            lastIsland = Instantiate(islandPrefabs[0], spawnPosition, Quaternion.identity).GetComponent<Island>();
        }
        RandomizeTransform(lastIsland);
        SpawnCoin();
        islandsQueue.Enqueue(lastIsland);
    }

    void SpawnCoin()
    {
        int rnd = Random.Range(0, 3);
        if (rnd == 0)
        {
            Instantiate(coin, new Vector3(lastIsland.transform.position.x, lastIsland.transform.position.y + 1.5f, lastIsland.transform.position.z), Quaternion.identity);
        }
    }

    void RandomizeTransform(Island island)
    {
        if (island.scalable)
        {
            float randomScale = Random.Range(2.0f, 4.0f);
            island.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
        if (island.rotatable)
        {
            float randomRotation = Random.Range(0.0f, 360.0f);
            island.transform.Rotate(new Vector3(0, randomRotation, 0));
        }
    }

    void SpawnObstacle(float zPosOffset)
    {
        int rnd = Random.Range(0, 3);
        if (zPosOffset >= 13.0f && rnd == 0)
        {
            Vector3 obstacleSpawnPos = new Vector3(-9.0f, 2.5f, lastIsland.transform.position.z + zPosOffset / 2.0f);
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], obstacleSpawnPos, Quaternion.identity);
            //HARD CODED, FIX IN THE FUTURE PLZ
            if (obstacle.tag == "Wind") obstacle.transform.Rotate(new Vector3(-180, -90, 0));
            else obstacle.transform.position = new Vector3(0.0f, 0.0f, lastIsland.transform.position.z + zPosOffset / 2.0f);
        }
    }

    void DeleteIslands(int i)
    {
        while (islandsToDequeue > 0)
        {
            Island removedIsland = islandsQueue.Dequeue();
            removedIsland.player = player.GetComponent<Player>();
            if (!removedIsland.triggered) removedIsland.AddPoint();
            Object.Destroy(removedIsland.gameObject);
            SpawnIsland();
            islandsToDequeue--;
        }
    }
}
