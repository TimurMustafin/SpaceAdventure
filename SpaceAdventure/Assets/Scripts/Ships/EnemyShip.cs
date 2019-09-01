using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : SpaceShip, IMovable, IPoolReturnable
{
    public int HitPoints;

    public float VibrationAmp;
    public float VibrationFreq;
    protected float VibrationPhase;

   // public AudioSource EnemyShipShoot;


    private void Start()
    {
        fireTimer = 1 / fireRate;
    }


    private void Update()
    {
        if (OutOfBoundaries())
            BackToPool();

        if (fireTimer < 0)
        {
            Shoot();
            fireTimer = 1 / fireRate;
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
