using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Countdown : MonoBehaviour
{
    public static Countdown countdown;
    public TextMeshProUGUI timerText;
    public float timeToPlay;
    public float remainingTime;
    bool startTimer = false;
    public PhotonView PV;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        timeToPlay = 300f;
        startTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
            PV.RPC("StartCountdown", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    void StartCountdown()
    {
        if (startTimer)
        {
            timeToPlay -= Time.deltaTime;
            int min = Mathf.FloorToInt(timeToPlay / 60);
            int sec = Mathf.FloorToInt(timeToPlay % 60);
            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        }

        if (timeToPlay <= 0f)
        {
            startTimer = false;
            return;
        }
    }
}
