﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public int numberOfPlayersSelected = 2;

    public Dropdown dropdown;
    public GameObject mainMenu;
    public GameObject infoMenu;
    public GameObject rulesMenu;
    public GameObject playerNamesMenu;
    public GameObject[] inputFields;
    public TextMeshProUGUI[] playerNamesText;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.options.Clear();

        List<int> items = new List<int>();

        for(int i=2;i<5;i++)
        {
            items.Add(i);
        }

        foreach(var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item.ToString() });
        }

        dropdown.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdown); });
    }

    private void DropdownItemSelected(Dropdown dropdown)
    {
        numberOfPlayersSelected = dropdown.value + 2;
        GameManager.Instance.numberOfPlayers = numberOfPlayersSelected;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameStart()
    {
        playerNamesMenu.SetActive(true);
        mainMenu.SetActive(false);

        for (int i=0;i<inputFields.Length;i++)
        {
            inputFields[i].SetActive(false);
        }

        for (int i=0;i<numberOfPlayersSelected;i++)
        {
            inputFields[i].SetActive(true);
        }

    }

    public void UpdateNames()
    {
        for (int i=0;i<numberOfPlayersSelected;i++)
        {
            GameManager.Instance.players[i].name = playerNamesText[i].text;
        }
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }

    public void Back()
    {
        infoMenu.SetActive(false);
        rulesMenu.SetActive(false);
        mainMenu.SetActive(true);
        playerNamesMenu.SetActive(false);
    }

    public void Rules()
    {
        infoMenu.SetActive(false);
        mainMenu.SetActive(false);
        rulesMenu.SetActive(true);
    }

    public void Info()
    {
        infoMenu.SetActive(true);
        mainMenu.SetActive(false);
        rulesMenu.SetActive(false);
    }
}
