using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orial : MonoBehaviour {

    public GameObject botMen;

    public Dialogue dial2;

	
	public void BotMen () {
        Invoke("BotMen2", 1f);
	}

    public void BotMen2()
    {
        botMen.SetActive(true);
    }

    public void Dia()
    {
        Invoke("Dia2", 6f);
    }

    public void Dia2()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dial2);
    }
}
