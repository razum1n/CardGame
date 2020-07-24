using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public QuestionStealing steal;
    public GameObject playerPanel;
    public GameObject timerBar;

    public TextMeshProUGUI[] playerTexts;

    public GameObject sCard;
    public GameObject qCard;
    public GameObject cardBG;
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

    private Color newColor = new Color(1f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        playerTexts = new TextMeshProUGUI[GameManager.Instance.numberOfPlayers];
        GameManager.Instance.uiManager = this;
        GameManager.Instance.GameStart();
        ChangeFosforLevel(fosforAmmount);
        currentPlayerText.text = "Vuorossa " + GameManager.Instance.players[GameManager.Instance.currentPlayer-1].name;
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

        if (sCard.activeSelf || qCard.activeSelf)
            cardBG.SetActive(true);
        else
            cardBG.SetActive(false);
    }

    public void CreatePlayerUi()
    {
        for (int i = 0; i < GameManager.Instance.numberOfPlayers; i++)
        {
            playerTexts[i] = playerPanel.GetComponentInChildren<TextMeshProUGUI>();
            playerTexts[i].text = GameManager.Instance.players[i].name;
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
        steal.DeActivateQuestionStealing();

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
        currentPlayerText.text = "Vuorossa " + GameManager.Instance.players[GameManager.Instance.currentPlayer-1].name;
    }

    public void DestroyCurrentCard()
    {
        qCard.SetActive(false);
        sCard.SetActive(false);
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();
    }

}
