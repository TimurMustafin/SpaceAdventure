﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : SpaceObject
{
    public GameObject rocketPrefab;
    public Transform[] FirePoints;
    public float fireRate;
    protected float fireTimer;

    protected abstract void Shoot();
}
