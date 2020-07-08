using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI currentPlayerScoreText;
    public TextMeshProUGUI currentPlayerWaste;
    public Transform fosforBar;
    public float fosforAmmount;
    public GameObject currentCard;

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
        currentPlayerText.text = "Player " + currentPlayer;
        currentPlayerScoreText.text = players[currentPlayer-1].score.ToString();
        currentPlayerWaste.text = players[currentPlayer - 1].waste.ToString();

    }

    public void UpdateScore(int newScore)
    {
        players[currentPlayer - 1].score += newScore;
    }

    public void UpdateWaste(int newWaste)
    {
        if ((players[currentPlayer - 1].waste += newWaste) < 0)
            players[currentPlayer - 1].waste = 0;
        else
        players[currentPlayer - 1].waste += newWaste;
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
    }

    public void DestroyCurrentCard()
    {
        Destroy(currentCard);
    }

}
