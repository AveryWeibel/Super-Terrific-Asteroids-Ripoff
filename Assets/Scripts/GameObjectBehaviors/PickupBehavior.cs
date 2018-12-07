using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour {

    public bool overWorldItem = false;

	void FixedUpdate () {
        if (GameManager.onShuttle && !overWorldItem)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            AudioPlayer.pickup.Play();
        }
    }
}
