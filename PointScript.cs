using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointScript : MonoBehaviour {
    public int points;
	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = "+"+points.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0, 0.0001f, 0f));
        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a-0.01f);
        if (GetComponent<Text>().color.a <= 0)
        {
            Destroy(gameObject);
        }

    }
}
