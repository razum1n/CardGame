using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public GameObject playerPanel;

    public TextMeshProUGUI[] playerTexts;

    public GameObject sCard;
    public GameObject qCard;
    public GameObject pauseMenu;
    public GameObject rulesMenu;
    public UnityEngine.UI.Image fosforBarObject;
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI wasteText;

    public bool pauseActive = false;
    public bool rulesActive = false;
    public bool doublePoints = false;

    public TextMeshProUGUI currentPlayerText;

    public Transform spawnPoint;
    public Transform fosforBar;

    public float fosforAmmount = 0;

    private Color newColor = new Color(0.7169812f, 0.09807763f, 0.09807763f);

    // Start is called before the first frame update
    void Start()
    {
        playerTexts = new TextMeshProUGUI[GameManager.Instance.numberOfPlayers];
        GameManager.Instance.uiManager = this;
        GameManager.Instance.GameStart();
        ChangeFosforLevel(fosforAmmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActive = !pauseActive;
            rulesActive = false;
        }

        if (pauseActive)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);

        if (rulesActive)
            rulesMenu.SetActive(true);
        else
            rulesMenu.SetActive(false);

        pointsText.text = qCard.GetComponent<CardManager>().score.ToString();
        wasteText.text = qCard.GetComponent<CardManager>().waste.ToString();
    }

    public void CreatePlayerUi()
    {
        for (int i = 0; i < GameManager.Instance.numberOfPlayers; i++)
        {
            playerTexts[i] = playerPanel.GetComponentInChildren<TextMeshProUGUI>();
            playerTexts[i].text = "Pelaaja " + (i + 1).ToString();
            Instantiate(playerPanel, spawnPoint);
        }
        
    }

    public void Resume()
    {
        pauseActive = !pauseActive;
    }

    public void BackToMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void Rules()
    {
        rulesActive = !rulesActive;
    }

    public void PauseShutDown()
    {
        GameManager.Instance.ExitApplication();
    }

    public void UpdateFosfor(float newFosforCount)
    {
        fosforAmmount += newFosforCount;
        if (fosforAmmount > 1)
            fosforAmmount = 1;
        else if (fosforAmmount < 0.0)
            fosforAmmount = 0;
        ChangeFosforLevel(fosforAmmount);
    }

    public void ChangeFosforLevel(float sizeNormalized)
    {
        fosforBar.localScale = new Vector3(sizeNormalized, 1f);
        if (fosforAmmount < 0.5)
            doublePoints = false;
        else
            doublePoints = true;

        if (doublePoints)
        {
            qCard.GetComponent<CardManager>().waste = 6;
            qCard.GetComponent<CardManager>().score = 6;
            fosforBarObject.color = newColor;
        }

    }

    public void NextPlayer()
    {
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].questionCardDrawn = false;
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].scenarioCardDrawn = false;
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].correctAnswer = false;

        if (GameManager.Instance.currentPlayer < GameManager.Instance.numberOfPlayers)
        {
            GameManager.Instance.currentPlayer++;
        }
        else
        {
            GameManager.Instance.currentPlayer = 1;
        }

        sCard.SetActive(false);
        qCard.SetActive(false);
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();
        currentPlayerText.text = "Vuorossa Pelaaja " + GameManager.Instance.currentPlayer;
    }

    public void DestroyCurrentCard()
    {
        qCard.SetActive(false);
        sCard.SetActive(false);
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();
    }

}
