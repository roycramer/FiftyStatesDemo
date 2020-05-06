using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestControl : MonoBehaviourPun, IPunObservable
{

    public Vector3 pos = new Vector3(0, 0, 0);
    public Text txt;
    public string sendingText = "";

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    
        if (stream.IsWriting)
        {
            stream.SendNext(this.pos);
            stream.SendNext(this.sendingText);
        }
        else
        {
            this.pos = (Vector3)stream.ReceiveNext();
            this.sendingText = (string)stream.ReceiveNext();
        }

        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

    }

    public void MarkText(string _sendingText) //triggered when someone presses enter on the typing box.
    {
        sendingText = _sendingText;
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    public void MoveBoth(Vector3 input)
    {
        photonView.RPC("MoveBoth2", RpcTarget.All, input);
    }

    [PunRPC]
    public void MoveBoth2(Vector3 input)
    {
        Vector3 temp = transform.position;
        temp += input;
        transform.position = temp;
    }

}
