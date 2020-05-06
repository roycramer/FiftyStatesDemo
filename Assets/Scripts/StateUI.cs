using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StateUI : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    InputField box;

    public bool correct = false;
    string inputTemp = "";

    void Start()
    {
        box = this.GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAnswer(string input)
    {
        correct = (input.ToLower() == box.gameObject.name);
        box.transform.Find("CheckBoxImage").gameObject.SetActive(correct);
        Debug.Log(input+" "+ box.gameObject.name +"???"+box.text);
        if(input.Length>4 && !correct)
        {
            inputTemp = input;
            Invoke("SummonAssistant", 5);
        }
    }

    public void CommandSetText(string input) //called with OnValueChanged.
    {
        if (box.isFocused) //only send the event if it's from the player that is typing.
            photonView.RPC("RPCSetText", RpcTarget.Others, input);
    }

    [PunRPC]
    public void RPCSetText(string input)
    {
        if (box.isFocused)
            EventSystem.current.SetSelectedGameObject(null, null);
        box.text = input;
    }

    public void SummonAssistant()
    {
        //if (inputTemp.Equals(box.text))
        //{
        //    string dialog = "You wrote " +inputTemp + " for one of the states. I think that state is " + box.gameObject.name+"!";
        //    GameObject.Find("AdviceText").GetComponent<Text>().text = dialog;
        //}
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //throw new System.NotImplementedException();
    }
}
