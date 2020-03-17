using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner instance;
    public GameObject playerPrefab = null;
    //public int defaultColorIndex = 0;
    public Vector3[] spawnPoints;
    //public Color[] playerColors;
    //public IList colorsUsed;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Spawn();
    }

    
    public void Spawn()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Debug.Log(randomIndex);
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[randomIndex], Quaternion.identity);
        return;
    }
}
