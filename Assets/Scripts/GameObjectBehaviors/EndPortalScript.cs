using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndPortalScript : MonoBehaviour {

    public TMP_Text myText;
    public TMP_Text myPrompt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (myPrompt.IsActive() == false)
            {
                myPrompt.SetText("Press Space to Enter");
                myPrompt.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //If player presses space nearby
        if (other.tag == "Player" &&
        Input.GetKeyDown(KeyCode.Space) &&
        GameManager.onShuttle &&
        GameManager.allLevelsComplete)
        {
            Debug.Log("You Win!");
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (myPrompt.IsActive() == true)
        {
            myPrompt.gameObject.SetActive(false);
        }
    }
}

