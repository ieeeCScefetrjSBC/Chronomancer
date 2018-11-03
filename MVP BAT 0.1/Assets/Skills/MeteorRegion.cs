using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRegion : MonoBehaviour {

    public bool pl;
    public float dano;

    public void Finish()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D ou)
    {
        if (ou.transform != null)
        {
            Quimica qui = ou.transform.GetComponent<Quimica>();
            if (qui != null) qui.calor = 1000;

            if (pl)
            {
                Inimigo i = ou.transform.gameObject.GetComponent<Inimigo>();
                if (i != null)
                {
                    i.vida -= dano;//Causar Dano
                }
            }
            else
            {
                Player i = ou.transform.gameObject.GetComponent<Player>();
                if (i != null)
                {
                    Player.CausarDano(dano);//Causar Dano
                }
            }
        }
    }

}
