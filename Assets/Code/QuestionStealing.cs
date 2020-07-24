using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class QuestionStealing : MonoBehaviour
{
    public GameObject timerBar;
    public CardManager questionCard;
    public int timer;
    public GameObject playerIconPanel;
    public GameObject[] playerButtons;

    public void Timer()
    {
        LeanTween.scaleX(timerBar, 1, timer).setOnComplete(ActivateQuestionStealing);
    }

    public void ActivateQuestionStealing()
    {
        playerIconPanel.SetActive(true);
        questionCard.questionStealing = true;
        questionCard.playerSteal = GameManager.Instance.currentPlayer - 1;

        for(int i=0;i<GameManager.Instance.numberOfPlayers;i++)
        {
            playerButtons[i].SetActive(true);
        }
    }

    public void DeActivateQuestionStealing()
    {
        playerIconPanel.SetActive(false);
        questionCard.questionStealing = false;
        LeanTween.scaleX(timerBar, 0, 0.5f);
        for (int i = 0; i < GameManager.Instance.numberOfPlayers; i++)
        {
            playerButtons[i].SetActive(false);
        }
    }

    public void NumberOne()
    {
        questionCard.playerSteal = 0;
    }
    public void NumberTwo()
    {
        questionCard.playerSteal = 1;
    }

    public void NumberThree()
    {
        questionCard.playerSteal = 2;
    }

    public void NumberFour()
    {
        questionCard.playerSteal = 3;
    }

    public void Pause()
    {
        LeanTween.pauseAll();
    }
}
