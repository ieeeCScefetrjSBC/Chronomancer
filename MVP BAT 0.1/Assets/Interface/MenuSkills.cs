using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSkills : MonoBehaviour {

    static public Transform[] posKill;

    public int skillNumber = 0;
    public int skillIdentifier = 0;

    MenuSkills ocupado;
    Player.SKILL skillAtual;
    Vector3 ini;
    Vector3 diff;

    void Start(){
        if (posKill == null) posKill = new Transform[4];


        

        if (skillNumber != 0) posKill[skillNumber - 1] = transform;
        ini = transform.position;
    }

    public void iniMover(){
        diff = transform.position - Input.mousePosition;

        if (skillIdentifier != 0)
        {
            switch (skillIdentifier)
            {
                case (1):
                    skillAtual = SkillUser.Ice_Block_del;
                    break;
                case (2):
                    skillAtual = SkillUser.Chain_Lightning_del;
                    break;
                case (3):
                    skillAtual = SkillUser.Crippling_Oil_del;
                    break;
            }

        }

    }

    public void mover () {
        transform.position = Input.mousePosition + diff;
	}

    public void fimMover()
    {
        float[] dis = new float[4];
        for (int i = 0; i < posKill.Length; i++) dis[i] = (transform.position - posKill[i].position).magnitude;

        if (Mathf.Min(dis) < 80)
        {
            int n = Array.IndexOf(dis, Mathf.Min(dis));
            var ou = posKill[n].GetComponent<MenuSkills>().ocupado;

            if (ou == null) {
                ou = this;
                ocupado = ou;
                transform.position = posKill[n].position;
                if (n == 0) Player.skill1 = skillAtual;
                else if (n == 1) Player.skill2 = skillAtual;
                else if (n == 2) Player.skill3 = skillAtual;
                else if (n == 3) Player.skill4 = skillAtual;
            } else {
                transform.position = ini;
            }
        }
        else
        {
            if (ocupado != null){
                ocupado.ocupado = null;
                ocupado = null;
            }
            transform.position = ini;
        }
    }
}
