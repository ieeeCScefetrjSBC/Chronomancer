using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class credits : MonoBehaviour {

    public float TempoFim;

	void Start () {
        Invoke("Fim", TempoFim);
	}
	
	void Update () {
        if (Input.anyKey) Fim();
	}

    void Fim()
    {
        SceneManager.LoadScene(0);
    }
}
