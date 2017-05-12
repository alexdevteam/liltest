using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityBehaviour))]
public class VaultBehaviour : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<EntityBehaviour>().interactMethod = OnInteract;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnInteract()
    {
        Animator anim = transform.GetChild(0).GetComponent<Animator>();
        anim.SetTrigger("Interaction");
    }

}
