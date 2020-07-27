using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherDeckCode : MonoBehaviour
{
    public enum TypeOfDeck { SKENARIO, QUESTION }; // what type of deck is in question.
    public TypeOfDeck deckType;

    [SerializeField]
    private UiScript ui;

    [SerializeField]
    private GameStatus gameStatus;

    [SerializeField]
    private QuestionStealing steal;

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private GameObject otherCard;

    public List<CardAsset> cardTemplates = new List<CardAsset>();

    [SerializeField]
    private int price;

    private int questionNumber = 1;

    [SerializeField]
    private int cardCount = 0;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0;i<cardTemplates.Count;i++)
        {
            if(deckType == TypeOfDeck.QUESTION)
                cardTemplates[i] = Resources.Load<CardAsset>("Questions/Questions/Q" + (i + 1).ToString());
            else
                cardTemplates[i] = Resources.Load<CardAsset>("Questions/Skenarios/S" + (i + 1).ToString());
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
        if(cardCount > (cardTemplates.Count - 1)) // are there any cards to draw
        {
            gameStatus.gameEnd = true;
        }
        else
        {
            if (deckType == TypeOfDeck.SKENARIO && card.GetComponent<CardManager>().cardType == CardManager.TypeOfCard.SKENARIO)
            {
                if (GameManager.Instance.HasPlayerDrawnSkenario())
                {
                    Debug.Log("Card Already Drawn");
                }
                else
                {
                    SpawnCard();
                }
            }
            else
            {
                if (GameManager.Instance.HasPlayerDrawnQuestion() || (GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].correctAnswer == false))
                {
                    Debug.Log("Card Already Drawn or skenario card not correct");
                }
                else
                {
                    SpawnCard();
                }
            }
        }

    }

    public void SpawnCard()
    {
        if(deckType == TypeOfDeck.QUESTION)
        {
            card.SetActive(true);
            otherCard.SetActive(false);
            steal.Timer();
            GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].questionCardDrawn = true;
            card.GetComponent<CardManager>().cardAsset = cardTemplates[cardCount];
            card.GetComponent<CardManager>().ReadCardInfo();
            cardCount++;
        }
        else
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
