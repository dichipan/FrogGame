using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public MouseVector controller;
    public int points;
    public int coins;
    public float time = 100.0f;
    public float timeDrain = 0.1f;
    public float landRegen = 17.5f;
    public bool inPlay;
    private Rigidbody rb;
    private BoxCollider box;
    
    void Start()
    {
        LoadPlayer();
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        if (inPlay)
        {
            time -= timeDrain;
        }
        if (time <= 0.0f || transform.position.y < -5) Death();
        
    }

    public bool GroundCheck()
    {
        Vector3 positionCheck = new Vector3(transform.position.x, transform.position.y + 10.0f, transform.position.z);
        return Physics.BoxCast(positionCheck, new Vector3(box.size.x / 2, box.size.y / 2, box.size.z / 2), transform.TransformDirection(Vector3.down), transform.rotation, 20.0f);
    }

    void Death()
    {
        SavePlayer();
        SceneManager.LoadScene("LilyPads");
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayerData(this);
    }

    public void LoadPlayer()
    {
        SaveData data = SaveSystem.LoadPlayer(this);
        coins = data.coins;
    }

}
