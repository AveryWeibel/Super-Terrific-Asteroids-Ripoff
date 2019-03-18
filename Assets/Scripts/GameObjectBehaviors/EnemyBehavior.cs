using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    private GameObject player;

    public GameObject bullet;
    public GameObject thisLight;
    public GameObject debris;
    public ParticleSystem explosion;

    private Vector3 target;
    public enum Targets { player, other};
    public Targets curTarget;

    public float speed = 1;
    private float firingSpeed = 3.5f;
    private float stoppingDistance = 15f;
    private float firingDistance = 20f;
    private float activeDistance = 60f;

    private float health = 2;

    public bool inStoppingDistance = false;
    public bool inFiringDistance = false;
    public bool inActiveRange = false;

    bool hit = false;

    public bool mover;
    public bool turret;

    void Start() {
        player = GameManager.player;
        if (player != null) {
            target = player.transform.position;
        }
        thisLight = transform.GetChild(0).gameObject;

        if (turret) {
            firingSpeed -= 3;
            health = 1;
        }

        InvokeRepeating("FireShot", firingSpeed, Random.Range(firingSpeed, firingSpeed + 1));
    }

    void FixedUpdate() {
        RaycastHit wallHit;

        player = GameManager.player;
        transform.LookAt(target);

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if (GameManager.onShuttle)
        {
            gameObject.SetActive(false);
        }


        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
            if (Vector3.Distance(target, transform.position) < activeDistance)
            {
                inActiveRange = true;
                if (Vector3.Distance(target, transform.position) < firingDistance)
                {
                    if (curTarget == Targets.player) {
                        inFiringDistance = true;
                    }

                    if (Vector3.Distance(target, transform.position) < stoppingDistance)
                    {
                        inStoppingDistance = true;
                    }
                    else
                    {
                        inStoppingDistance = false;
                    }
                }
                else
                {
                    inFiringDistance = false;
                }
            }
            else {
                inActiveRange = false;
            }

        //Debug.DrawLine(transform.position, player.transform.position, Color.green);
        if (player != null) {
            if (Physics.Linecast(transform.position, player.transform.position, out wallHit) && wallHit.collider.tag == "LaserWall")
            {
                target = wallHit.point;
                curTarget = Targets.other;
            }
            else {
                target = player.transform.position;
                curTarget = Targets.player;
            }
        }

        if (mover) {
            if (!inStoppingDistance && inActiveRange)
            {
                transform.Translate(0, 0, speed);
            }
        }

        /*if (Vector3.Distance(transform.position, player.transform.position) > 150) {
            Destroy(this.gameObject);
            GameManager.UpdateEntityCount("Enemy", -1);
        }*/
    }

    private void FireShot() {
        if (player.gameObject != null && inFiringDistance && this.gameObject.activeSelf && this.gameObject.transform.parent.gameObject.activeSelf &&
            !GameManager.onShuttle && player.activeInHierarchy) {
            Instantiate(bullet, transform.position, transform.rotation, null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("PlayerBullet"))
        {
            if (!hit) {
                hit = true;
                Instantiate(explosion, transform.position, transform.rotation, null);

                //AudioPlayer.lasHit.Play();
                AudioPlayer.enmHit.Play();
                

                GetComponent<Rigidbody>().AddForceAtPosition(Vector3.one * 100, other.transform.position);
                //Destroy(other.gameObject);
                GameManager.UpdateEntityCount("Enemy", -1);
            }
            if (player.GetComponent<PlayerConteroller>().curType == GameManager.shipType.fighter) {
                UpdateHealth(-1);
            }
            else if (player.GetComponent<PlayerConteroller>().curType == GameManager.shipType.scout)
            {
                UpdateHealth(-.5f);
            }
            else if (player.GetComponent<PlayerConteroller>().curType == GameManager.shipType.tank)
            {
                UpdateHealth(-2);
            }
        }
    }

    private void UpdateHealth(float inc)
    {

        health += inc;

        if (health == 0)
        {
            Instantiate(debris, transform.position, transform.rotation, null);
            Destroy(this.gameObject);
        }
        hit = false;
    }
}
