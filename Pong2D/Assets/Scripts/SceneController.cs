using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    GameObject mainPanel, typePanel, localPanel, multiplayerPanel, titleText, backBtn, helpPanel, exitConfirmationPanel;
    public static int currentMenu = 0;
    AudioSource audio;
    public AudioClip hitButtonSound;
    public static bool isLocal = false;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel = GameObject.Find("MainPanel");
        typePanel = GameObject.Find("TypePanel");
        localPanel = GameObject.Find("LocalPanel");
        multiplayerPanel = GameObject.Find("MultiplayerPanel");
        titleText = GameObject.Find("TitleText");
        backBtn = GameObject.Find("BackBtn");
        helpPanel = GameObject.Find("HelpPanel");
        exitConfirmationPanel = GameObject.Find("ExitConfirmationPanel");

        init();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void init(){
        typePanel.SetActive(false);
        localPanel.SetActive(false);
        multiplayerPanel.SetActive(false);
        backBtn.SetActive(false);
        helpPanel.SetActive(false);
        exitConfirmationPanel.SetActive(false);
    }

    public void ClickButtonAudio(){
        audio.PlayOneShot(hitButtonSound);
    }

    public void MainPanel(Button button){
        ClickButtonAudio();

        if(button.name.Equals("StartBtn"))
        {
            currentMenu = 1;
            typePanel.SetActive(true);
            backBtn.SetActive(true);
            mainPanel.SetActive(false);
        }

        if(button.name.Equals("ExitBtn"))
            exitConfirmationPanel.SetActive(true);
    }

    public void SelectGameType(Button button){
        ClickButtonAudio();
        typePanel.SetActive(false);
        titleText.SetActive(false);

        if (button.name.Equals("LocalBtn"))
        {
            currentMenu = 2;
            localPanel.SetActive(true);
            isLocal = true;
        }

        if(button.name.Equals("MultiplayerBtn"))
        {
            backBtn.SetActive(false);
            multiplayerPanel.SetActive(true);
            isLocal = false;
        }

    }

    public void SelectLocal(Button button){
        ClickButtonAudio();

        if(button.name.Equals("PvpBtn"))
        {
            PlayerPrefs.SetString("player1Name", "Player1");
            PlayerPrefs.SetString("player2Name", "Player2");
            PlayerPrefs.SetString("isPVP", "true");
        }

        if(button.name.Equals("CpuBtn"))
        {
            PlayerPrefs.SetString("player1Name", "You");
            PlayerPrefs.SetString("player2Name", "CPU");
            PlayerPrefs.SetString("isPVP", "false");
        }

        SceneManager.LoadScene("Main");
    }

    public void BackBtnClicked(){
        if(currentMenu == 1){
            mainPanel.SetActive(true);
            typePanel.SetActive(false);
            backBtn.SetActive(false);
            currentMenu = 0;
        }

        if(currentMenu == 2){
            localPanel.SetActive(false);
            titleText.SetActive(true);
            typePanel.SetActive(true);
            currentMenu = 1;
        }
    }

    public void Help()
    {
        ClickButtonAudio();
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        ClickButtonAudio();
        helpPanel.SetActive(false);
    }

    public void ExitConfirmation(Button button){
        if (button.name.Equals("YesBtn"))
            Application.Quit();

        else if(button.name.Equals("NoBtn"))
            exitConfirmationPanel.SetActive(false);
    }
}
