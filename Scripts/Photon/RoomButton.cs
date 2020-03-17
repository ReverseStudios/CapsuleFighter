using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class RoomButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sizeText;
    public bool startTimer = false;

    public string roomName;
    public int roomSize;

    public void SetRoom()
    {
        nameText.text = roomName;
        sizeText.text = roomSize.ToString();
    }

    public void JoinRoomOnClick()
    {
        startTimer = true;
        PhotonNetwork.JoinRoom(roomName);
    }
}
