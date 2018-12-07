using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guarded : MonoBehaviour {

    private GameObject[] guards = new GameObject[2];

	void Start () {
        for (int i = 0; i < guards.Length; i++) {
            guards[i] = transform.GetChild(i).gameObject;
        }
	}
	
	void FixedUpdate () {
        if (guards[0] == null && guards[1] == null) {
            Destroy(gameObject);
        }
	}
}
