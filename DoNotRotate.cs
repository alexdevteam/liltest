using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotRotate : MonoBehaviour {
    public Vector3 fixedRotation;
    public Vector3 fixedPos;
    public Transform target;
    
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(fixedRotation);
        transform.position = target.position - fixedPos;
	}
}
