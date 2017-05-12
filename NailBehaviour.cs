using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityBehaviour))]
public class NailBehaviour : MonoBehaviour {
    GameObject lastAnchorPoint=null;
	// Use this for initialization
	void Start () {
        GetComponent<EntityBehaviour>().interactMethod = OnInteract;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnInteract()
    {
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y-0.35f, transform.position.z), transform.forward);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.35f, transform.position.z), transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit, 0.3f))
        {
            if (hit.transform.gameObject.GetComponent<EntityBehaviour>().canNail)
            {
                if (lastAnchorPoint == null)
                {
                    Destroy(gameObject.GetComponent<Rigidbody>());
                    gameObject.GetComponent<BoxCollider>().isTrigger = true;
                    lastAnchorPoint = hit.transform.gameObject;
                    transform.Translate(new Vector3(0f, 0f, 0.1f));
                }
                else
                {
                    transform.Translate(new Vector3(0f, 0f, 0.1f));
                    FixedJoint joint = hit.transform.gameObject.AddComponent<FixedJoint>();
                    joint.connectedBody = lastAnchorPoint.GetComponent<Rigidbody>();
                    lastAnchorPoint = hit.transform.gameObject;
                }
            }else
            {
                Debug.LogWarning("Nail: Body not nailable!");
            }
        }else
        {
            Debug.LogWarning("Nail: No body to attach!");
        }
    }
}
