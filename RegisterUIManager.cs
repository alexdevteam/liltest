using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUIManager : MonoBehaviour {
    public InputField user;
    public InputField pass;

    public void OnClick()
    {
        Client.instance.CreateAccount(user.text, pass.text);
    }
}
