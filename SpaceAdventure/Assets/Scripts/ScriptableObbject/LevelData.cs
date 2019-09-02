using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Game")]
    public int PointsToWin;
    public int GenerationRate;
    


    [Header("Player")]
    public float PlayerSpeed;
    public float PlayerFirerate;


    [Header("Asteroids")]
    
    public float SmallAsteroidSpeed;
    public float MediumAsteroidSpeed;
    public float BigAsteroidSpeed;


    public int SmallAsteroidDamage;
    public int MediumAsteroidDamage;
    public int BigAsteroidDamage;

    [Header("Ships")]
    
    public float ShipOneSpeed;
    public float ShipTwoSpeed;
    public float ShipThreeSpeed;

    [Header("Ship's vibration")]
    public float ShipOneAmp;
    public float ShipTwoAmp;
    public float ShipThreeAmp;

    public float ShipOneFreq;
    public float ShipTwoFreq;
    public float ShipThreeFreq;


    public float ShipOneFirerate;
    public float ShipTwoFirerate;
    public float ShipThreeFirerate;

    [Header("Rocket")]
    public float RocketSpeed;
	public float RocketLifetime;
    public int RocketDamage;
}

