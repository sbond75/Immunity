﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlayer = null;
    public GameObject CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
        set
        {
            currentPlayer = value;
            currentPlayer.GetComponent<Agent>().Velocity = Vector2.zero;
        }
    }
    public GameObject Spawner;
    public bool StartedGame = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}