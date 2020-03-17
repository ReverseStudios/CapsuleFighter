using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class PhotonRoomCustom : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    public static PhotonRoomCustom room;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    //PlayerInfo
    Player[] photonPlayers;
    public int playerInRoom;
    public int myNumberInRoom;

    public GameObject lobbyGO;
    public GameObject roomGO;
    public GameObject playerListings;
    public GameObject startButton;
    public Transform playerPanel;
    

    private void Awake()
    {
        if(PhotonRoomCustom.room == null)
        {
            PhotonRoomCustom.room = this;
        }
        else
        {
            if(PhotonRoomCustom.room != this)
            {
                Destroy(PhotonRoomCustom.room.gameObject);
                PhotonRoomCustom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.LoadLevel("Game");
    }
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        ClearPlayerList();
        ListPlayer();
        photonPlayers = PhotonNetwork.PlayerList;
        playerInRoom++;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        playerInRoom--;
        ClearPlayerList();
        ListPlayer(); 
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        lobbyGO.SetActive(false);
        roomGO.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
        ClearPlayerList();
        ListPlayer();

        photonPlayers = PhotonNetwork.PlayerList;
        playerInRoom = photonPlayers.Length;
        
    }

    void ClearPlayerList()
    {
        for(int i = playerPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(playerPanel.GetChild(i).gameObject);
        }
    }

    void ListPlayer()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListings, playerPanel);
                TextMeshProUGUI tempText = tempListing.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                tempText.text = player.NickName;
            }
        }
    }
}
