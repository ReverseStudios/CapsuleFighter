using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{

    private PhotonView PV;
    private PlayerSetup playerSetup;
    private PlayerSpawner playerSpawner;
    public Transform rayOrigin;
    public GameObject bulletPrefab;
    public GameObject deathEffect;
    public Vector3 bulletOrigin;
    public float projectileSpeed;
    public LineRenderer laser;
    public Vector3 startPoint, endPoint;
    public float respawnWaitTime;
    public bool isRespawn = false;
    //public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        playerSetup = GetComponent<PlayerSetup>();
        playerSpawner = GameObject.Find("PlayerSpawner").GetComponent<PlayerSpawner>();
        laser.enabled = false;
        isRespawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        bulletOrigin.x = rayOrigin.position.x;
        bulletOrigin.y = rayOrigin.position.y;
        bulletOrigin.z = rayOrigin.position.z;

        if (!PV.IsMine)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            PV.RPC("RPC_Shooting", RpcTarget.All);
        }
        if (Input.GetMouseButtonUp(0))
        {
            PV.RPC("RPC_StopShoot", RpcTarget.All);
        }

        if (isRespawn)
        {
            Debug.Log(respawnWaitTime);
            if(respawnWaitTime <= 0f)
            {
                PV.RPC("RespawnPlayer", RpcTarget.All);
            }
            respawnWaitTime -= Time.deltaTime;
        }
        else
        {
            return;
        }
    }

    [PunRPC]
    void RPC_Shooting()
    {
        //PhotonNetwork.Instantiate(bulletPrefab.name, bulletOrigin, Quaternion.identity);
        
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), out hit, 1000))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.TransformDirection(Vector3.forward), Color.red);
            if (hit.transform.tag == "Player")
            {
                TakeDamage(hit);
            }
        }
        laser.enabled = true;
        laser.SetPosition(0, startPoint);
        laser.SetPosition(1, endPoint);
    }

    [PunRPC]
    void RPC_StopShoot()
    {
        laser.enabled = false;
    }

    [PunRPC]
    void TakeDamage(RaycastHit hit)
    {
        Debug.Log("Player was hit");
        if (hit.transform.gameObject.GetComponent<PlayerSetup>().playerHeath <= 0f) 
        {
            PhotonNetwork.Destroy(hit.transform.gameObject);
            Instantiate(deathEffect, hit.transform.position, hit.transform.rotation);
            isRespawn = true;
            Debug.Log(isRespawn);
        }

        hit.transform.gameObject.GetComponent<PlayerSetup>().playerHeath -= playerSetup.playerDamange;
    }

    [PunRPC]
    void RespawnPlayer()
    {
        if (!PV.IsMine && isRespawn)
        {
            isRespawn = false;
            respawnWaitTime = 5f;
            Debug.Log(isRespawn);
            playerSpawner.Spawn();
            return;
        }
        else
        {
            return;
        }
    }
}
