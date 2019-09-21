using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AuthUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loginPanel;
    public GameObject signupPanel;
    public GameObject loggedinPanel;

    public InputField loginEmail;
    public InputField loginPassword;


    public InputField colegio;
    public InputField grado;
    public InputField nombres;
    public InputField apellidos;
    public InputField username;
    public InputField signupEmail;
    public InputField signupPassword;
    public InputField signupConfirmPassword;

    public Text loggedinUserEmail;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        ShowLoginPanel();
    }

    public void ShowLoginPanel()
    {
        Debug.Log("LOGIIINNNNN");
        try
        {
            loginPanel.SetActive(true);
            ShowPanel(loginPanel);
        }
        catch (Exception e) { Debug.Log(e.Message); }

    }

    public void ShowSignupPanel()
    {
        ShowPanel(signupPanel);

    }

    public void ShowLoggedinPanel()
    {
        ShowPanel(loggedinPanel);

    }

    public void ShowPanel(GameObject panel)
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        loggedinPanel.SetActive(false);

        panel.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
