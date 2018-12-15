using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaBoss : MonoBehaviour {


    public Image Life;
    public Transform hora, minuto;
    float posHora, posMin, maxLife;
    SkillBoss Boss;

    void Start () {
        Boss = FindObjectOfType<SkillBoss>();
        maxLife = Boss.vida;
	}
	
	void Update () {
        hora.rotation = Quaternion.Euler(posHora + 1, -90, 90);
        posHora += 1;
        minuto.rotation = Quaternion.Euler(posMin + 12, -90, 90);
        posMin += 12;

        Life.fillAmount = Boss.vida/maxLife;
    }
}
