////////////////////////////////////////////////////////////////////////////////////////////////////////
//FileName: PlayerBehaviour.cs
//FileType: Unity C# Source file
//Author : Wanbo. Wang
//StudentID : 101265108
//Created On : 10/23/2022 02:31 AM
//Last Modified On : 10/24/2022 05:17 AM
//Copy Rights : SkyeHouse Intelligence
//Rivision Histrory: Create file => add ray, sfx, pick up properties and score, health .etc, level change
//                   when player die, save score and updating all the UI, => debug
//Description : script for manage player properties : moving, gain score, get pickups, firing, use ray
//              weapon
//              The initial code is from in class lab
////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    public float speed = 2.0f;
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;

    public float playerSpeed = 10.0f;
    public bool usingMobileInput = false;

    public float healthValue = 100;
    public float powerValue = 50;
    public int damage = 1;
    public int score = 0;

    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint2;

    public float fireRate = 0.2f;

    [Header("Ray")]
    public GameObject ray;

    [Header("UI Properties")]
    public Image healthBar;
    public Image powerBar;
    public TMP_Text scoreText;

    private Camera camera;
    //private ScoreManager scoreManager;
    private BulletManager bulletManager;
    private Vector2 screenWorldSize;

    [Header("SFX Properties")]
    public AudioSource audioSource;
    public AudioSource localAudioSource;

    public AudioClip fire;
    public AudioClip hurt;
    public AudioClip explode;
    public AudioClip HPUp;
    public AudioClip MPUp;
    public AudioClip ScoreUp;
    public AudioClip DamageUp;
    public AudioClip RaySFX;

    void Start()
    {
        ray.SetActive(false);
        bulletManager = FindObjectOfType<BulletManager>();

        camera = Camera.main;

        usingMobileInput = Application.platform == RuntimePlatform.Android ||
                           Application.platform == RuntimePlatform.IPhonePlayer;

        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();

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

        if(healthValue <= 0)
        {
            Destroy(GameObject.Find("GameController"));
            PlayerPrefs.SetInt("score", score);
            audioSource.PlayOneShot(explode);
            SceneManager.LoadScene(sceneBuildIndex: 3);
        }

        UpdateUI();

    }

    private void UpdateUI()
    {
        if(healthValue > 100)
        {
            healthValue = 100;
        }
        if(powerValue > 100)
        {
            powerValue = 100;
        }

        healthBar.fillAmount = healthValue / 100.0f;
        powerBar.fillAmount = powerValue / 100.0f;
        scoreText.text = $"Score: {score}";
    }

    private void AdaptOrientations()
    {
        screenWorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        horizontalBoundary.max = screenWorldSize.x - 0.6f;
        horizontalBoundary.min = -screenWorldSize.x + 0.6f;
        verticalBoundary.max = 0.0f;
        verticalBoundary.min = -screenWorldSize.y + 0.5f;
        transform.localRotation = Quaternion.identity;

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
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x, 0.0f, 0.0f);

        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        transform.position += new Vector3(0.0f, y, 0.0f);
    }

    public void Move()
    {
        ray.transform.position = new Vector2(transform.position.x, transform.position.y + 6.1f);

        float clampedXPosition = Mathf.Clamp(transform.position.x, horizontalBoundary.min, horizontalBoundary.max);
        float clampedYPosition = Mathf.Clamp(transform.position.y, verticalBoundary.min, verticalBoundary.max);
        transform.position = new Vector2(clampedXPosition, clampedYPosition);
    }

    void FireBullets()
    {
        audioSource.PlayOneShot(fire);

        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletType.PLAYER);
        var bullet2 = bulletManager.GetBullet(bulletSpawnPoint2.position, BulletType.PLAYER);

    }

    public void AbilityButtonClick()
    {
        if(powerValue >= 100)
        {
            localAudioSource.PlayOneShot(RaySFX);
            StartCoroutine(StartRayShooting());
        }
    }

    IEnumerator StartRayShooting()
    {
        powerValue = 0;
        ray.SetActive(true);
        yield return new WaitForSeconds(5);
        ray.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            localAudioSource.PlayOneShot(hurt);
            healthValue -= 10;
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            BulletBehaviour bullet = other.gameObject.GetComponent<BulletBehaviour>();

            if(bullet != null && bullet.bulletType != BulletType.PLAYER)
            {
                localAudioSource.PlayOneShot(hurt);

                healthValue -= bullet.damageValue;
                bullet.bulletManager.ReturnBullet(bullet.gameObject, bullet.bulletType);

            }
        }
        else if (other.gameObject.CompareTag("PickUp"))
        {

            PickUpBehaviour pickUp = other.gameObject.GetComponent<PickUpBehaviour>();

            if (pickUp != null)
            {
                switch(pickUp.type)
                {
                    case PickUpType.HP:
                        healthValue += 10;
                        localAudioSource.PlayOneShot(HPUp);

                        break;

                    case PickUpType.MP:
                        powerValue += 25;
                        localAudioSource.PlayOneShot(MPUp);

                        break;

                    case PickUpType.DAMAGE:
                        damage += 1;
                        localAudioSource.PlayOneShot(DamageUp);

                        break;

                    case PickUpType.SCORE:
                        score += 150;
                        localAudioSource.PlayOneShot(ScoreUp);
                        break;

                }

                pickUp.Reset();

            }
        }
    }
}
