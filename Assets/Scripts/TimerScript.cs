using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    //TMPro.TextMeshProUGUI timerText;
    Text infoText;
    int timer = 0; //length in seconds.
    string statesLeftString = "";
    string playerNames = "";
    public Transform statesList;
    bool foundPlayers = false;

    void Start()
    {
        infoText = this.GetComponent<Text>();
        InvokeRepeating("Increment", 0, 1);
        playerNames = ListPlayerNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Increment()
    {
        //if(!foundPlayers && PhotonNetwork.CountOfRooms>0 && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        //{
        //    foundPlayers = true;
        //    playerNames = ListPlayerNames();
        //}

        //if (foundPlayers)
        //{
            
            //if (timer > 0)
                timer += 1;
            string timeLeftString = (timer / 60).ToString("00") + ":" + (timer % 60).ToString("00");
            Count();
            infoText.text = timeLeftString + "\n" + statesLeftString;
            if (PhotonNetwork.NickName.ToLower().Equals("research"))
            {
                infoText.text += "\n" + playerNames;
            }
        //}
        

        
    }

    void Count()
    {
        int count = 0;
        int total = statesList.childCount;
        for (int i = 0; i < total; i++)
        {
            if (statesList.GetChild(i).GetComponent<StateUI>().correct)
                count++;
        }
        statesLeftString = (count) + "/" + total;
    }

    public string ListPlayerNames()
    {
        string result = "";
        Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;
        for (int i=0; i<players.Length; i++)
        {
            result += players[i]+"\n";
        }
        return result;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
