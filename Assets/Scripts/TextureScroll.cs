using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public float scrollSpeedX;
    public float scrollSpeedY;
    private MeshRenderer mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeedX;
        mesh.material.SetTextureOffset("_Foam", new Vector2(offset, offset));
    }
}
