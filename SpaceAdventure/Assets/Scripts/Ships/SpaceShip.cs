using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : SpaceObject
{
    public GameObject rocketPrefab;
    public Transform[] FirePoints;
    protected float FireRate;
    protected float FireTimer;

    protected abstract void Shoot();
}
