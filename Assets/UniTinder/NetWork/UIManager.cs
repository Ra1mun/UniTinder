using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class UIManager : MonoBehaviour
{

    public static UIManager Instance {  get; private set; }

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject startMenu2;
    public InputField usernameField;
    public InputField messageField;

    private void Awake()
    {
        messageField.interactable = false;
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        startMenu2.SetActive(true);
        messageField.interactable = true;
        Client.Instance.ConnectToServer();
        
    }

    public void SendMessageToServer()
    {
        ClientSend.SendMessageToServer();
    }

}
