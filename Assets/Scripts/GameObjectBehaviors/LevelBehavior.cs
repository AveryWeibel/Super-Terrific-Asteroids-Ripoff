using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehavior : MonoBehaviour {

    public int thisLevel;
    public bool complete;
    public int initSpawners;
    public int initEnemies;

    public GameObject levelOrigin;
    public GameObject launchpad;

    private void OnEnable()
    {
        UpdateGameManager();
    }

    private void UpdateGameManager() {
        if (!complete) {
            GameManager.enemiesInScene = initEnemies;
            GameManager.spawnersInScene = initSpawners;
        }
    }

}
