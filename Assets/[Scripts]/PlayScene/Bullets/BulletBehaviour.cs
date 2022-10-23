////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: BulletBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Last Modified On : 10/22/2022 
//Rivision Histrory: Create file
//Description : script for all the bullet behaviours : move, respawn, behaviour, collision check
//              The main code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundary horizontal;
    public Boundary vertical;
}


public class BulletBehaviour: MonoBehaviour
{
    [Header("Bullet Properties")]
    public BulletDirection bulletDirection;
    public float speed;
    public ScreenBounds bounds;
    public BulletType bulletType;

    private Vector3 velocity;
    private BulletManager bulletManager;

    private Vector2 screenWorldSize;
    private bool isPortrait;

    void Start()
    {
        AdaptOrientations();

        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            isPortrait = false;

        }
        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown ||
                 Screen.orientation == ScreenOrientation.Portrait)
        {
            isPortrait = true;
        }

        bulletManager = FindObjectOfType<BulletManager>();
    }

    void Update()
    {
        CheckCurrentOrientation();
        AdaptOrientations();
        Move();
        CheckBounds();
        
    }

    private void AdaptOrientations()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        bounds.horizontal.max = screenWorldSize.x + 1.0f;
        bounds.horizontal.min = -screenWorldSize.x - 1.0f;
        bounds.vertical.max = screenWorldSize.y + 1.0f;
        bounds.vertical.min = -screenWorldSize.y - 1.0f;
    }

    void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    void CheckBounds()
    {
        if ((transform.position.x > bounds.horizontal.max) ||
            (transform.position.x < bounds.horizontal.min) ||
            (transform.position.y > bounds.vertical.max) ||
            (transform.position.y < bounds.vertical.min))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }

    void CheckCurrentOrientation()
    {
        if(isPortrait)
        {
            if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
                Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                isPortrait = false;
                Destroy(gameObject);
                bulletManager.ResetBullet();
            }
        }
        else
        {
            if (Screen.orientation == ScreenOrientation.PortraitUpsideDown ||
                Screen.orientation == ScreenOrientation.Portrait)
            {
                isPortrait = true;
                Destroy(gameObject);
                bulletManager.ResetBullet();
            }
        }
    }

    public void SetDirection(BulletDirection direction)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up * speed;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right * speed;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down * speed;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left * speed;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((bulletType == BulletType.PLAYER) ||
            (bulletType == BulletType.ENEMY && other.gameObject.CompareTag("Player")))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
        
    }

}