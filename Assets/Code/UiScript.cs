using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiScript : MonoBehaviour
{
    public GameObject playerPanel;
    public TextMeshProUGUI[] playerTexts;
    public GameObject sCard;
    public GameObject qCard;

    public TextMeshProUGUI currentPlayerText;

    public Transform spawnPoint;
    public Transform fosforBar;

    public int bugCount;

    public float fosforAmmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerTexts = new TextMeshProUGUI[GameManager.Instance.numberOfPlayers];
        GameManager.Instance.uiManager = this;
        GameManager.Instance.GameStart();
        ChangeFosforLevel(fosforAmmount);
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
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].questionCardDrawn = false;
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].scenarioCardDrawn = false;
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
        bugCount = 0;
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();
        currentPlayerText.text = "Vuorossa Pelaaja " + GameManager.Instance.currentPlayer;
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
