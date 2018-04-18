using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParams : MonoBehaviour {
    int NumberOfPlayers;

    public static GameParams Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetNumberOfPlayers(int val)
    {
        NumberOfPlayers = val;
    }
    public int GetNumberOfPlayers()
    {
        return NumberOfPlayers;
    }
}
