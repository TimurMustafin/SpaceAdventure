using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : SpaceShip, IMovable, IPoolReturnable
{
    protected int HitPoints;

    protected float VibrationAmp;
    protected float VibrationFreq;

   // public AudioSource EnemyShipShoot;

    private void Start()
    {
        LevelData levelData = GameMaster.Instance.levelData;

       switch(IdTag)

       {
            case "EnemyShipOne":
                Speed = levelData.Ship1Speed;
                FireRate = levelData.Ship1Firerate;
                VibrationAmp = levelData.Ship1VibrationAmp;
                VibrationFreq = levelData.Ship1VibrationFreq;
                HitPoints = levelData.Ship1Hitpoints;
                break;

            case "EnemyShipTwo":
                Speed = levelData.Ship2Speed;
                FireRate = levelData.Ship2Firerate;
                VibrationAmp = levelData.Ship2VibrationAmp;
                VibrationFreq = levelData.Ship2VibrationFreq;
                HitPoints = levelData.Ship2Hitpoints;
                break;
            case "EnemyShipThree":
                Speed = levelData.Ship3Speed;
                FireRate = levelData.Ship3Firerate;
                VibrationAmp = levelData.Ship3VibrationAmp;
                VibrationFreq = levelData.Ship3VibrationFreq;
                HitPoints = levelData.Ship3Hitpoints;
                break;
       }

        FireTimer = 1 / FireRate;
    }

    private void Update()
    {
        if (OutOfBoundaries())
            BackToPool();

        if (FireTimer < 0)
        {
            Shoot();
            FireTimer = 1 / FireRate;
        }

        FireTimer -= Time.deltaTime;

        Move();
        VIbration();
    }  

    public void Move()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

	public void VIbration()
	{
        transform.Translate(Vector3.right * VibrationAmp * Mathf.Sin(VibrationFreq * Time.time));
	}

	protected override void Shoot()
    {
        for (int i = 0; i < FirePoints.Length; i++)
        {
            Instantiate(rocketPrefab, FirePoints[i].position, Quaternion.identity);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "PlayerRocket")
        {
            PlayerStats.HitPoints += HitPoints;
            GameMaster.Instance.UpdateHitPoint();
            GameMaster.Instance.Explostion();
            BackToPool();
        }  
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
        EnemyGenerator.Instance.DictionaryEnemyPool[IdTag].Enqueue(gameObject);
    }

    public void Rotate()
    {
        //
    }

}
