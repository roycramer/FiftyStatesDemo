using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Responding : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    private InputField tf;
    public Text adviceText;


    void Start()
    {
        tf = this.GetComponent<InputField>();
        bool display = (PhotonNetwork.NickName.ToLower() == "research");

        if (display)
            this.gameObject.SetActive(false); //if you are the researcher, you do not need to see this.
        else
            adviceText.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CommandSetAdvice(string input)
    {
        photonView.RPC("RPCSetAdvice", RpcTarget.All, input);
        tf.text = "";
    }

    [PunRPC]
    public void RPCSetAdvice(string input)
    {
        //box.text = input;
        adviceText.text = input;
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
