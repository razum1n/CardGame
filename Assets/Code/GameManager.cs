using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] listOfPlayerScoreUi; // array of player score ui elements.
    public GameObject[] listOfPlayerWasteUi;
    public UiScript uiManager;
    public GameObject qCard; // question card object.
    public GameObject sCard; // scenario card object.
    

    public class Player
    {
        public int score; // players score
        public int waste; // players waste
        public bool scenarioCardDrawn = false; // used to check weather player has drawn a specific card this turn.
        public bool questionCardDrawn = false;
        public bool correctAnswer = false; // used to check weather player answered correctly.
        public string name; // players name.

    }

    public Player[] players; // array of player objects.
    public int numberOfPlayers; // number of players in game.

    public static GameManager Instance { get; private set; }

    public int currentPlayer = 1; // whose turn it is.

    void Awake()
    {
        // create an instance of GameManager.
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
        DontDestroyOnLoad(this.gameObject);
    }


    // check for drawn cards. Returns true if drawn.
    public bool HasPlayerDrawnSkenario()
    {
        return players[currentPlayer - 1].scenarioCardDrawn;
    }

    public bool HasPlayerDrawnQuestion()
    {
        return players[currentPlayer - 1].questionCardDrawn;
    }


    // called at the start of the game.
    public void GameStart()
    {
        uiManager.CreatePlayerUi();
        FindPlayerUi();
        qCard.SetActive(false);
        sCard.SetActive(false);
    }

    // finds the object and assings them to the arrays.
    void FindPlayerUi()
    {
        listOfPlayerScoreUi = GameObject.FindGameObjectsWithTag("PlayerScore");
        listOfPlayerWasteUi = GameObject.FindGameObjectsWithTag("PlayerWaste");
    }


    //Creates a number of player object.
    public void CreatePlayers()
    {
        players = new Player[numberOfPlayers];

        for (int i = 0; i < numberOfPlayers; i++)
        {
            players[i] = new Player();
        }

    }

    // updates the ui with current scores and waste.
    public void UpdateUi()
    {
        for(int i=0;i<numberOfPlayers;i++)
        {
            listOfPlayerScoreUi[i].GetComponent<TextMeshProUGUI>().text = players[i].score.ToString();
            listOfPlayerWasteUi[i].GetComponent<TextMeshProUGUI>().text = players[i].waste.ToString();
        }
    }

    // updates the score of the current player.
    public void UpdateScore(int newScore, bool playerSteal = false, int playerNumber = 0)
    {
        if (!playerSteal) // checks weather another player has chosen to answer the question
        {
            players[currentPlayer - 1].score += newScore;
            if (players[currentPlayer - 1].score < 0)
                players[currentPlayer - 1].score = 0;
            UpdateUi();
        }
        else if (playerSteal) // if another player has chosen to answer the score is then given to them.
        {
            players[playerNumber].score += newScore;
            if (players[playerNumber].score < 0)
                players[playerNumber].score = 0;
            UpdateUi();
        }
    }

    // works the same way as UpdateScore function.
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

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
