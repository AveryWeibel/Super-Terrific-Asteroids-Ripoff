using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapeShuttleBehavior : MonoBehaviour {

    public TMP_Text prompt;
    public static bool playerInRange;

    private void OnTriggerStay(Collider other)
    {
        playerInRange = true;
        if (other.tag == "Player")
        {
            if (GameManager.completedLevels[GameManager.curLevel - 1] == true)
            {
                prompt.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {



                    GameManager.LevelComplete();


                    GameManager.player.GetComponent<PlayerConteroller>().curFr = PlayerConteroller.fireRate.low;
                    GameManager.player.GetComponent<PlayerConteroller>().powerupFlags[0] = false;

                    for (int i = 0; i < GameManager.player.GetComponent<PlayerConteroller>().powerupGeo.Length; i++) {
                        GameManager.player.GetComponent<PlayerConteroller>().powerupGeo[i].SetActive(false);
                    }

                    GameManager.onShuttle = true;
                    GameManager.markers.SetActive(true); //turn on markers before turning off level to prevent error
                    GameManager.levels[GameManager.curLevel - 1].SetActive(false);
                    other.transform.GetChild(1).gameObject.SetActive(true);
                    
                    gameObject.SetActive(false);
                } //Player presses space end

            }//Level is complete end 

        }//Other tag is player end

    }//OnTriggerEnter end

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            prompt.gameObject.SetActive(false);
        }
        playerInRange = false;
    }
}
