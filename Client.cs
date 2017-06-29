using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Linq;
using System.Text;

public class Client : MonoBehaviour {

    public static Client instance;

    public enum Exceptions
    {
        Success,
        InvalidPass,
        NullPassOrUser,
        DatabaseError,
    }

    enum Commands
    {
        NULL,
        Login,
        CreateAccount,
        Move,
        Destroy,
        Build,
        ModInv,
        Send
    }
    static Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    // Use this for initialization
    void Awake () {
        instance = this;
        DontDestroyOnLoad(gameObject);
        s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        s.Connect("127.0.0.1", 8);
    }

    void Send(byte[] item)
    {
        s.Send(item);
        s.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
    }

    void Send(int item)
    {
        s.Send(BitConverter.GetBytes(item));
        s.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
    }

    void Send(string item)
    {
        s.Send(Encoding.Default.GetBytes(item));
        s.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
    }

    public void Login(string name, string pass)
    {
        Debug.Log("Client >> Sending command n");
        Send((int)Commands.Login);
        Debug.Log("Client >> Sending username");
        Send(name);
        Debug.Log("Client >> Sending password");
        Send(pass);
    }

    public void CreateAccount(string name, string pass)
    {
        Debug.Log("Client >> Sending command n");
        Send((int)Commands.CreateAccount);
        Debug.Log("Client >> Sending username");
        Send(name);
        Debug.Log("Client >> Sending password");
        Send(pass);
    }

    static void callback(IAsyncResult ar)
    {
        try
        {
            s.EndReceive(ar);

            byte[] buf = new byte[8192];

            int rec = s.Receive(buf, buf.Length, 0);

            if (rec < buf.Length)
            {
                Array.Resize(ref buf, rec);
            }

            s.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            Debug.Log("Server >> " + Encoding.Default.GetString(buf));
            if (buf[0] == (int)Exceptions.Success)
            {
                Debug.Log("Server >> Finished successfully");
            }
            
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            Close();
        }
    }

    static void Close()
    {
        s.Close();
        s.Dispose();
    }
}
