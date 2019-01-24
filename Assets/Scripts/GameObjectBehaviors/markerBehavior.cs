using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class markerBehavior : MonoBehaviour {

    public GameObject myLevel;
    public GameObject myLaunchpad;
    public TMP_Text myText;
    public TMP_Text myPrompt;
    public int thisLevelNum;
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

    private void OnTriggerEnter(Collider other)
    {
        if ((GameManager.completedLevels[thisLevelNum] || GameManager.completedLevels[thisLevelNum - 1]))
        {
            if (myPrompt.IsActive() == false)
            {
                myPrompt.SetText("Press Space to Enter");
                myPrompt.gameObject.SetActive(true);
            }
        }
        else {
            if (myPrompt.IsActive() == false)
            {
                myPrompt.SetText("Complete Previous Level to Enter");
                myPrompt.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player" && 
        Input.GetKeyDown(KeyCode.Space) && 
        GameManager.onShuttle &&
        (GameManager.completedLevels[thisLevelNum] || GameManager.completedLevels[thisLevelNum - 1])) {
            GameManager.curLevel = myLevel.GetComponent<LevelBehavior>().thisLevel;
            Debug.Log(GameManager.curLevel);
            other.transform.GetChild(1).gameObject.SetActive(false);
            myLevel.SetActive(true);
            rocket.SetActive(true);
            transform.parent.gameObject.SetActive(false);
            GameManager.onShuttle = false;
        } //If player presses space nearby
    }
    private void OnTriggerExit(Collider other)
    {
        if (myPrompt.IsActive() == true)
        {
            myPrompt.gameObject.SetActive(false);
        }
    }
}
