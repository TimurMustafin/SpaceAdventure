using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket, IMovable
{
    private void Start()
    {
        LevelData levelData = GameMaster.Instance.levelData;
        Speed = levelData.RocketSpeed;
        LifeTime = levelData.RocketLifetime;
        Damage = levelData.RocketDamage;
    }

    void Update()
    {
        if (OutOfBoundaries() || LifeTime < 0)
        {
            DestroyRocket();
        }

        LifeTime -= Time.deltaTime;

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
