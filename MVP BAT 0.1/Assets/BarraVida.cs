using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour {

    public Image cima, baixo;

    static float maxVida;

    void Start () {
        cima.fillAmount = 1;
        baixo.fillAmount = 0;
        if(maxVida == 0) maxVida = Player.Insta.vida;

    }
	
	
	void Update () {
        cima.fillAmount = Player.Insta.vida/maxVida;
        baixo.fillAmount = 1-cima.fillAmount;
    }
}
