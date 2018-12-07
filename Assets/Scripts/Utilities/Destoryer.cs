using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryer : MonoBehaviour {

    public float timer = 5f;

	void Start () {
        Invoke("DestroyThis", timer);
	}

    void DestroyThis() {
        Destroy(this.gameObject);
    }
}
