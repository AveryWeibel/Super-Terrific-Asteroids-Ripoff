using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerBehavior : MonoBehaviour {

    public GameObject enemy;
    public float enemySpawnrate = 5;
    public GameObject[] weakPointObjs;
    public static int totalWeakPoints;

    private GameObject thisTrail;

    public ParticleSystem explosion;

    void Start () {
        InvokeRepeating("PingTrail", 4, 4);
        thisTrail = transform.GetChild(1).gameObject;
        if (GameManager.player != null) {
            thisTrail.transform.position = GameManager.player.transform.position;
        }
        totalWeakPoints = weakPointObjs.Length;
        InvokeRepeating("SpawnEnemy", enemySpawnrate, enemySpawnrate);
	}

    private void FixedUpdate()
    {
        thisTrail.transform.position = Vector3.MoveTowards(thisTrail.transform.position, transform.position, .5f);

        if (transform.GetChild(0).childCount  == 0) {
            Instantiate(explosion, transform.position, transform.rotation, null);
            GameManager.UpdateEntityCount("Spawner", -1);            
            Destroy(this.gameObject);
        }
    }

    private void PingTrail() {
        if (GameManager.player != null) {
            thisTrail.transform.position = GameManager.player.transform.position;
        }
    }

    private void SpawnEnemy() {
        if (GameManager.enemiesInScene <= 15) {
            Transform enemyParent = GameManager.levels[GameManager.curLevel -1].transform;


            Instantiate(enemy, transform.position, transform.rotation, enemyParent);
            GameManager.UpdateEntityCount("Enemy", 1);
        }
    } //SpawnEnemy end
}
