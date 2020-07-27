using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [SerializeField]
    private UiScript gameUi;

    [SerializeField]
    private TextMeshProUGUI[] playerNames = new TextMeshProUGUI[4];

    [SerializeField]
    private TextMeshProUGUI[] playerScores = new TextMeshProUGUI[4];

    [SerializeField]
    private GameObject gameEndScreen;

    public bool gameEnd;
    private bool finalScoresCounted = false;

    private int[] playerFinalScore = new int[4];


    private void Start()
    {
        for(int i=0;i<GameManager.Instance.numberOfPlayers;i++)
        {
            playerNames[i].text = GameManager.Instance.players[i].name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameUi.fosforFull || gameEnd)
        {
            gameEndScreen.SetActive(true);
            if (!finalScoresCounted)
                finalScoresCounted = FinalScores();

        }
        else
        {
            gameEndScreen.SetActive(false);
        }

    }


    // restarts the game. keeps the names of the players.
    public void Restart()
    {
        for(int i=0;i<GameManager.Instance.numberOfPlayers;i++)
        {
            GameManager.Instance.players[i].score = 0;
            GameManager.Instance.players[i].waste = 0;
            GameManager.Instance.players[i].questionCardDrawn = false;
            GameManager.Instance.players[i].scenarioCardDrawn = false;
            GameManager.Instance.currentPlayer = 1;
            GameManager.Instance.ReloadScene();
        }
    }

    // counts the final scores and updates the ui accordingly.
    bool FinalScores()
    {
        for(int i=0;i<GameManager.Instance.numberOfPlayers;i++)
        {
            if (GameManager.Instance.players[i].score - GameManager.Instance.players[i].waste < 0)
                playerFinalScore[i] = 0;
            else
                playerFinalScore[i] = GameManager.Instance.players[i].score - GameManager.Instance.players[i].waste;

            playerScores[i].text = playerFinalScore[i].ToString();
        }
        return true;
    }
}
