using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class UILobby : MonoBehaviour
{
    
    
    [SerializeField] InputField joinmatchInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button HostButton;
    public void Host()
    {
        joinmatchInput.interactable = false;
        joinButton.interactable = false;
        HostButton.interactable = false;
        
     
    }

    public void Join()
    {
        joinmatchInput.interactable = false;
        joinButton.interactable = false;
        HostButton.interactable = false;
        
    }


}
