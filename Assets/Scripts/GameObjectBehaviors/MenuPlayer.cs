using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour {

    public ParticleSystem jets;
    public Transform spawnpoint;
    public int spawnAdjust = 0;

    public GameObject[] shipModels = new GameObject[2];

	void Start () {
        InvokeRepeating("ResetPosition", 8, 8);
	}
	
	void FixedUpdate () {
        jets.Emit(2);

        GetComponent<Rigidbody>().AddTorque(0, Random.Range(50, 100), 0);
        GetComponent<Rigidbody>().AddForce(Random.Range(1, 3), 0, Random.Range(1, 3));
    }

    void ResetPosition() {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = spawnpoint.transform.position;
        spawnpoint.transform.position = new Vector3(transform.position.x + spawnAdjust, transform.position.y, transform.position.z);
        spawnAdjust++;

        if (shipModels[0].activeSelf) {    
            shipModels[0].SetActive(false);
            shipModels[1].SetActive(true);
        }
        else {
            shipModels[0].SetActive(true);
            shipModels[1].SetActive(false);
        }
    }
}
