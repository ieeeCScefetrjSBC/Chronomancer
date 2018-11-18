using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainRegion : MonoBehaviour {

    public bool pl;
    public float dano;
    
    float time = 0;
    Collider2D col;

    private void Awake()
    {
        
        col = GetComponent<Collider2D>();
    }

    public void Finish()
    {
        
        Debug.Log("Fim");
        Destroy(gameObject);
    }

    private void Update()
    {
        
        if(Time.time > time + 0.5f)
        {
            time = Time.time;
            col.enabled = !col.enabled;
            
        }
    }
    
    private void OnTriggerEnter2D (Collider2D ou)
    {
       
        if (ou.transform != null)
        {
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
