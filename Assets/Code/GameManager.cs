using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        players = new Player[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = new Player();
        }
        FindPlayerUi();
        qCard.SetActive(false);
        sCard.SetActive(false);
    }

    void FindPlayerUi()
    {
        listOfPlayerScoreUi = GameObject.FindGameObjectsWithTag("PlayerScore");
        listOfPlayerWasteUi = GameObject.FindGameObjectsWithTag("PlayerWaste");
    }

    public void UpdateUi()
    {
        for(int i=0;i<numberOfPlayers;i++)
        {
            listOfPlayerScoreUi[i].GetComponent<TextMeshProUGUI>().text = players[i].score.ToString();
            listOfPlayerWasteUi[i].GetComponent<TextMeshProUGUI>().text = players[i].waste.ToString();
        }
    }

    public void UpdateScore(int newScore)
    {
        players[currentPlayer - 1].score += newScore;
        if (players[currentPlayer - 1].score < 0)
            players[currentPlayer - 1].score = 0;
        UpdateUi();
    }

    public void UpdateWaste(int newWaste)
    {
        if ((players[currentPlayer - 1].waste += (newWaste/2)) < 0)
            players[currentPlayer - 1].waste = 0;
        else
        players[currentPlayer - 1].waste += (newWaste/2);
        UpdateUi();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

}
