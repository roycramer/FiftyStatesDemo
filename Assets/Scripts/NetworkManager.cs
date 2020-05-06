using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
            gameObject.SetActive(false);
        else
        {
            //set the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!");
        
        PhotonNetwork.JoinLobby();
        //Debug.Log(PhotonNetwork.GetCustomRoomList(TypedLobby.Default, ""));
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinedLobby!");
        CreateRoom("testroom");
        
    }

    public override void OnConnected()
    {
        Debug.Log("Connected!");
        
    }


    public void CreateRoom (string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        // 4
        if (PhotonNetwork.IsMasterClient)
        {
            //buttonLoadArena.SetActive(true);
            //buttonJoinRoom.SetActive(false);
            Debug.Log("You are Lobby Leader");
        }
        else
        {
            Debug.Log("Connected to Lobby");
        }
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

}
