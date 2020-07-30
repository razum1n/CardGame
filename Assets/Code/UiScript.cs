using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField]
    private QuestionStealing steal;

    public GameObject playerPanel; // ui panel where all the player info is.

    public TextMeshProUGUI[] playerTexts;

    public GameObject sCard; // scenario card object
    public GameObject qCard; // question card object

    public GameObject cardBG;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject rulesMenu;

    public UnityEngine.UI.Image fosforBarObject;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI wasteText;

    private bool pauseActive = false;

    private bool rulesActive = false; // weather rules menu in pause is active.

    private bool doublePoints = false; // game halfway point double points.

    public bool fosforFull = false; // weather the "fosfor" bar is full.

    public TextMeshProUGUI currentPlayerText;

    public Transform spawnPoint;
    public Transform fosforBar;

    public float fosforAmmount = 0; // range is 0-1;

    private Color newColor = new Color(1f, 0f, 0f); // color of the bar at halfway point.

    // Start is called before the first frame update
    void Start()
    {
        playerTexts = new TextMeshProUGUI[GameManager.Instance.numberOfPlayers];
        GameManager.Instance.uiManager = this;
        GameManager.Instance.GameStart();
        ChangeFosforLevel(fosforAmmount);
        currentPlayerText.text = "Vuorossa " + GameManager.Instance.players[GameManager.Instance.currentPlayer-1].name;
    }

    private void Update()
    {
        // "pause" menu activated/deactivated with the escape key.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseActive = !pauseActive;
            rulesActive = false;
        }
        // activate/deactivate pause menu
        if (pauseActive)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);

        //rules menu in pause screen active/deactive.
        if (rulesActive)
            rulesMenu.SetActive(true);
        else
            rulesMenu.SetActive(false);

        // points info on question cards up to date.
        pointsText.text = qCard.GetComponent<CardManager>().score.ToString();
        wasteText.text = qCard.GetComponent<CardManager>().waste.ToString();

        // fosfor bar is full if amount value is 1.
        if (fosforAmmount >= 1)
            fosforFull = true;

        // card bg image active only if eather card is active.
        if (sCard.activeSelf || qCard.activeSelf)
            cardBG.SetActive(true);
        else
            cardBG.SetActive(false);
    }


    // Creates the player ui:s to the player panel at the top of the screen.
    public void CreatePlayerUi()
    {
        for (int i = 0; i < GameManager.Instance.numberOfPlayers; i++)
        {
            playerTexts[i] = playerPanel.GetComponentInChildren<TextMeshProUGUI>();
            playerTexts[i].text = GameManager.Instance.players[i].name;
            Instantiate(playerPanel, spawnPoint);
        }
        
    }

    // button functions.
    public void Resume()
    {
        pauseActive = !pauseActive;
    }

    public void BackToMenu()
    {
        GameManager.Instance.ResetPlayers();
        GameManager.Instance.numberOfPlayers = 2;
        GameManager.Instance.LoadMainMenu();
    }

    public void Rules()
    {
        rulesActive = !rulesActive;
    }

    public void PauseShutDown()
    {
        GameManager.Instance.ExitApplication();
    }

    // updates the fosfor levels.
    public void UpdateFosfor(float newFosforCount)
    {
        fosforAmmount += newFosforCount;

        if (fosforAmmount > 1) // keeps fosfor on 0-1 range.
            fosforAmmount = 1;
        else if (fosforAmmount < 0.0)
            fosforAmmount = 0;

        ChangeFosforLevel(fosforAmmount);
    }

    public void ChangeFosforLevel(float sizeNormalized)
    {
        // fills the bar the apropriate ammount.
        fosforBar.localScale = new Vector3(sizeNormalized, 1f);

        // if game is at halfway point "double" points is activated.
        if (fosforAmmount < 0.5)
            doublePoints = false;
        else
            doublePoints = true;

        // updates the points on question cards if double points is active.
        if (doublePoints)
        {
            qCard.GetComponent<CardManager>().waste = 6;
            qCard.GetComponent<CardManager>().score = 6;
            fosforBarObject.color = newColor;
        }

    }

    public void NextPlayer()
    {
        // resets player booleans to default values.
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].questionCardDrawn = false;
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].scenarioCardDrawn = false;
        GameManager.Instance.players[GameManager.Instance.currentPlayer -1].correctAnswer = false;

        // deactivates question stealing if active for the next player.
        steal.DeActivateQuestionStealing();

        // checks weather there are more players or the start of a new round
        if (GameManager.Instance.currentPlayer < GameManager.Instance.numberOfPlayers)
        {
            GameManager.Instance.currentPlayer++;
        }
        else
        {
            GameManager.Instance.currentPlayer = 1;
        }

        // resets cards to default positions.
        sCard.SetActive(false);
        qCard.SetActive(false);
        sCard.GetComponent<CardManager>().CardReset();
        qCard.GetComponent<CardManager>().CardReset();

        // updates the current player text.
        currentPlayerText.text = "Vuorossa " + GameManager.Instance.players[GameManager.Instance.currentPlayer-1].name;
    }

}
