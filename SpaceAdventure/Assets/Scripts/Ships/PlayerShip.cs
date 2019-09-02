using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : SpaceShip, IMovable
{
    public AudioSource PlayerShoot;
    public AudioSource CrashShip;
    public GameObject CrashEffect;
    public Camera mainCamera;
	float timer;

    private void Start()
    {
        
        LevelData levelData = GameMaster.Instance.levelData;
        Speed = levelData.PlayerSpeed;
        FireRate = levelData.PlayerFirerate;
        timer = 1 / FireRate;
    }

    void Update()
    {
        //Mobile
        Touch[] touches = Input.touches;
        if (Input.touchCount == 1)
        {
            if (touches[0].position.x < Screen.width)
            {
                transform.Translate(Vector3.right * Speed * Time.deltaTime);
                Vector3 temp = transform.position;
                temp.x = Mathf.Clamp(temp.x, -14, 14);
                transform.position = temp;
            }
        }
        if (Input.touchCount == 2)
        {
            if (timer < 0)
            {
                Shoot();
                timer = 1 / FireRate;
            }
        }

        // Desktop
       /* if (Input.GetKey(KeyCode.Space))
        {
			if (timer < 0)
			{
				Shoot();
				timer = 1/ FireRate;
			}            	
        }
		timer -= Time.deltaTime;       

        Move(); */
    }

    public void Move()
    {
        transform.Translate(Vector3.right * (-Input.GetAxis("Horizontal")) * Speed * Time.deltaTime, Space.Self);
        Vector3 temp = transform.position;
        temp.x = Mathf.Clamp(temp.x, -14, 14);
        transform.position = temp;
    }

    public void Rotate()
    {
        //
    }

    protected override void Shoot()
    {
        Instantiate(rocketPrefab, FirePoints[0].position, Quaternion.identity);
        PlayerShoot.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {      
        GameObject effect =
                    Instantiate(CrashEffect, collision.collider.transform.position, Quaternion.identity);
        CrashShip.Play();
        StartCoroutine(mainCamera.GetComponent<CameraMotion>().CameraShake());

        Destroy(effect, 0.3f);
    }
}
