using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlockRegion : MonoBehaviour {

    public bool pl;
    public float dano;
    public float tempo;
    public GameObject Gelo;

    public void Finish() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D ou)
    {
        if (ou.transform != null)
        {
            Quimica qui = ou.transform.GetComponent<Quimica>();
            if (qui != null) qui.humidade = 1;

            if (pl)
            {
                Inimigo i = ou.transform.gameObject.GetComponent<Inimigo>();
                if (i != null)
                {
                    Rigidbody2D b = ou.transform.gameObject.GetComponent<Rigidbody2D>();
                    b.constraints = RigidbodyConstraints2D.FreezeAll;
                    b.velocity = Vector2.zero;
                    b.angularVelocity = 0;
                    Instantiate(Gelo, ou.transform);
                    i.descongelar(tempo);


                    i.vida -= dano;//Causar Dano
                }
            }
            else {
                Player i = ou.transform.gameObject.GetComponent<Player>();
                if (i != null)
                {
                    i.enabled = false;
                    Instantiate(Gelo, ou.transform);
                    i.descongelar(tempo);


                    i.vida -= dano;//Causar Dano
                }
            }
        }
    }

}
