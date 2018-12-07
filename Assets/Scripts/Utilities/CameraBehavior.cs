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

        //if (GameManager.onShuttle)
        //{
        /*if (Input.GetAxis("Mouse ScrollWheel") > 0 && Vector3.Distance(player.transform.position, transform.position) > 30)
        {
            transform.Translate(0, 0, mouseScrollSpeed);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && Vector3.Distance(player.transform.position, transform.position) < 100) {
            transform.Translate(0, 0, -mouseScrollSpeed);
        }*/
        //}
        if (player != null && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0)) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), followPlayerSpeed);
        }
    }
}
