﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public int numberOfPlayersSelected = 2;

    public Dropdown dropdown; 

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

    public void ExitApplication()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        dropdown.onValueChanged.RemoveAllListeners();
    }
}