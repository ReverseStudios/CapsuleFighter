using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CustomLobby : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static CustomLobby lobby;
    public string roomName;
    public int roomSize;
    public GameObject roomListings;
    public Transform roomPanel;
    public TextMeshProUGUI statusText;
    

    private void Awake()
    {
        lobby = this;
    }
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        statusText.text = "Status : Connecting to Server...";
    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "Status : Connected";
        Debug.Log("Player connected to master.");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RemoveRoomList();
        foreach (RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    void RemoveRoomList()
    {
        while(roomPanel.childCount != 0)
        {
            Destroy(roomPanel.GetChild(0));
        }
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempList = Instantiate(roomListings, roomPanel);
            RoomButton tempButton = tempList.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }
    

public void CreateRoom()
    {
        Debug.Log("Creating a room.");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Room Name already Exists");
    }

   public void OnRoomNameChanged(string name)
    {
        roomName = name;
    }

    public void OnRoomSizeChanged(string size)
    {
        roomSize = int.Parse(size);
    }

    public void JoinLobby()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
