using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour {

    Vector3 offset1 = new Vector3(1, 0, 0);
    Vector3 offset2 = new Vector3(1, 0, 1);

    public GameObject stage2Obj;
    public GameObject stage3Obj;

    public ParticleSystem rockBurst;

    public bool hasDrop = false;

    bool hit = false;

    private GameObject hLight;

    public GameObject[] drops;

    public enum Stage { one, two, three};
    public Stage curStage;

    void Start () {
        if (curStage == Stage.two || curStage == Stage.three) {
            GetComponent<Rigidbody>().AddForce(Random.Range(-150f, 250f), 0, Random.Range(-150f, 250f));
            if (curStage == Stage.three && Random.Range(0,5) == 2) {
                hasDrop = true;
                hLight = transform.GetChild(1).gameObject;
                hLight.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {

        Mathf.Clamp(GetComponent<Rigidbody>().velocity.x, -5, 5);
        Mathf.Clamp(GetComponent<Rigidbody>().velocity.y, -5, 5);
        /*
        if (GetComponent<Rigidbody>().velocity.magnitude > 5) {
            GetComponent<Rigidbody>().velocity -= new Vector3(.5f, .5f, .5f);

            if (GetComponent<Rigidbody>().velocity.magnitude > 15)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(10, 10, 10);
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet" || other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);

            AudioPlayer.astHit.Play();
            AudioPlayer.lasHit.Play();

            Instantiate(rockBurst, transform.position, transform.rotation);

            if (curStage == Stage.one &! hit)
            {
                hit = true;
                Instantiate(stage2Obj, transform.position, transform.rotation, transform.parent);
                Instantiate(stage2Obj, transform.position + offset1, transform.rotation, transform.parent);
                Destroy(this.gameObject);
            }
            if (curStage == Stage.two &! hit)
            {
                hit = true;
                Instantiate(stage3Obj, transform.position, transform.rotation, transform.parent);
                Instantiate(stage3Obj, transform.position + offset1, transform.rotation, transform.parent);
                Instantiate(stage3Obj, transform.position + offset2, transform.rotation, transform.parent);
                Destroy(this.gameObject);
            }
            if (curStage == Stage.three &! hit)
            {
                hit = true;
                if (hasDrop) {
                    if (Random.Range(0, 8) == 2)
                    {
                        Instantiate(drops[Random.Range(0, drops.Length)], transform.position, transform.rotation, null);
                    }
                    else {                        
                        GameObject thisDrop = Instantiate(drops[Random.Range(0, drops.Length - 1)], transform.position, transform.rotation, null);
                        Debug.Log("Dropped: " + thisDrop.name);
                    }
                    Destroy(this.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
