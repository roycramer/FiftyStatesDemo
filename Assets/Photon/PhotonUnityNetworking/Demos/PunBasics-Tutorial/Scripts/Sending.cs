using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sending : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    private InputField tf;
    public Text adviceText;
    public GameObject[] hiddenObjects;


    void Start()
    {
        tf = this.GetComponent<InputField>();
        bool display = (PhotonNetwork.NickName.ToLower() != "research");

        if (display)
            this.gameObject.SetActive(false);
        for (int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CommandSetAdvice(string input)
    {
        photonView.RPC("RPCSetAdvice", RpcTarget.All, input);
    }

    [PunRPC]
    public void RPCSetAdvice(string input)
    {
        for (int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].SetActive(true);
        }
        //box.text = input;
        adviceText.text = input;
        Debug.Log("RPC Set Advice.");
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
