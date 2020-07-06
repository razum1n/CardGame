using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public CardAsset cardAsset;

    public TextMeshProUGUI question;
    public TextMeshProUGUI answerOne;
    public TextMeshProUGUI answerTwo;
    public TextMeshProUGUI answerThree;

    public GameObject cardFace;
    public GameObject cardCorrect;
    public GameObject cardFalse;

    private int answer;

    // Start is called before the first frame update
    void Awake()
    {
        ReadCardInfo();
    }

    public void ReadCardInfo()
    {
        question.text = cardAsset.Question;
        answerOne.text = cardAsset.OptionOneText;
        answerTwo.text = cardAsset.OptionTwoText;
        answerThree.text = cardAsset.OptionThreeText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OptionOne()
    {
        answer = 1;
        Answer();
    }

    public void OptionTwo()
    {
        answer = 2;
        Answer();
    }

    public void OptionThree()
    {
        answer = 3;
        Answer();
    }

    void Answer()
    {
        if(answer == cardAsset.CorrectAnswer)
        {
            cardFace.SetActive(false);
            cardCorrect.SetActive(true);
        }
        else if (answer != cardAsset.CorrectAnswer)
        {
            cardFace.SetActive(false);
            cardFalse.SetActive(true);
        }
    }
}
