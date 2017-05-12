using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour {
    public Mesh cubeMesh;
    public Animator armsAnim;
    
    private void FixedUpdate()
    {
        Vector3 movement=new Vector3();
        movement.x = Input.GetAxis("Horizontal")*5f*Time.deltaTime;
        movement.z = Input.GetAxis("Vertical")*5f*Time.deltaTime;
        Debug.Log(movement);
        transform.Translate(movement,null);
        transform.LookAt(movement+transform.position, -Vector3.forward);
        transform.eulerAngles = new Vector3(0, -transform.eulerAngles.z,0);
    }
}
