﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameMaster : MonoBehaviour
{
    [Header("LevelData")]
    public LevelData LevelData;

    [Header("UI")]
    public Text HeathText;
    public Slider HealthBar;
    public Text HitPointText;
    public Slider HitpointsBar;

    public Text CountdownText;
    public GameObject GameOverUI;
    public GameObject YouWinUI;
    public GameObject HUD;

    
    int PointsToWin;

    [Header("Sound")]
    public AudioSource ExplosionSound;
    public AudioSource StartGameSound;
    public AudioSource Soundtrack;
    

    [HideInInspector]
    public bool GameStarted;
    
    bool isPaused;

    #region Singleton

    public static GameMaster Instance;   

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    

    private void Start()
    {
        Time.timeScale = 1f;
        PointsToWin = LevelData.PointsToWin;
        PlayerStats.Health = 100;
        PlayerStats.HitPoints = 0;
        HealthBar.value = PlayerStats.Health;
        HitpointsBar.value = PlayerStats.HitPoints;
        isPaused = false;
        GameStarted = false;
        
        StartCoroutine(CountDown());
    }

    void Update()
    {
        if (PlayerStats.Health <= 0)
            GameOver();
        if (PlayerStats.HitPoints >= PointsToWin)
            YouWin();
    }

    public void UpdateHealth()
    {
        HeathText.text = PlayerStats.Health.ToString();
        HealthBar.value = PlayerStats.Health;
        //Debug.Log(HealthBar.value);


    }

    public void UpdateHitPoint()
    {
        HitPointText.text = PlayerStats.HitPoints.ToString();
        HitpointsBar.value = PlayerStats.HitPoints;
    }
    
    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;

        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
        }


    }

    public void YouWin()
    {
        YouWinUI.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0.2f;
    }

    public void GameOver()
    {
        
        GameOverUI.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0.2f;
        Soundtrack.Stop();
        
    }

    public void Retry()
    {
        PlayerStats.Health = 100;
        PlayerStats.HitPoints = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Explostion()
    {
        ExplosionSound.Play();
    }

    IEnumerator CountDown()
    {
        float timer = 3;
        while (timer > 0)
        {
            CountdownText.text = Mathf.Clamp(Mathf.Round(timer), 1, 3).ToString();

            timer -= Time.deltaTime;

            yield return null;
        }
        CountdownText.enabled = false;
        GameStarted = true;
        StartGameSound.Play();
        Soundtrack.Play();
    }
}
