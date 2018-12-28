using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallbehavior : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("PlayerBullet") || other.tag == ("EnemyBullet"))
        {
                
                //Destroy(other.gameObject);
        }
    }

}
