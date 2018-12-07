using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameObject player;
    public GameObject playerPrefab;

    public static GameObject markers;

    public static int enemiesInScene = 0;
    public static int spawnersInScene = 0;

    public static int curLevel = 1;

    public static int currentPrideColor = 0;

    public static bool[] completedLevels = new bool[7];

    public static bool onShuttle;

    public bool firstLoad = true;

    public static GameObject[] levels = new GameObject[7];
    public GameObject[] levelPrefabs = new GameObject[levels.Length];

    public static GameObject GameOverScreenObj;
    public static GameObject LevelCompleteScreenObj;
    public static GameObject ShipSelectScreenObj;
    public static GameObject PauseMenuObj;

    public enum shipType { scout, fighter, tank };
    public static shipType chosenType;

    private void Awake()
    {

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1)) { //Makes sure we are in the main scene before proceeding

            markers = GameObject.Find("Markers"); //Gets the markers gameObject, because it is active at launch, turn it off
            if (markers != null && markers.activeSelf)
            {
                markers.SetActive(false);
            }

            // Gets the player object. Must be last child of this object.
            //player = transform.GetChild(transform.childCount - 1).gameObject;
            //player.transform.parent = null;

            //Adding Levels to the levels[] array
            for (int i = 0; i < transform.childCount; i++) {
                levels[i] = transform.GetChild(i).gameObject;
            }

            //Gets and disables Canvas text objects
            GameOverScreenObj = GameObject.Find("GameOverScreen");
            LevelCompleteScreenObj = GameObject.Find("LevelCompleteScreen");
            ShipSelectScreenObj = GameObject.Find("ShipSelectScreen");
            PauseMenuObj = GameObject.Find("PauseMenuScreen");
            GameOverScreenObj.SetActive(false);
            LevelCompleteScreenObj.SetActive(false);
            ShipSelectScreenObj.SetActive(false);
            PauseMenuObj.SetActive(false);
        }

        //Handles ship select
        selectShip();
    }

    //Handles game ending
    public static void GameOver () {
        GameOverScreenObj.SetActive(true); //Enables death menu
        player.SetActive(false); //Disables player to prevent control
    }

    //Handles finishing a level
    public static void LevelComplete()
    {
        completedLevels[curLevel - 1] = true;
        levels[curLevel - 1].GetComponent<LevelBehavior>().complete = true;
    }

    //Resets game at start of current level
    public void resetLevel() {

        AudioPlayer.menuClick.Play();

        completedLevels[curLevel - 1] = false;

        Destroy(levels[curLevel - 1]);    

            markers.transform.GetChild(curLevel - 1).GetComponent<markerBehavior>().myLevel =
            levels[curLevel - 1] =
            Instantiate(levelPrefabs[curLevel - 1], levelPrefabs[curLevel - 1].transform.position, levelPrefabs[curLevel - 1].transform.rotation, transform);

        markers.transform.GetChild(curLevel - 1).GetComponent<markerBehavior>().myLaunchpad = levels[curLevel - 1].GetComponent<LevelBehavior>().launchpad;

        GameObject oldPlayer = player;


        selectShip();
        //player = Instantiate(playerPrefab,
           // levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.position + new Vector3(0, 0, 5),
            //levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.rotation, null);

        Destroy(oldPlayer);

        GameOverScreenObj.SetActive(false);

        if (LevelCompleteScreenObj.activeSelf) {
            LevelCompleteScreenObj.SetActive(false);
        }
        
        //SceneManager.LoadScene(0);
    }

    //Takes in a value(-1,0,1) and increments entitiy counts by that amount
    public static void UpdateEntityCount(string type, int increment) {
        if (Mathf.Abs(increment) == 1)
        {
            if (type == "Enemy")
            {
                enemiesInScene += increment;
            }

            if (type == "Spawner")
            {
                spawnersInScene += increment;
                if (spawnersInScene <= 0)
                {
                    LevelComplete();
                    if (!LevelCompleteScreenObj.activeSelf)
                    {
                        LevelCompleteScreenObj.SetActive(true); //Toggles level complete screen
                    }
                }
            }
        }
        else {
            if (Mathf.Abs(increment) > 1) {
                Debug.Log("UpdateEntity increment is greater than one");
                Debug.Break();
            }
            else if (increment == 0) {
                Debug.Log("UpdateEntity increment is zero");
                Debug.Break();
            }
            
        }
    }

    public void selectShip(){

        if (player != null) {
            Destroy(player);
        }

        if (ShipSelectScreenObj != null) {
            ShipSelectScreenObj.SetActive(true);
        }

    }

    public void selectScout()
    {
        AudioPlayer.menuClick.Play();

        chosenType = shipType.scout;

        player = Instantiate(playerPrefab,
        levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.position + new Vector3(0, 0, 5),
        levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.rotation, null);
        firstLoad = false;
        ShipSelectScreenObj.SetActive(false);
    }

    public void selectFighter()
    {
        AudioPlayer.menuClick.Play();

        chosenType = shipType.fighter;

        player = Instantiate(playerPrefab,
        levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.position + new Vector3(0, 0, 5),
        levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.rotation, null);
        firstLoad = false;
        ShipSelectScreenObj.SetActive(false);
    }

    public void selectTank()
    {
        AudioPlayer.menuClick.Play();

        chosenType = shipType.fighter;
        //chosenType = shipType.tank;

        player = Instantiate(playerPrefab,
        levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.position + new Vector3(0, 0, 5),
        levels[curLevel - 1].GetComponent<LevelBehavior>().levelOrigin.transform.rotation, null);
        firstLoad = false;
        ShipSelectScreenObj.SetActive(false);
    }

    public void startGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        //StartCoroutine("launchGame");
    }

    public void toMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    /*IEnumerator launchGame() {

        AudioPlayer.menuClick.Play();

        yield return new WaitForSeconds(0.163f);

        //if (!AudioPlayer.menuClick.isPlaying)
        //{
            SceneManager.LoadScene(0);
        Debug.Log("launchGame Running");
       // }

    }*/

    public void exitGame() {
        AudioPlayer.menuClick.Play();
        Application.Quit();
    }
}
