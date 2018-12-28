using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehavior : MonoBehaviour {

    private GameObject player;
    public float followPlayerSpeed;
    public float mouseScrollSpeed;

    private void Start()
    {
        player = GameManager.player;
    }

    void FixedUpdate () {
        if (player == null)
        {
            player = GameManager.player;
        }

        if (player != null && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1)) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), followPlayerSpeed);
        }
    }
}
