﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject DeathScreen;
    public GameObject player;

    private GeneralCounts Counts;

    private bool GameIsPaused = false;
    private bool WaitPause = false;

    void Start()
    {
        Counts = SaveSystem.GetInstance().generalCounts;
        DeathScreen.SetActive(false);
        PauseMenuUI.SetActive(false);
    }

    
    void Update()
    {
        Counts.TotalPlayTime += Time.deltaTime;
        if(GeneralCounts.Kill && !DeathScreen.activeSelf) {
            Death();
        }

        if(Input.GetAxisRaw("Pause") == 1 && WaitPause == false)
        {
            if(GameIsPaused)
            {
                Resume();
                StartCoroutine(KeepPaused());
            }
            else if(!GameIsPaused)
            {
                Pause();
                StartCoroutine(KeepPaused());
            }
        }
    }

    public void Death() {
        player.GetComponent<RagdollController>().DoRagdoll(true);
        player.GetComponent<MovimentPlayer>().enabled = false;
        DeathScreen.SetActive(true);
        Counts.DeathCount++;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    IEnumerator KeepPaused() {
        WaitPause = true;
        yield return new WaitForSecondsRealtime(0.5f);
        WaitPause = false;
    }
}
