using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherDeckCode : MonoBehaviour
{
    public enum TypeOfDeck { SKENARIO, QUESTION };
    public TypeOfDeck deckType;

    public GameObject card;
    public List<CardAsset> cardTemplates = new List<CardAsset>();
    public GameObject spawnPoint;
    public GameManager gameManager;
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


    public void SpawnCard()
    {
        if (deckType == TypeOfDeck.QUESTION)
        {
            if (gameManager.bugCount < 1)
            {
                if ((gameManager.players[gameManager.currentPlayer - 1].score >= price) && (cardCount < cardTemplates.Count))
                {
                    card.SetActive(true);
                    gameManager.sCard.SetActive(false);
                    card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
                    card.GetComponent<CardManager>().ReadCardInfo();
                    cardCount++;
                    gameManager.players[gameManager.currentPlayer - 1].score -= price;
                }
            }
            else
            {
                card.SetActive(true);
                gameManager.sCard.SetActive(false);
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
                gameManager.qCard.SetActive(false);
                card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
                card.GetComponent<CardManager>().ReadCardInfo();
                cardCount++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
