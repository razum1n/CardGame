using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class QuestionStealing : MonoBehaviour
{
    public GameObject timerBar;
    public CardManager questionCard;
    public int timer; // time before another player can answer a question.
    public GameObject playerIconPanel;
    public GameObject[] playerButtons;

    public void Timer()
    {
        LeanTween.scaleX(timerBar, 1, timer).setOnComplete(ActivateQuestionStealing); // scales between 0 and 1 and calls a function when it reaches 1.
    }


    // called when timer has reached its end.
    public void ActivateQuestionStealing()
    {
        playerIconPanel.SetActive(true);
        questionCard.questionStealing = true;
        questionCard.playerSteal = GameManager.Instance.currentPlayer - 1;

        // activates buttons that are used to give the answering chance to different players.
        for(int i=0;i<GameManager.Instance.numberOfPlayers;i++)
        {
            playerButtons[i].SetActive(true);
        }
    }

    // called when question stealing chance is deactivated.
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

    // button calls a specific function depending on which player button was pushed.
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

    // pauses the time if players manages to answer before it runs out.
    public void Pause()
    {
        LeanTween.pauseAll();
    }
}
