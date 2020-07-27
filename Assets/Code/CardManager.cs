using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public enum TypeOfCard {SKENARIO, QUESTION}; // type of the card.

    public TypeOfCard cardType;

    public CardAsset cardAsset; // card asset that has all the card info.

    public QuestionStealing stealing;

    // card info that is loaded from the card asset.
    public TextMeshProUGUI question;
    public TextMeshProUGUI answerOne;
    public TextMeshProUGUI answerTwo;
    public TextMeshProUGUI answerThree;
    public TextMeshProUGUI correctInfo;
    public TextMeshProUGUI wrongInfo;

    // object that hold different text objects as child.
    public GameObject cardFace;
    public GameObject cardCorrect;
    public GameObject cardFalse;

    public int score; // how much points are awarded for right answers

    public int waste; // how much waste is generated from incorrect answers.

    public float fosforPrice; // how much fosfor is gained from correct answers.

    private int answer; // what is the players answer.

    public int correctAnswer; // what is the correct answer.

    public bool questionStealing = false; // has the time ran out (only on question card types).

    public int playerSteal; // who is answering/stealing the question (only on question card types).

    private void Awake()
    {
        if (cardType == TypeOfCard.QUESTION)
        {
            GameManager.Instance.qCard = this.gameObject;
        }
        else if (cardType == TypeOfCard.SKENARIO)
        {
            GameManager.Instance.sCard = this.gameObject;
        }
    }

    // reads the card info from the asset object.
    public void ReadCardInfo()
    {
        if(cardType == TypeOfCard.QUESTION)
        {
            question.text = cardAsset.Question;
            answerOne.text = cardAsset.OptionOneText;
            answerTwo.text = cardAsset.OptionTwoText;
            answerThree.text = cardAsset.OptionThreeText;
            correctAnswer = cardAsset.CorrectAnswer;

            if (correctAnswer == 1)
            {
                correctInfo.text = cardAsset.OptionOneText;
                wrongInfo.text = cardAsset.OptionOneText;
            }
            else if (correctAnswer == 2)
            {
                correctInfo.text = cardAsset.OptionTwoText;
                wrongInfo.text = cardAsset.OptionTwoText;
            }
            else if (correctAnswer == 3)
            {
                correctInfo.text = cardAsset.OptionThreeText;
                wrongInfo.text = cardAsset.OptionThreeText;
            }
        }
        else if(cardType == TypeOfCard.SKENARIO)
        {
            question.text = cardAsset.Question;
            answerOne.text = cardAsset.OptionOneText;
            answerTwo.text = cardAsset.OptionTwoText;
            correctInfo.text = cardAsset.Reasoning;
            wrongInfo.text = cardAsset.Reasoning;
        }

    }

    // button functions for different options.
    public void OptionOne()
    {
        answer = 1;
        Answer();
        answer = 0;
    }

    public void OptionTwo()
    {
        answer = 2;
        Answer();
        answer = 0;
    }

    public void OptionThree()
    {
        answer = 3;
        Answer();
        answer = 0;
    }

    void Answer()
    {

        // different effects for question and scenario cards.
        if(cardType == TypeOfCard.QUESTION)
        {
            if (answer == cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardCorrect.SetActive(true);
                GameManager.Instance.UpdateScore(score, questionStealing,playerSteal);
                GameManager.Instance.uiManager.UpdateFosfor(fosforPrice);
                stealing.Pause();
            }
            else if (answer != cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardFalse.SetActive(true);
                GameManager.Instance.UpdateWaste(waste, questionStealing, playerSteal);
                stealing.Pause();
            }

        }
        else // scenario card.
        {
            if (answer == cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardCorrect.SetActive(true);
                GameManager.Instance.UpdateScore(score);
                GameManager.Instance.UpdateWaste(-waste); // removes waste from the player.
                GameManager.Instance.uiManager.UpdateFosfor(fosforPrice);
                GameManager.Instance.players[GameManager.Instance.currentPlayer - 1].correctAnswer = true;
            }
            else if (answer != cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardFalse.SetActive(true);
            }
        }
    }

    // used to reset the cards to default positions.
    public void CardReset()
    {
        cardFace.SetActive(true);
        cardCorrect.SetActive(false);
        cardFalse.SetActive(false);
    }
}
