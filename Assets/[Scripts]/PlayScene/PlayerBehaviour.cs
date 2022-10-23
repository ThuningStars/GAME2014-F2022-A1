////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/23/2022 02:23 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file
//Description : script for manage player properties : moving, gain score, get pickups, firing, use ray
//              weapon
//              The code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    public float speed = 2.0f;
    public Boundary boundary;
    public float fixedPosition;
    public float playerSpeed = 10.0f;
    public bool usingMobileInput = false;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public float fireRate = 0.2f;


    private Camera camera;
    //private ScoreManager scoreManager;
    private BulletManager bulletManager;
    private Vector2 screenWorldSize;
    private bool isPortrait;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();

        camera = Camera.main;

        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

        //scoreManager = FindObjectOfType<ScoreManager>();

        InvokeRepeating("FireBullets", 0.0f, fireRate);
    }

    // Update is called once per frame
    void Update()
    {
        AdaptOrientations();

        if (usingMobileInput)
        {
            MobileInput();
        }
        else
        {
            ConventionalInput();
        }

        Move();

        if (Input.GetKeyDown(KeyCode.K))
        {
            //scoreManager.AddPoints(10);
        }

    }

    private void AdaptOrientations()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            isPortrait = false;
            boundary.max = screenWorldSize.y - 0.6f;
            boundary.min = -screenWorldSize.y + 0.6f;
            transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
            fixedPosition = -screenWorldSize.x + 1.0f;

        }
        else if (Screen.orientation == ScreenOrientation.PortraitUpsideDown ||
                 Screen.orientation == ScreenOrientation.Portrait)
        {
            isPortrait = true;
            boundary.max = screenWorldSize.x - 0.6f;
            boundary.min = -screenWorldSize.x + 0.6f;
            transform.localRotation = Quaternion.identity;
            fixedPosition = -screenWorldSize.y + 1.0f;

        }
    }

    public void MobileInput()
    {
        foreach (var touch in Input.touches)
        {
            var destination = camera.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * playerSpeed);
        }
    }

    public void ConventionalInput()
    {
        if (isPortrait)
        {
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            transform.position += new Vector3(x, 0.0f, 0.0f);
        }
        else
        {
            float y = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            transform.position += new Vector3(0.0f, y, 0.0f);
        }
    }

    public void Move()
    {
        float clampedPosition;

        if (isPortrait)
        {
            clampedPosition = Mathf.Clamp(transform.position.x, boundary.min, boundary.max);

            transform.position = new Vector2(clampedPosition, fixedPosition);
        }
        else
        {
            clampedPosition = Mathf.Clamp(transform.position.y, boundary.min, boundary.max);

            transform.position = new Vector2(fixedPosition, clampedPosition);
        }
    }

    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.PLAYER);
    }
}
