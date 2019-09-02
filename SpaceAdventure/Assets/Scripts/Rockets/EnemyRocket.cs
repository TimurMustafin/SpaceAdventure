using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket, IMovable
{
    LevelData levelData;

    private void Start()
    {
        levelData = GameMaster.Instance.LevelData;

        Speed = levelData.RocketSpeed;
        Damage = levelData.RocketDamage;
        lifeTime = levelData.RocketLifetime;
    }


    void Update()
    {
        if (OutOfBoundaries() || lifeTime < 0)
        {
            DestroyRocket();
        }

        lifeTime -= Time.deltaTime;

        //Move();
        transform.Translate(Vector3.down * Speed * Time.deltaTime, Space.Self);
    }

    public void Move()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime, Space.World);
    }

    public void Rotate()
    {
        //
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            PlayerStats.Health -= Damage;
            GameMaster.Instance.UpdateHealth();
        }      
        DestroyRocket();
    }

    void DestroyRocket()
    {
        GameObject effect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        Destroy(effect, 0.3f);
    }
}
