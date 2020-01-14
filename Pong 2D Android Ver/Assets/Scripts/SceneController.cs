using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public bool isEscapeToExit;
    GameObject stagePanel, mainButtons, helpPanel, exitConfirmationPanel;
    AudioSource audio;
    public AudioClip hitButtonSound;

    // Start is called before the first frame update
    void Start()
    {
        mainButtons = GameObject.Find("MainButtons");

        stagePanel = GameObject.Find("StagePanel");
        stagePanel.SetActive(false);

        helpPanel = GameObject.Find("HelpPanel");
        helpPanel.SetActive(false);

        exitConfirmationPanel = GameObject.Find("ExitConfirmationPanel");
        exitConfirmationPanel.SetActive(false);

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscapeToExit)
                Application.Quit();

            else
                BackToMenu();
        }
    }

    public void StartGame()
    {
        audio.PlayOneShot(hitButtonSound);
        mainButtons.SetActive(false);
        stagePanel.SetActive(true);
    }

    public void ExitGame()
    {
        audio.PlayOneShot(hitButtonSound);
        exitConfirmationPanel.SetActive(true);
    }

    public void ExitConfirmation(Button button){
        if(button.name.Equals("YesBtn"))
            Application.Quit();

        if(button.name.Equals( "NoBtn"))
            exitConfirmationPanel.SetActive(false);
    }

    public void PVPBtnClicked()
    {
        audio.PlayOneShot(hitButtonSound);
        PlayerPrefs.SetString("player1Name", "Player1");
        PlayerPrefs.SetString("player2Name", "Player2");
        PlayerPrefs.SetString("isPVP", "true");
        SceneManager.LoadScene("Main");
    }

    public void CPUBtnClicked()
    {
        audio.PlayOneShot(hitButtonSound);
        PlayerPrefs.SetString("player1Name", "You");
        PlayerPrefs.SetString("player2Name", "CPU");
        PlayerPrefs.SetString("isPVP", "false");
        SceneManager.LoadScene("Main");
    }

    public void BackToMenu()
    {
       SceneManager.LoadScene("Menu");
    }

    public void Help()
    {
        audio.PlayOneShot(hitButtonSound);
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}
