using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] listOfPlayerScoreUi;
    public GameObject[] listOfPlayerWasteUi;
    public UiScript uiManager;
    public Transform fosforBar;
    public float fosforAmmount;
    public GameObject qCard;
    public GameObject sCard;
    

    public class Player
    {
        public int score;
        public int waste;
        public bool scenarioCardDrawn = false;
        public bool questionCardDrawn = false; // checks weather the player has drawn a specific card this turn.
        public bool correctAnswer = false;
        public string name;

    }

    public Player[] players;
    public int numberOfPlayers;

    public static GameManager Instance { get; private set; }

    public int currentPlayer = 0;
    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
        DontDestroyOnLoad(this.gameObject);
    }

    public bool HasPlayerDrawnSkenario()
    {
        return players[currentPlayer - 1].scenarioCardDrawn;
    }

    public bool HasPlayerDrawnQuestion()
    {
        return players[currentPlayer - 1].questionCardDrawn;
    }

    public void GameStart()
    {
        uiManager.CreatePlayerUi();
        FindPlayerUi();
        qCard.SetActive(false);
        sCard.SetActive(false);
    }

    void FindPlayerUi()
    {
        listOfPlayerScoreUi = GameObject.FindGameObjectsWithTag("PlayerScore");
        listOfPlayerWasteUi = GameObject.FindGameObjectsWithTag("PlayerWaste");
    }

    public void CreatePlayers()
    {
        players = new Player[numberOfPlayers];

        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = new Player();
        }

    }

    public void UpdateUi()
    {
        for(int i=0;i<numberOfPlayers;i++)
        {
            listOfPlayerScoreUi[i].GetComponent<TextMeshProUGUI>().text = players[i].score.ToString();
            listOfPlayerWasteUi[i].GetComponent<TextMeshProUGUI>().text = players[i].waste.ToString();
        }
    }

    public void UpdateScore(int newScore, bool playerSteal = false, int playerNumber = 0)
    {
        if (!playerSteal)
        {
            players[currentPlayer - 1].score += newScore;
            if (players[currentPlayer - 1].score < 0)
                players[currentPlayer - 1].score = 0;
            UpdateUi();
        }
        else if (playerSteal)
        {
            players[playerNumber].score += newScore;
            if (players[playerNumber].score < 0)
                players[playerNumber].score = 0;
            UpdateUi();
        }
    }

    public void UpdateWaste(int newWaste, bool playerSteal = false, int playerNumber = 0)
    {
        if (!playerSteal)
        {
            players[currentPlayer - 1].waste = players[currentPlayer - 1].waste + newWaste;
            if (players[currentPlayer - 1].waste < 0)
                players[currentPlayer - 1].waste = 0;
            UpdateUi();
        }
        else if (playerSteal)
        {
            players[playerNumber].waste = players[playerNumber].waste + newWaste;
            if (players[playerNumber].waste < 0)
                players[playerNumber].waste = 0;
            UpdateUi();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

}
