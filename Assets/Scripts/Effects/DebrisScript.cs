using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour {
	
	void Start () {
        transform.rotation = Quaternion.Euler(Random.Range(1, 360), Random.Range(1, 360), Random.Range(1, 360));

        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-16f, 60f) * 6, Random.Range(-50f, -60f) * 3, Random.Range(-18f, 60f) * 6));
        GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10f, 10f) * 6, 0, Random.Range(-10f, 10f) * 6));

        Invoke("DestroyThis", 5);
    }

    void DestroyThis() {
        Destroy(this.gameObject);
    }
}
