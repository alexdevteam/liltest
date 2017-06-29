using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Texts());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Texts()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        while (transform.GetChild(2).GetComponent<Text>().fontSize < 72)
        {
            transform.GetChild(2).GetComponent<Text>().fontSize++;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(1);
    }
}
