using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class GamePlayNetworkManager : MonoBehaviourPunCallbacks
{
    public void BackToMenu()
    {
        StartCoroutine(BackToMenuByCR());
    }
    IEnumerator BackToMenuByCR()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToLobby()
    {
        StartCoroutine(BackToLobbyByCR());
    }

    IEnumerator BackToLobbyByCR()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom || PhotonNetwork.IsConnectedAndReady == false)
        {
            yield return null;
        }

        SceneManager.LoadScene("Lobby");
    }

    public void Replay()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var scene = SceneManager.GetActiveScene();
            PhotonNetwork.LoadLevel(scene.name);
        }
    }

    public void Quit()
    {
        StartCoroutine(QuitByCR());
    }
    IEnumerator QuitByCR()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }

        Application.Quit();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            BackToLobby();
        }
    }
}
