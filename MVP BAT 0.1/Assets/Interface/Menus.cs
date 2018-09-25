using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour {

	void Start () {
		
	}
	
	public void Sair () {
        Application.Quit();
	}

    public void AbrirFechar(){
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
