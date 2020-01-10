using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class UINetwork : MonoBehaviour
{
    GameObject infoPanel, typePanel, multiplayerPanel, titleText, mainPanel, backBtn;
    AudioSource audio;
    public AudioClip hitButtonSound;
    Button hostBtn, joinBtn, cancelBtn, backMultiplayerBtn;
    Text infoTxt;
    NetworkManager network;
    int status = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        soundManager = GameObject.Find("SoundManager");

        typePanel = GameObject.Find("TypePanel");
        multiplayerPanel = GameObject.Find("MultiplayerPanel");
        titleText = GameObject.Find("TitleText");
        mainPanel = GameObject.Find("MainPanel");
        backBtn = GameObject.Find("BackBtn");

        infoPanel = GameObject.Find("InfoPanel");
        infoTxt = GameObject.Find("InfoText").GetComponent<Text>();

        hostBtn = GameObject.Find("HostBtn").GetComponent<Button>();
        joinBtn = GameObject.Find("JoinBtn").GetComponent<Button>();
        backMultiplayerBtn = GameObject.Find("BackMultiplayerBtn").GetComponent<Button>();
        cancelBtn = GameObject.Find("CancelBtn").GetComponent<Button>();

        hostBtn.onClick.AddListener(StartHostGame);
        joinBtn.onClick.AddListener(StartJoinGame);
        backMultiplayerBtn.onClick.AddListener(Back);
        cancelBtn.onClick.AddListener(CancelConnection);
        cancelBtn.gameObject.SetActive(false);

        network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        infoTxt.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkClient.active || NetworkServer.active)
        {
            hostBtn.interactable = false;
            joinBtn.interactable = false;

            infoPanel.SetActive(true);
            backMultiplayerBtn.gameObject.SetActive(false);
            cancelBtn.gameObject.SetActive(true);
        }

        else
        {
            hostBtn.interactable = true;
            joinBtn.interactable = true;

            backMultiplayerBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(false);
        }

        if (NetworkServer.connections.Count == 2 && status == 0)
        {
            status = 1;
            StartGame();
        }

        if (ClientScene.ready && !NetworkServer.active && status == 0)
        {
            status = 1;
            StartGame();
        }
    }

    public void StartGame(){
        SceneController.isLocal = false;
        SceneManager.LoadScene ("Main");
    }

    private void StartHostGame()
    {
        if (!NetworkServer.active)
            network.StartHost();

        if (NetworkServer.active)
            infoTxt.text = "Info: Waiting for other player to join the game...";
    }
    private void StartJoinGame()
    {
        if (!NetworkClient.active)
        {
            network.StartClient();
            network.client.RegisterHandler(MsgType.Disconnect, ConnectionError);
        }

        if (NetworkClient.active)
            infoTxt.text = "Info: Connecting...";
    }
    private void CancelConnection()
    {
        if (NetworkServer.active)
            network.StopHost();

        if (NetworkClient.active)
            network.StopClient();

        hostBtn.interactable = true;
        joinBtn.interactable = true;
        infoTxt.text = "Info: Server Address " + network.networkAddress + " with port " + network.networkPort;
    }

    private void ConnectionError(NetworkMessage netMsg)
    {
        network.StopHost ();
        SceneManager.LoadScene ("Menu");
    }

    private void Back(){
        SceneController.currentMenu = 2;
        multiplayerPanel.SetActive(false);
        mainPanel.SetActive(false);
        backBtn.SetActive(true);
        titleText.SetActive(true);
        typePanel.SetActive(true);
    }
}
