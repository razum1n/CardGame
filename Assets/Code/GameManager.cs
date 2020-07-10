using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerText;
    public GameObject[] listOfPlayerScoreUi;
    public GameObject[] listOfPlayerWasteUi;
    public UiScript uiManager;
    public Transform fosforBar;
    public float fosforAmmount;
    public GameObject qCard;
    public GameObject sCard;
    public int bugCount;

    public class Player
    {
        public int score;
        public int waste;

    }

    public Player[] players;
    public int numberOfPlayers;

    public int currentPlayer = 1;

    // Start is called before the first frame update
    void Start()
    {
        uiManager.CreatePlayerUi();
        players = new Player[numberOfPlayers];
        for(int i=0;i<numberOfPlayers;i++)
        {
            players[i] = new Player();
        }
        FindPlayerUi();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerText.text = "Vuorossa Pelaaja " + currentPlayer;
    }

    void FindPlayerUi()
    {
        listOfPlayerScoreUi = GameObject.FindGameObjectsWithTag("PlayerScore");
        listOfPlayerWasteUi = GameObject.FindGameObjectsWithTag("PlayerWaste");
    }

    void UpdateUi()
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

    public void UpdateFosfor(float newFosforCount)
    {
        fosforAmmount += newFosforCount;
        if (fosforAmmount > 1)
            fosforAmmount = 1;
        else if (fosforAmmount < 0.0)
            fosforAmmount = 0;
        ChangeFosforLevel(fosforAmmount);
        UpdateUi();
    }

    public void ChangeFosforLevel(float sizeNormalized)
    {
        fosforBar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void NextPlayer()
    {
        if(currentPlayer < numberOfPlayers)
        {
            currentPlayer++;
        }
        else
        {
            currentPlayer = 1;
        }
        sCard.SetActive(false);
        qCard.SetActive(false);
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();
        bugCount = 0;
    }

    public void DestroyCurrentCard()
    {
        if (qCard.activeSelf)
            bugCount++;
        qCard.SetActive(false);
        sCard.SetActive(false);
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();

    }

}
