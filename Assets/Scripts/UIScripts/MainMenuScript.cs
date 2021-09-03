using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script attached to main menu empty parent object, handling all button clicks
public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private SpawnManagerAsteroids spawnManagerAsteroids;

    [SerializeField]
    private SpawnManagerPlayer spawnManagerPlayer;

    [SerializeField]
    private SpawnManagerUFO spawnManagerUFO;

    [SerializeField]
    private PlayerMainScript playerMainScript;

    //group for enabling and disabling whole main menu
    [SerializeField]
    private GameObject mainMenuGroup;

    //reference to continueButton required to enable button after starting a game
    [SerializeField]
    private GameObject continueButton;

    private bool gameInProgress = false;


    private void Start()
    {
        Time.timeScale = 0;
    }


    private void Update()
    {
        //do nothing if game has not started yet
        if (!gameInProgress) 
            return;

        //toggle main menu On and Off on Esc key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenuGroup.activeSelf)
            {
                Time.timeScale = 1;
                mainMenuGroup.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                mainMenuGroup.SetActive(true);
            }
        }
    }


    public void ContinueButtonPressed()
    {
        Time.timeScale = 1;
        mainMenuGroup.SetActive(false);
    }


    public void NewGameButtonPressed()
    {
        Time.timeScale = 1;
        continueButton.SetActive(true);
        mainMenuGroup.SetActive(false);

        ScoreCounter.instance.ResetScore();
        spawnManagerPlayer.StartNewGame();
        spawnManagerAsteroids.StartNewGame();
        spawnManagerUFO.StartNewGame();

        gameInProgress = true;
    }

    //Just passing control type to player object
    //Done to channel all main menu actions through this main script
    public void ChangeControlType(ControlsBaseClass.ControlType controlType)
    {
        playerMainScript.ChangeControls(controlType);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
