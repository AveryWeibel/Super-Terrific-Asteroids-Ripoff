using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class markerBehavior : MonoBehaviour {

    public GameObject myLevel;
    public GameObject myLaunchpad;
    public TMP_Text myText;
    private GameObject rocket;

    private void Start()
    {
        if (myLevel != null) {
            rocket = myLaunchpad.transform.GetChild(0).gameObject;
        }
    }

    private void OnEnable()
    {
        /*if (GameManager.levels[myLevel.GetComponent<LevelBehavior>().thisLevel - 1] != null && 
            GameManager.completedLevels[myLevel.GetComponent<LevelBehavior>().thisLevel] == true) {
            myLevel.GetComponent<LevelBehavior>().complete = true;
        }*/
        if (myLevel != null && myLevel.GetComponent<LevelBehavior>().complete) {
            myText.GetComponent<TMP_Text>().color = Color.green;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.Space) && GameManager.onShuttle) {
            GameManager.curLevel = myLevel.GetComponent<LevelBehavior>().thisLevel;
            Debug.Log(GameManager.curLevel);
            other.transform.GetChild(1).gameObject.SetActive(false);
            myLevel.SetActive(true);
            rocket.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            GameManager.onShuttle = false;
        } //If player presses space nearby
    }
}
