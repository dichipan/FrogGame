using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttach : MonoBehaviour
{
    public GameObject player;
    public float followDistance = 50;

    private void Start()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (player.transform.position.z - followDistance));
    }
}
