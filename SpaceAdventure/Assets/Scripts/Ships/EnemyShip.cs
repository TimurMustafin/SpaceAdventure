using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : SpaceShip, IMovable, IPoolReturnable
{
    public int HitPoints;

    float VibrationAmp;
    float VibrationFreq;
    
    LevelData levelData;
   // public AudioSource EnemyShipShoot;


    private void Start()
    {
        levelData = GameMaster.Instance.LevelData;
        switch (IdTag)
        {
            case "EnemyShipOne":
                Speed = levelData.ShipOneSpeed;
                FireRate = levelData.ShipOneFirerate;
                VibrationAmp = levelData.ShipOneAmp;
                VibrationFreq = levelData.ShipOneFreq;
                break;
            case "EnemyShipTwo":
                Speed = levelData.ShipTwoSpeed;
                FireRate = levelData.ShipTwoFirerate;
                VibrationAmp = levelData.ShipTwoAmp;
                VibrationFreq = levelData.ShipTwoFreq;
                break;
            case "EnemyShipThree":
                Speed = levelData.ShipThreeSpeed;
                FireRate = levelData.ShipThreeFirerate;
                VibrationAmp = levelData.ShipThreeAmp;
                VibrationFreq = levelData.ShipThreeFreq;
                break;
        }

        fireTimer = 1 / FireRate;
    }


    private void Update()
    {
        if (OutOfBoundaries())
            BackToPool();

        if (fireTimer < 0)
        {
            Shoot();
            fireTimer = 1 / FireRate;
        }

        fireTimer -= Time.deltaTime;

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
