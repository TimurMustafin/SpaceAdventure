using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Game")]
    public int GeneratorRate;
    public int PointsToWin;


    [Header("Player")]
    public float PlayerSpeed;
    public float PlayerFirerate;

    [Header("Enemy Ships")]
    public float Ship1Speed;
    public float Ship1Firerate;
    public float Ship1VibrationAmp;
    public float Ship1VibrationFreq;
    public int Ship1Hitpoints;

    public float Ship2Speed;
    public float Ship2Firerate;
    public float Ship2VibrationAmp;
    public float Ship2VibrationFreq;
    public int Ship2Hitpoints;

    public float Ship3Speed;
    public float Ship3Firerate;
    public float Ship3VibrationAmp;
    public float Ship3VibrationFreq;
    public int Ship3Hitpoints;

    [Header("Asteroids")]
    public float AsteroidSmallSpeed;
    public float AsteroidSmallRotationSpeed;
    public int AsteroidSmallDamage;

    public float AsteroidMediumSpeed; 
    public float AsteroidMediumRotationSpeed;
    public int AsteroidMediumDamage;
    
    public float AsteroidBigSpeed;
    public float AsteroidBigRotationSpeed;
    public int AsteroidBigDamage;

    [Header("Rocket")]
    public float RocketSpeed;
    public float RocketLifetime;
    public int RocketDamage;

}
