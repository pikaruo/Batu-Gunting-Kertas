using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class ConnectManager : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_Text feedbackText;
    public void ClickConnect()
    {
        feedbackText.text = "";

        if (usernameInput.text.Length < 3)
        {
            feedbackText.color = Color.red;
            feedbackText.text = "Nama Pengguna Minimal 3 Karakter!";
            return;
        }

        // save username
        PhotonNetwork.NickName = usernameInput.text;
        PhotonNetwork.AutomaticallySyncScene = true;

        // connect to server
        PhotonNetwork.ConnectUsingSettings();
        feedbackText.color = Color.green;
        feedbackText.text = "Connecting...";
    }

    // run when connected
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        feedbackText.text = "Connected to Master";
        SceneManager.LoadScene("Lobby");
    }

}
