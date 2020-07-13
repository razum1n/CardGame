using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherDeckCode : MonoBehaviour
{
    public enum TypeOfDeck { SKENARIO, QUESTION };
    public TypeOfDeck deckType;

    public UiScript ui;

    public GameObject card;
    public GameObject spawnPoint;

    public List<CardAsset> cardTemplates = new List<CardAsset>();

    public int price;
    private int questionNumber = 1;
    public int cardCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0;i<cardTemplates.Count;i++)
        {
            if(deckType == TypeOfDeck.QUESTION)
                cardTemplates[i] = Resources.Load<CardAsset>("Questions/Q" + (i + 1).ToString());
            else
                cardTemplates[i] = Resources.Load<CardAsset>("Questions/S" + (i + 1).ToString());
        }
        for (int i = 0; i < cardTemplates.Count; i++)
        {
            CardAsset temp = cardTemplates[i];
            int randomIndex = UnityEngine.Random.Range(i, cardTemplates.Count);
            cardTemplates[i] = cardTemplates[randomIndex];
            cardTemplates[randomIndex] = temp;
        }

        questionNumber = questionNumber + 1;
    }

    public void TryDrawing()
    {
        if(deckType == TypeOfDeck.SKENARIO && card.GetComponent<CardManager>().cardType == CardManager.TypeOfCard.SKENARIO && GameManager.Instance.HasPlayerDrawnSkenario())
        {
            Debug.Log("Card Already Drawn");
        }
        else if(deckType == TypeOfDeck.QUESTION && card.GetComponent<CardManager>().cardType == CardManager.TypeOfCard.QUESTION && GameManager.Instance.HasPlayerDrawnQuestion())
        {
            Debug.Log("Card Already Drawn");
        }
        else
        {
            SpawnCard();
        }
    }
    public void SpawnCard()
    {
        if (deckType == TypeOfDeck.QUESTION)
        {
            if (ui.bugCount < 1)
            {
                if ((GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].score >= price) && (cardCount < cardTemplates.Count))
                {
                    card.SetActive(true);
                    GameManager.Instance.sCard.SetActive(false);
                    GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].questionCardDrawn = true;
                    card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
                    card.GetComponent<CardManager>().ReadCardInfo();
                    cardCount++;
                    GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].score -= price;
                }
            }
            else
            {
                card.SetActive(true);
                GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].questionCardDrawn = true;
                GameManager.Instance.sCard.SetActive(false);
                card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
                card.GetComponent<CardManager>().ReadCardInfo();
                cardCount++;
            }
        }
        else if(deckType == TypeOfDeck.QUESTION)
        {
            if (cardCount < cardTemplates.Count)
            {
                card.SetActive(true);
                GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].questionCardDrawn = true;
                card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
                card.GetComponent<CardManager>().ReadCardInfo();
                cardCount++;
            }
        }
        else
        {
            if (cardCount < cardTemplates.Count)
            {
                card.SetActive(true);
                GameManager.Instance.qCard.SetActive(false);
                GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].scenarioCardDrawn = true;
                card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
                card.GetComponent<CardManager>().ReadCardInfo();
                cardCount++;
            }
        }
    }
}
