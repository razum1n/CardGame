using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QDeck : MonoBehaviour
{
    public enum TypeOfDeck { SKENARIO, QUESTION };
    public TypeOfDeck deckType;

    public GameObject Card;
    public GameObject cardInPlay;
    public List<GameObject> deckOfCards = new List<GameObject>();
    public GameObject spawnPoint;
    public UiScript ui;

    public int price;
    private int questionNumber = 0;
    public int cardNumber = 0;

    // Start is called before the first frame update
    void Start()
    {

        for(int i=0;i<deckOfCards.Count;i++)
        {
            if (deckOfCards[i] == null)
                deckOfCards[i] = Card;
            
            questionNumber++;
        }
        questionNumber = 1;

        cardNumber = deckOfCards.Count - 1;
    }


    public void SpawnCard()
    {
        if(deckType == TypeOfDeck.QUESTION)
        {
            if(ui.bugCount < 1)
            {
                if ((GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].score >= price) && (cardNumber < deckOfCards.Count))
                {
                    GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].score -= price;
                    Spawning();
                }
            }
            else
            {
                Spawning();
            }

        }
        else
        {
            if (cardNumber < deckOfCards.Count)
            {
                Spawning();
            }
        }



    }

    private void Spawning()
    {
        if(cardNumber >= 0)
        {
            cardInPlay = Instantiate(deckOfCards[cardNumber], spawnPoint.transform.position, spawnPoint.transform.rotation);
            if (deckOfCards[cardNumber].GetComponent<CardManager>().cardType == CardManager.TypeOfCard.QUESTION)
            {
                deckOfCards[cardNumber].GetComponent<CardManager>().cardAsset = Resources.Load<CardAsset>("Questions/Q" + questionNumber.ToString());
                deckOfCards.RemoveAt(cardNumber);
            }

            else
            {
                deckOfCards[cardNumber].GetComponent<CardManager>().cardAsset = Resources.Load<CardAsset>("Questions/S" + questionNumber.ToString());
                deckOfCards.RemoveAt(cardNumber);
            }
            questionNumber += 1;
            cardNumber = cardNumber - 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
