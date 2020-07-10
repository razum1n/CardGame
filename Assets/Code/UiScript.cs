using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    public GameObject playerPanel;
    public Transform spawnPoint;
    public int numberOfPlayers;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void CreatePlayerUi()
    {
        for (int i = 0; i < numberOfPlayers; i++)
            Instantiate(playerPanel, spawnPoint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
