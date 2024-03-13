using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseVector : MonoBehaviour
{
    public Player player;
    public LineRenderer line;
    public GameObject landDot;
    public float lineWidth = 0.25f;
    Vector3 movementVector;
    Vector3 startPosition;
    Vector3 endPosition;
    public Vector3 newPosition;
    float speed = 75.0f;
    bool holdingDown;
    bool launchingCharacter;
    private float xyDistance;
    private float distance;
    private float halfDistance;
    private bool distanceSet;
    private Vector3 lerpVector;

    void Start()
    {
        line.positionCount = 2;
        line.startWidth = 0;
        line.endWidth = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.GroundCheck() && !IsPointerOverGameObject())
        {
            MousePressed();
        }
        if (holdingDown && !launchingCharacter)
        {
            CalculateLine();
            if (!Input.GetMouseButton(0))
            {
                MouseReleased();
            }
        }
        if (launchingCharacter)
        {
            LaunchCharacter();
        }
    }

    bool IsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }

    void DrawLandDot()
    {
        RaycastHit hit;
        //Debug.Log("Lerp: " + lerpVector);
        //Debug.Log("Player: " + player.transform.position);
        Physics.Raycast(player.transform.position + lerpVector + new Vector3(0, 20.0f, 0), transform.TransformDirection(Vector3.down), out hit, 100.0f);
        landDot.transform.position = hit.point;
        landDot.transform.position = new Vector3(landDot.transform.position.x, landDot.transform.position.y + 0.1f, landDot.transform.position.z);
    }

    void SetDistance()
    {
        if (!distanceSet)
        {
            distanceSet = true;
            distance = Vector3.Distance(player.transform.position, newPosition);
            halfDistance = distance / 4;
        }
    }

    void MousePressed()
    {
        startPosition = Input.mousePosition;
        holdingDown = true;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        landDot.GetComponent<MeshRenderer>().enabled = true;
    }

    void MouseReleased()
    {
        if (!player.inPlay) player.inPlay = true;
        holdingDown = false;
        launchingCharacter = true;
        newPosition = player.transform.position + lerpVector;
        line.startWidth = 0;
        line.endWidth = 0;
    }

    void CalculateLine()
    {
        endPosition = Input.mousePosition;
        movementVector = (endPosition - startPosition) * -0.035f; // change float for sens -0.05 for PC, -0.035 for mobile
        movementVector = new Vector3(Mathf.Round(movementVector.x * 10.0f) * 0.1f, 0f, Mathf.Round(movementVector.y * 10.0f) * 0.1f);
        lerpVector = Vector3.Lerp(lerpVector, movementVector, 17.0f * Time.deltaTime); // change float for smoothness, lower for longer lerp
        line.SetPosition(0, player.transform.position + new Vector3(0.0f, 1.5f, 0.0f));
        line.SetPosition(1, player.transform.position + lerpVector + new Vector3(0.0f, 0.5f, 0.0f));
        DrawLandDot();
        player.transform.LookAt(player.transform.position + lerpVector);
    }

    void LaunchCharacter()
    {
        SetDistance();
        xyDistance = Vector3.Distance(new Vector3(player.transform.position.x, newPosition.y, player.transform.position.z), newPosition);
        if (xyDistance > halfDistance)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(newPosition.x, player.transform.position.y + 6.0f, newPosition.z), speed * Time.deltaTime);
        }
        else
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, newPosition, speed * Time.deltaTime);

        }
        if (Vector3.Distance(player.transform.position, newPosition) < 0.01f)
        {
            distanceSet = false;
            launchingCharacter = false;
            line.SetPosition(1, player.transform.position + new Vector3(0.0f, 0.5f, 0.0f));
            lerpVector = new Vector3(0, 0, 0);
            landDot.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
