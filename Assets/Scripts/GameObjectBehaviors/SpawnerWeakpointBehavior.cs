using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWeakpointBehavior : MonoBehaviour {

    public ParticleSystem explosion;
    public GameObject parentSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("PlayerBullet"))
        {
            AudioPlayer.enmHit.Play();
            //AudioPlayer.lasHit.Play();

            Instantiate(explosion, transform.position, transform.rotation, null);
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
