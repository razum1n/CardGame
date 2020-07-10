using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public enum TypeOfCard {SKENARIO, QUESTION};

    public TypeOfCard cardType;

    public CardAsset cardAsset;

    public TextMeshProUGUI question;
    public TextMeshProUGUI answerOne;
    public TextMeshProUGUI answerTwo;
    public TextMeshProUGUI answerThree;
    public TextMeshProUGUI correctInfo;
    public TextMeshProUGUI wrongInfo;

    public GameObject cardFace;
    public GameObject cardCorrect;
    public GameObject cardFalse;

    public GameManager gameManager;

    public int score;

    public int waste;

    public int price;

    public float fosforPrice;

    private int answer;


    private void Start()
    {

    }

    public void ReadCardInfo()
    {
        if(cardType == TypeOfCard.QUESTION)
        {
            question.text = cardAsset.Question;
            answerOne.text = cardAsset.OptionOneText;
            answerTwo.text = cardAsset.OptionTwoText;
            answerThree.text = cardAsset.OptionThreeText;
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

    // Update is called once per frame
    void Update()
    {
        
    }

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
        if(cardType == TypeOfCard.QUESTION)
        {
            if (answer == cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardCorrect.SetActive(true);
                gameManager.UpdateScore(score);
                gameManager.UpdateFosfor(fosforPrice);
            }
            else if (answer != cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardFalse.SetActive(true);
                gameManager.UpdateWaste(waste);
            }

        }
        else
        {
            if (answer == cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardCorrect.SetActive(true);
                gameManager.UpdateScore(score);
                gameManager.UpdateWaste(-waste);
            }
            else if (answer != cardAsset.CorrectAnswer)
            {
                cardFace.SetActive(false);
                cardFalse.SetActive(true);
                gameManager.UpdateFosfor(-fosforPrice);
            }
        }
    }

    public void CardReset()
    {
        cardFace.SetActive(true);
        cardCorrect.SetActive(false);
        cardFalse.SetActive(false);
    }
}
