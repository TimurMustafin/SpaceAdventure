using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : DamagingSpaceObject, IMovable, IPoolReturnable
{

    LevelData levelData;

    private void Start()
    {
        
        levelData = GameMaster.Instance.LevelData;
        switch (IdTag)
        {
            case "AsterSmall":
                Speed = levelData.SmallAsteroidSpeed;
                Damage = levelData.SmallAsteroidDamage;
                break;
            case "AsterMedium":
                Speed = levelData.MediumAsteroidSpeed;
                Damage = levelData.MediumAsteroidDamage;
                break;
            case "AsterBig":
                Speed = levelData.BigAsteroidSpeed;
                Damage = levelData.BigAsteroidDamage;
                break;
        }

    }


    private void Update()
    {
		if (OutOfBoundaries())
		{
            BackToPool();
		}

		Move();
        Rotate();
    }    

    public void Move()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime, Space.World);
    }
  
    void OnCollisionEnter2D(Collision2D collision)
    {      

		if (collision.collider.tag == "PlayerRocket")
		{
            GameMaster.Instance.Explostion();
            BackToPool(); 
        }

		if (collision.collider.tag == "Player")
		{
			PlayerStats.Health -= Damage;
            GameMaster.Instance.UpdateHealth();
		}


	}

    public void Rotate()
    {
        transform.Rotate(0, 0, RotationSpeed);
    }

    public void BackToPool()
    {
        gameObject.SetActive(false);
        EnemyGenerator.Instance.DictionaryEnemyPool[IdTag].Enqueue(gameObject);
    }



}
