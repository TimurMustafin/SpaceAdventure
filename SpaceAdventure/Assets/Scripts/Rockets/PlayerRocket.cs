using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : Rocket, IMovable
{
    private void Start()
    {
        LevelData levelData = GameMaster.Instance.levelData;
        Speed = levelData.RocketSpeed;
        LifeTime = levelData.RocketLifetime;
        Damage = levelData.RocketDamage;
    }

    private void Update()
    {
        if (OutOfBoundaries() || LifeTime < 0)
        {
            DestroyRocket();
        }
        LifeTime -= Time.deltaTime;

        Move(); 
    }

    public void Move()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        

        DestroyRocket();
    }

    void DestroyRocket()
    {
        GameObject effect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        Destroy(effect, 0.3f);
    }

    public void Rotate()
    {
        //
    }
}
