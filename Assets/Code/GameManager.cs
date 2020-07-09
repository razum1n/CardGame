using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;
    public TextMeshProUGUI playerThreeScoreText;
    public TextMeshProUGUI playerFourScoreText;
    public TextMeshProUGUI playerOneWaste;
    public TextMeshProUGUI playerTwoWaste;
    public TextMeshProUGUI playerThreeWaste;
    public TextMeshProUGUI playerFourWaste;
    public Transform fosforBar;
    public float fosforAmmount;
    public GameObject currentCard;
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
        players = new Player[numberOfPlayers];
        for(int i=0;i<numberOfPlayers;i++)
        {
            players[i] = new Player();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCard == null)
        {
            currentCard = GameObject.FindGameObjectWithTag("Card");
        }
        currentPlayerText.text = "Vuorossa Pelaaja " + currentPlayer;
        playerOneScoreText.text = players[0].score.ToString();
        playerTwoScoreText.text = players[1].score.ToString();
        playerThreeScoreText.text = players[2].score.ToString();
        playerFourScoreText.text = players[3].score.ToString();
        playerOneWaste.text = players[0].waste.ToString();
        playerTwoWaste.text = players[1].waste.ToString();
        playerThreeWaste.text = players[2].waste.ToString();
        playerFourWaste.text = players[3].waste.ToString();

    }

    public void UpdateScore(int newScore)
    {
        players[currentPlayer - 1].score += newScore;
    }

    public void UpdateWaste(int newWaste)
    {
        if ((players[currentPlayer - 1].waste += (newWaste/2)) < 0)
            players[currentPlayer - 1].waste = 0;
        else
        players[currentPlayer - 1].waste += (newWaste/2);
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
        Destroy(currentCard);
        bugCount = 0;
    }

    public void DestroyCurrentCard()
    {
        if(currentCard.GetComponent<CardManager>().cardType == CardManager.TypeOfCard.QUESTION)
            bugCount++;

        Destroy(currentCard);

    }

}
