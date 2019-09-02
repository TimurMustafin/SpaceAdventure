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
    LevelData levelData;

    private void Start()
    {
        levelData = GameMaster.Instance.LevelData;

        Speed = levelData.PlayerSpeed;
        FireRate = levelData.PlayerFirerate;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
			if (timer < 0)
			{
				Shoot();
				timer = 1/FireRate;
			}            
			
        }
		timer -= Time.deltaTime;
        

         Move(); 
    }

    public void Move()
    {
        transform.Translate(Vector3.right * (-Input.GetAxis("Horizontal")) * Speed * Time.deltaTime, Space.Self);
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
