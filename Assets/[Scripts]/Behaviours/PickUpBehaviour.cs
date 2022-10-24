////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PickUpBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/24/2022 03:15 AM
//Last Modified On : 10/24/2022 03:40 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file
//Description : script for controlling the pick up properties
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    HP,
    MP,
    SCORE,
    DAMAGE
}

public class PickUpBehaviour : MonoBehaviour
{
    public PickUpType type;
    public bool isStartTimer = false;
    public bool isActivated = false;
    public bool isWaiting = true;

    public float timer = 0;
    public ScreenBounds bounds;

    private float speed;


    private Vector2 screenWorldSize;

    // Start is called before the first frame update
    void Start()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        bounds.horizontal.max = screenWorldSize.x + 0.8f;
        bounds.horizontal.min = -screenWorldSize.x - 0.8f;
        bounds.vertical.max = screenWorldSize.y + 3.5f;
        bounds.vertical.min = -screenWorldSize.y - 3.5f;

        switch (type)
        {
            case PickUpType.HP:
                speed = 3;
                break;

            case PickUpType.MP:
                speed = 2;
                break;

            case PickUpType.DAMAGE:
                speed = 5;
                break;

            case PickUpType.SCORE:
                speed = 1;
                break;
        }

        transform.position = new Vector2(0.0f, screenWorldSize.y + 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStartTimer)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                isStartTimer = false;
                isActivated = true;
                SpawnPickUp();
            }
        }

        if(isActivated)
        {
            Move();
            CheckBounds();
        }
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
    }

    public void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            timer = 0;
            transform.position = new Vector2(0.0f, screenWorldSize.y + 1.0f);
            isActivated = false;
            isWaiting = true;
        }
    }

    public void SpawnPickUp()
    {
        var RandomXPosition = Random.Range(- screenWorldSize.x + 0.5f, screenWorldSize.x - 0.5f);

        transform.position = new Vector3(RandomXPosition, screenWorldSize.y + 1.0f, 0.0f);
    }

    public void Reset()
    {
        timer = 0;
        transform.position = new Vector2(0.0f, screenWorldSize.y + 1.0f);
        isActivated = false;
        isWaiting = true;

    }
}
