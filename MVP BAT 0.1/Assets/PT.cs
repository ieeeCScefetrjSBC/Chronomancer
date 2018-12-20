using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PT : MonoBehaviour {

    public bool key;

    void Start () {
		
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (key) Destroy(gameObject);
        else
        {
            var na = FindObjectsOfType<PT>();
            foreach(var n in na) Destroy(n.gameObject, 0.2f);
            SceneManager.LoadScene(2);
        }
    }
}
