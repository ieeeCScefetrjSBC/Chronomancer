using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaBoss : MonoBehaviour {


    public Image Life;
    public Transform hora, minuto;
    public Dialogue dia;

    float posHora, posMin, maxLife;
    SkillBoss Boss;

    void Start () {
        Boss = FindObjectOfType<SkillBoss>();
        maxLife = Boss.vida;
        FindObjectOfType<DialogueManager>().StartDialogue(dia);
	}
	
	void Update () {
        hora.rotation = Quaternion.Euler(posHora + 1, -90, 90).normalized;
        posHora += 1;
        minuto.rotation = Quaternion.Euler(posMin + 12, -90, 90).normalized;
        posMin += 12;

        Life.fillAmount = Boss.vida/maxLife;
    }
}
