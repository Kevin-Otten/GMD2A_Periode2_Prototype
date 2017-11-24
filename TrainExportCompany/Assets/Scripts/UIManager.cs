﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //Enum
    public enum UIState
    {
        PauseMenu,
        Train,
        Station
    }
    public UIState uiState;

    //Pannels
    public GameObject pausePanel;
    public GameObject trainPanel;
    public GameObject stationPanel;
    private GameObject lastPanel;

    void Start()
    {
        ChangeUIState();
    }

    void Update()
    {
        if(Input.GetButtonDown("Escape"))
        {
            print("Escape");
            ChangeUIState();
        }
    }

    //UI Chance
    public void ChangeUIState()
    {
            if (uiState == UIState.Train)
            {
                uiState = UIState.PauseMenu;
            }
            else if (uiState == UIState.Station)
            {
                uiState = UIState.Train;
            }
            else if (uiState == UIState.PauseMenu)
            {
            Time.timeScale = 1;
                uiState = UIState.Train;
            }
            ChangeUI();
    }

    public void ChangeUI()
    {
        //Valnet over een null refrence error
        if(lastPanel != null)
        {
            lastPanel.SetActive(false);
        }


        if (uiState == UIState.PauseMenu)
        {
            pausePanel.SetActive(true);
            lastPanel = pausePanel;
            Time.timeScale = 0;
        }
        else if (uiState == UIState.Train)
        {
            trainPanel.SetActive(true);
            lastPanel = trainPanel;
        }
        else if (uiState == UIState.Station)
        {
            stationPanel.SetActive(true);
            lastPanel = stationPanel;
        }
    }

    //UI PauseMenu
    public void ReturnToGame()
    {
        Time.timeScale = 1;
        ChangeUIState();
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        GameManager.SceneChance(0);
    }

    //UI Train


    //UI Station

}
