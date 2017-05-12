using System;
using System.Collections;
using UnityEngine;

public class LoginScript : MonoBehaviour
{
    string loginNick = ""; //this is the field where the player will put the name to login
    string loginPassword = ""; //this is his password
    string createAccNick = "";
    string createAccPassword = "";
    public Vector2 f;
    public Vector2 f1;
    public Vector2 f2;
    public Vector2 f3;
    string formText = ""; //this field is where the messages sent by PHP script will be in

    string LoginURL = "http://lilmafiagamedy3287dy872.890m.com/login.php";
    string CreateAccountURL = "http://lilmafiagamedy3287dy872.890m.com/createaccount.php";
    string hash = "FKqPup.g4j0pF"; //change your secret code, and remember to change into the PHP file too

    Rect textrect = new Rect(10, 150, 500, 500); //just make a GUI object rectangle
    GUIStyle textStyle = new GUIStyle();

    // Flags sent by server:
    /* [ createaccount.php ]
     * nullpass : password is null
     * nulluser : username is null
     * notequalhash : hash is different between client and server
     * nameused : username already used
     * newaccsuccess : account successfully created
     * error: ... : custom error
     */
    /* [ login.php ]
     * nulluser : username is null
     * notequalhash : hash is different between client and server
     * success:[sessionKey] : success login, key given for later use
     * wrongpass : wrong password entered
     * wrongname : entered name of non-existing account.
     */

    private void Awake()
    {
        textStyle.fontSize = 36;
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(494, 321, 0, 0), "Username:",textStyle);
        GUI.Label(new Rect(495, 401, 0, 0), "Password:",textStyle);

        loginNick = GUI.TextField(new Rect(685, 322, 195, 44), loginNick,textStyle); //here you will insert the new value to variable loginNick
        loginPassword = GUI.TextField(new Rect(685, 402, 195, 44), loginPassword,textStyle); //same as above, but for password

        if (GUI.Button(new Rect(10, 60, 100, 20), "Login"))
        { //just a button
            StartCoroutine(Login());
        }
        if (GUI.Button(new Rect(10, 120, 100, 20), "Create account"))
        { //just a button
            StartCoroutine(CreateAccount());
        }
        GUI.TextArea(textrect, formText);
    }

    private IEnumerator Login()
    {
        var form = new WWWForm(); //here you create a new form connection
        form.AddField("myform_hash", hash); //add your hash code to the field myform_hash, check that this variable name is the same as in PHP file
        form.AddField("myform_nick", loginNick);
        form.AddField("myform_pass", loginPassword);
        var w = new WWW(LoginURL, form); //here we create a var called 'w' and we sync with our URL and the form
        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        if (w.error != null)
        {
            Debug.Log("Error: " + w.error); //if there is an error, tell us
        }
        else
        {
            Debug.Log("Test ok");
            formText = w.text; //here we return the data our PHP told us
            w.Dispose(); //clear our form in game
        }

        loginNick = ""; //just clean our variables
        loginPassword = "";
    }

    private IEnumerator CreateAccount()
    {
        var form = new WWWForm(); //here you create a new form connection
        form.AddField("myform_hash", hash); //add your hash code to the field myform_hash, check that this variable name is the same as in PHP file
        form.AddField("myform_nick", loginNick);
        form.AddField("myform_pass", loginPassword);
        var w = new WWW(CreateAccountURL, form); //here we create a var called 'w' and we sync with our URL and the form
        yield return w; //we wait for the form to check the PHP file, so our game dont just hang
        if (w.error != null)
        {
            Debug.Log("Error: " + w.error); //if there is an error, tell us
        }
        else
        {
            Debug.Log("Test ok");
            formText = w.text; //here we return the data our PHP told us
            w.Dispose(); //clear our form in game
        }

        loginNick = ""; //just clean our variables
        loginPassword = "";
    }
}