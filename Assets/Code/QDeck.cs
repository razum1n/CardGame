using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QDeck : MonoBehaviour
{
    public GameObject Card;
    public GameObject cardInPlay;
    public List<GameObject> deckOfCards = new List<GameObject>();
    public GameObject spawnPoint;
    private int questionNumber = 0;
    private int cardNumber = 0;

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
    }


    public void SpawnCard()
    {
        if (cardNumber < deckOfCards.Count)
        {
            if (cardNumber > 0)
                Destroy(cardInPlay);
            cardInPlay = Instantiate(deckOfCards[cardNumber], spawnPoint.transform.position, spawnPoint.transform.rotation);
            if(deckOfCards[cardNumber].GetComponent<CardManager>().cardType == CardManager.TypeOfCard.QUESTION)
                deckOfCards[cardNumber].GetComponent<CardManager>().cardAsset = Resources.Load<CardAsset>("Questions/Q" + questionNumber.ToString());
            else
                deckOfCards[cardNumber].GetComponent<CardManager>().cardAsset = Resources.Load<CardAsset>("Questions/S" + questionNumber.ToString());
            questionNumber++;
            cardNumber++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
