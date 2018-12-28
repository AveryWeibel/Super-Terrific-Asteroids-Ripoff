using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerConteroller : MonoBehaviour {

    public float moveSpeed = 16f;
    public float dodgeSpeed = 0.5f;
    public float cooldownLength = 2;
    public float powerupWearoffLength = 30;
    public float brakeIntensity = .5f;
    public float maxSpeed = 10f;
    public float accelerationSpeed = 5f;
    public float accelerationCap = 20f;
    public float shootDelay = .2f;

    public TMP_Text firerateText;
    public TMP_Text boostText;
    public TMP_Text prideText;

    //public bool brakingOn = true;
    public bool invulnerable;

    public ParticleSystem jets;
    public ParticleSystem boostJets;
    public ParticleSystem playerHit;
    public ParticleSystem shieldHit;
    public ParticleSystem tankChargeSys;

    public GameObject bullet;
    public GameObject shotgunBullet;
    public GameObject railgunBullet;
    public GameObject prideBullet;
    public GameObject shield;
    public GameObject shotgunOrigin;
    public GameObject[] powerupGeo = new GameObject[3];
    public GameObject[] shipModels = new GameObject[3];
    private GameObject[] healthbar = new GameObject[4];
    public GameObject healthbarObj;
    private GameObject curChargeParticle;
    public GameObject chargeObj;

    public static float cooldownTime = 0;
    public static float powerupWearoffTime = 0;
    public static float shootTime = 0;
    public static int health;
    public static int maxHealth;

    public static bool dodging;

    public enum fireRate { low, med, high, pride };
    public fireRate curFr;

    public GameManager.shipType curType;

    //0: boost
    public bool[] powerupFlags = new bool[5];

    private float boostMod = 8;
    private float lowFireMod = 1.3f;
    private float highFireMod = .8f;

    private float scoutSpeedMod = 1.75f;
    private float tankSpeedMod = .4f;


    private float scoutFireMod = 2f;
    private float tankFireMod = 8.3f;

    private float tankChargeTime = 0;
    private bool tankCharged;
    private bool tankCharging;

    public Light playerLight;

    public Quaternion curHeading;

    private void Start()
    {
        healthbarObj = transform.GetChild(0).gameObject;

        curType = GameManager.chosenType;

        if (curType == GameManager.shipType.fighter) {
            health = maxHealth = 3;
        }
        else if (curType == GameManager.shipType.scout)
        {
            health = maxHealth = 2;
        }
        if (curType == GameManager.shipType.tank)
        {
            health = maxHealth = 4;
        }

        for (int i = healthbar.Length - 1; i >= 0; i--) {
            if (i < health)
            {
                healthbar[i] = transform.GetChild(0).GetChild(i).gameObject;
            }
            else {
                transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
            }
        }

        healthbarObj.transform.parent = null;

        health = maxHealth;


        if (curType == GameManager.shipType.fighter)
        {
            shipModels[0].SetActive(true);
        }
        else if (curType == GameManager.shipType.scout) {
            shipModels[1].SetActive(true);
        }
        else if (curType == GameManager.shipType.tank)
        {
            shipModels[2].SetActive(true);
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0f)
            {
                GameManager.PauseMenuObj.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                GameManager.PauseMenuObj.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= shootTime && !GameManager.onShuttle)
        {


            if (curFr == fireRate.low)
            {
                if (curType == GameManager.shipType.fighter)
                {
                    AudioPlayer.lasShot.time = 0.105f;
                    AudioPlayer.lasShot.Play();
                    shootTime = Time.time + shootDelay * lowFireMod;
                    Instantiate(bullet, transform.position, transform.rotation, null);
                }
                else if (curType == GameManager.shipType.scout)
                {
                    AudioPlayer.lasShot.time = 0.105f;
                    AudioPlayer.lasShot.Play();
                    shootTime = Time.time + shootDelay * lowFireMod * scoutFireMod;

                    for (int i = 0; i < 6; i++)
                    {
                        Instantiate
                            (shotgunBullet, //Object
                            shotgunOrigin.transform.position + new Vector3(Random.Range(-.2f, .2f), 0, Random.Range(.2f, .2f)), //Position
                            Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Random.Range(-15, 15), transform.rotation.eulerAngles.z)), //Rotation
                            null); //Parent
                    }
                }

                else if (curType == GameManager.shipType.tank)
                {
                    if (!tankCharging) {
                        tankChargeTime = Time.time + shootDelay * lowFireMod * tankFireMod;
                        tankChargeSys.Play();
                    }
                    tankCharging = true;

                    if (Time.time >= tankChargeTime && !tankCharged) {
                        Debug.Log("Tank Charged");
                        tankChargeSys.Stop();
                        tankChargeSys.Clear();
                        chargeObj.SetActive(true);
                        tankCharged = true;
                    }
                }
            }
            else if (curFr == fireRate.med)
            {
                if (curType == GameManager.shipType.fighter) {
                    AudioPlayer.lasShot.time = 0.105f;
                    AudioPlayer.lasShot.Play();
                    shootTime = Time.time + shootDelay;
                    Instantiate(bullet, transform.position, transform.rotation, null);
                }
                else if (curType == GameManager.shipType.scout)
                {
                    AudioPlayer.lasShot.time = 0.105f;
                    AudioPlayer.lasShot.Play();
                    shootTime = Time.time + shootDelay * scoutFireMod;
                    for (int i = 0; i < 6; i++)
                    {
                        Instantiate
                            (shotgunBullet, //Object
                            shotgunOrigin.transform.position + new Vector3(Random.Range(-.2f, .2f), 0, Random.Range(.2f, .2f)), //Position
                            Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Random.Range(-15, 15), transform.rotation.eulerAngles.z)), //Rotation
                            null); //Parent
                    }
                }
                else if (curType == GameManager.shipType.tank)
                {
                    if (!tankCharging)
                    {
                        tankChargeTime = Time.time + shootDelay * tankFireMod;
                        tankChargeSys.Play();
                    }
                    tankCharging = true;

                    if (Time.time >= tankChargeTime && !tankCharged)
                    {
                        Debug.Log("Tank Charged");
                        tankChargeSys.Stop();
                        tankChargeSys.Clear();
                        chargeObj.SetActive(true);
                        tankCharged = true;
                    }
                }

            }
            else if (curFr == fireRate.high)
            {
                if (curType == GameManager.shipType.fighter) {
                    AudioPlayer.lasShot.time = 0.105f;
                    AudioPlayer.lasShot.Play();
                    shootTime = Time.time + shootDelay;

                    Instantiate(bullet, powerupGeo[2].transform.GetChild(0).transform.position, transform.rotation, null);

                    Instantiate(bullet, powerupGeo[2].transform.GetChild(1).transform.position, transform.rotation, null);
                }
                else if (curType == GameManager.shipType.scout)
                {
                    AudioPlayer.lasShot.time = 0.105f;
                    AudioPlayer.lasShot.Play();
                    shootTime = Time.time + shootDelay * highFireMod * scoutFireMod;
                    for (int i = 0; i < 6; i++)
                    {
                        Instantiate
                            (shotgunBullet, //Object
                            shotgunOrigin.transform.position + new Vector3(Random.Range(-.2f, .2f), 0, Random.Range(.2f, .2f)), //Position
                            Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + Random.Range(-15, 15), transform.rotation.eulerAngles.z)), //Rotation
                            null); //Parent
                    }
                }
                else if (curType == GameManager.shipType.tank)
                {
                    if (!tankCharging)
                    {
                        tankChargeTime = Time.time + shootDelay * highFireMod * tankFireMod;
                        tankChargeSys.Play();
                    }
                    tankCharging = true;

                    if (Time.time >= tankChargeTime && !tankCharged)
                    {
                        Debug.Log("Tank Charged");
                        tankChargeSys.Stop();
                        tankChargeSys.Clear();
                        chargeObj.SetActive(true);
                        tankCharged = true;
                    }
                }
            }
            else if (curFr == fireRate.pride) {
                AudioPlayer.lasShot.time = 0.105f;
                AudioPlayer.lasShot.Play();
                shootTime = Time.time + shootDelay * highFireMod;

                Instantiate(prideBullet, transform.position, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 10, transform.rotation.eulerAngles.z)), null);
                Instantiate(prideBullet, transform.position, transform.rotation, null);
                Instantiate(prideBullet, transform.position, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 10, transform.rotation.eulerAngles.z)), null);
            }

        }//Getkey mouse 0 end

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (tankCharged) {
                Instantiate(railgunBullet, transform.position, transform.rotation, null);
            }

            chargeObj.SetActive(false);
            tankCharged = false;
            tankCharging = false;
            tankChargeSys.Stop();
            tankChargeSys.Clear();
        }

    } //Update end

    void FixedUpdate()
    {

        float thrustForce = 0;

        if (transform.position.y < -.01) {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        //if (Time.time >= powerupWearoffTime) {
        //cancelPowerup();
        //}

        if (!dodging)
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                AudioPlayer.thrusters.Play();
            }
            else if (Input.GetKeyUp(KeyCode.W)) {
                AudioPlayer.thrusters.Stop();
            }

            if (Input.GetKey(KeyCode.W))
            {           

                if (GetComponent<Rigidbody>().velocity.magnitude < accelerationCap)
                {
                    if (powerupFlags[0])
                    {
                        thrustForce = accelerationSpeed * boostMod;
                    }
                    else
                    {
                        thrustForce = accelerationSpeed;
                    }
                }
                else
                {
                    if (powerupFlags[0])
                    {
                        thrustForce = boostMod;
                    }
                    else
                    {
                        thrustForce = 1f;
                    }
                }

                if (curType == GameManager.shipType.scout)
                {
                    thrustForce *= scoutSpeedMod;
                }
                else if (curType == GameManager.shipType.tank)
                {
                    thrustForce *= tankSpeedMod;
                }
            }

            //if (Input.GetKeyDown(KeyCode.Space) && GameManager.completedLevels[GameManager.curLevel] &&) {

            //}

            if (thrustForce > 0)
            {
                if (powerupFlags[0])
                {
                    boostJets.Emit(2);
                }
                else
                {
                    jets.Emit(2);
                }
            }



            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit)) {
                //if (mouseHit.collider.tag == ("tpFloor"))
                //{
                // if (Input.GetKeyDown(KeyCode.Mouse1))
                //{ //Handles calling  of dodge command
                //if (Time.time > cooldownTime)
                //{
                //dodge(mouseHit.point);
                //Debug.Log("Dodged");
                //}
                //}
                // }
            }
            if (mouseHit.collider != null) {
                transform.LookAt(new Vector3(mouseHit.point.x, transform.position.y, mouseHit.point.z));
            }

            if (powerupFlags[0]) {
                if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed * boostMod) {
                    GetComponent<Rigidbody>().AddRelativeForce(0, 0, thrustForce);
                }
            }
            else {
                GetComponent<Rigidbody>().AddRelativeForce(0, 0, thrustForce);
            }
        }

        brake(GetComponent<Rigidbody>().velocity);

    } //<<<<<<<<FixedUpdateEnd

    /* private void dodge(Vector3 hitPoint) {
         dodging = true;

         Vector3 dodgeEndLocation = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
         Debug.Log(dodgeEndLocation);

         transform.position = dodgeEndLocation;

         GetComponent<Rigidbody>().velocity = Vector3.zero;

         cooldownTime = Time.time + cooldownLength;

         dodging = false;
     } //Dodge method end*/

    private void brake(Vector3 velocity) {
        GetComponent<Rigidbody>().velocity = Vector3.Lerp(velocity, Vector3.zero, brakeIntensity);
    }//Brake method end

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            AudioPlayer.plyrHit.Play();
            if (!invulnerable)
            {
                Instantiate(playerHit, transform.position, transform.rotation, null);
                GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(1, 0, 1) * 100, other.transform.position);

                if (curFr == fireRate.med || curFr == fireRate.high || curFr == fireRate.pride || powerupFlags[0])
                {
                    cancelPowerup();
                }
                else
                {
                    UpdateHealth(-1);
                }
            }
        }

        if (other.tag == "HealthPickup")
        {
            Destroy(other.gameObject);
            if (health < maxHealth) {
                UpdateHealth(1);
            }
            else if (health == maxHealth)
            {
                UpdateHealth(0);
            }
        }

        if (other.tag == "ShotSpeedUpgrade")
        {
            Destroy(other.gameObject);
            Instantiate(firerateText, transform.position, Quaternion.Euler(90, 0, 0), null);
            powerupWearoffTime = Time.time + powerupWearoffLength;
            if (curFr == fireRate.low) {
                curFr = fireRate.med;
                powerupGeo[1].SetActive(true);
            }
            else if (curFr == fireRate.med) {
                curFr = fireRate.high;
                powerupGeo[1].SetActive(false);
                powerupGeo[2].SetActive(true);
            }
        }

        if (other.tag == "PrideUpgrade")  
        {
            Destroy(other.gameObject);
            Instantiate(prideText, transform.position, Quaternion.Euler(90, 0, 0), null);
            powerupWearoffTime = Time.time + powerupWearoffLength;
            curFr = fireRate.pride;
            powerupGeo[2].SetActive(true);
            powerupGeo[1].SetActive(true);
        }

        if (other.tag == "BoostUpgrade")
        {
            Debug.Log("Picked up boost");
            Destroy(other.gameObject);
            Instantiate(boostText, transform.position, Quaternion.Euler(90, 0, 0), null);
            powerupGeo[0].SetActive(true);
            powerupWearoffTime = Time.time + powerupWearoffLength;
            powerupFlags[0] = true;
        }
    }

    private void cancelPowerup() {
        if (curFr == fireRate.pride)
        {
            curFr = fireRate.low;
            powerupGeo[2].SetActive(false);
            powerupGeo[1].SetActive(false);
        }
        else if (curFr == fireRate.high || curFr == fireRate.med) {
            curFr = fireRate.low;
            powerupGeo[2].SetActive(false);
            powerupGeo[1].SetActive(false);
        }

        if (powerupFlags[0]) {
            powerupFlags[0] = false;
            powerupGeo[0].SetActive(false);
        }
        if (powerupFlags[1])
        {
            powerupFlags[1] = false;
        }
    } //Cancel Powerup end

    private void OnCollisionEnter(Collision collision)
    {
        if (!invulnerable) {
            if (collision.collider.tag == "LaserWall")
            {
                Instantiate(playerHit, transform.position, transform.rotation, null);
                AudioPlayer.plyrHit.Play();
                if (curFr == fireRate.med || curFr == fireRate.high || curFr == fireRate.pride || powerupFlags[0])
                {
                    cancelPowerup();
                }
                else
                {
                    UpdateHealth(-1);
                }
            }
        }
    }

    private void UpdateHealth(int inc) { 

        CancelInvoke("TurnOffHealthbar");
        healthbarObj.SetActive(true);

        if (inc < 0) {
            invulnerable = true;
            health += inc;
            healthbar[health].transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Player health: " + health + "\nTurned off " + health);
        }
        else if (inc > 0) {
            healthbar[health].transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Player health: " + (health - 1) + "\nTurned off " + health);
            health += inc;
        }




        /*if (inc < 0) { 
            invulnerable = true;
            if (health == maxHealth - (maxHealth - 1)) {
                healthbar[health].transform.GetChild(0).gameObject.SetActive(false);
            }
            else {
                healthbar[health - 1].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        else if (inc > 0 && health != maxHealth)
        {
            if (health == maxHealth - 1)
            {
                healthbar[health].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                healthbar[health + 1].transform.GetChild(0).gameObject.SetActive(true);
            }
        } */



        /*for (int i = inc >= 0 ? healthbar.Length - 1 : 0; ; i -= inc)
        {
            if (inc != 0)
            {
                if (healthbar[i].activeInHierarchy && inc < 0)
                {
                    invulnerable = true;
                    healthbar[i].SetActive(false);
                    break;
                }

                if (!healthbar[i].activeInHierarchy && inc > 0)
                {
                    healthbar[i].SetActive(true);
                    break;
                }
            }
            else {
                break;
            }
        }*/

        if (health <= 0)
        {
            Destroy(healthbarObj);
            GameManager.GameOver();
        }

        if (health == maxHealth) {
            Invoke("TurnOffHealthbar", 2f);
        }

        Invoke("invulnOff", 0.3f);
    } //Update health end

    private void invulnOff() {
        invulnerable = false;
    }

    private void TurnOffHealthbar() {
        healthbarObj.SetActive(false);
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
