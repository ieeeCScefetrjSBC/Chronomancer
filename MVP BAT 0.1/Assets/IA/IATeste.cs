using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATeste : Inimigo
{

    [Space]
    [SerializeField]
    private float vel;
    [SerializeField]
    private Vector2 distSegue;
    [SerializeField]
    private float dano;
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private float callTime;

    private bool attackAble = true;
    private bool attacking = false;

    void Start()
    {
        StartCode();
        
    }

    void Update()
    {
        UpdateCode();

        if ((Player.position - transform.position).magnitude > distSegue.y) return;

        if ((Player.position - transform.position).magnitude < distSegue.x && attackAble)
        {
            attackAble = false;
            Invoke("Attack", callTime);
            Invoke("FimAttack", attackTime);
            Debug.Log("Vou Bater");
        }

        if (!attackAble) return;

        if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
        Vector2 dir = (PathFinding.instance.path[0] - transform.position);
        rb.velocity = dir.normalized * vel;

    }

    private void OnCollisionEnter2D(Collision2D ou)
    {
        if(ou.transform != null)
        {
            Player p = ou.transform.GetComponent<Player>();

            if (p != null && attacking)
            {
                Player.CausarDano(dano);
                Debug.Log("Acertei");
            }
        }
    }

  

    void Attack(){
        attacking = true;
        rb.AddForce((Player.position - transform.position)*100);
        
        
    }

    void FimAttack() { attackAble = true; attacking = false; }
}
