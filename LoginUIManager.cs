using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour {
    public InputField user;
    public InputField pass;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        Client.instance.Login(user.text, pass.text);
    }
}
