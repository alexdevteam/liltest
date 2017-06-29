using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTextCreator : MonoBehaviour {
    public static PointTextCreator instance;
    public GameObject pointTextPrefab;
	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PointText(int points)
    {
        GameObject x = Instantiate(pointTextPrefab, transform, false);
        x.GetComponent<RectTransform>().anchoredPosition = new Vector2(32.17f, 8f);
        x.GetComponent<PointScript>().points = points;
    }
}
