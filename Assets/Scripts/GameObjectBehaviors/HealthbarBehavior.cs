using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarBehavior : MonoBehaviour {

    private GameObject player;

	void Start () {
        player = GameManager.player;
	}
	
	void FixedUpdate () {
        if (player == null) {
            player = GameManager.player;
        }
        transform.position = new Vector3(player.transform.position.x -2, 5, player.transform.position.z);
	}
}
