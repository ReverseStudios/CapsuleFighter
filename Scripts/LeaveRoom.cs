using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour
{
    public void OnLeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        SceneManager.LoadScene("MainMenu");
    }
}
