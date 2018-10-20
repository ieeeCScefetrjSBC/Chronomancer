using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATeste : Inimigo
{

    [Space]
    [SerializeField]
    private float vel;
    [SerializeField]
    private float distSegue;
    [SerializeField]
    private float dano;
    [SerializeField]
    private float attackTime;

    private bool attackAble = true;

    void Start()
    {
        StartCode();
        
    }

    void Update()
    {
        UpdateCode();

        if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
        
        Vector2 dir = (PathFinding.instance.path[0] - transform.position);
        rb.velocity = dir.normalized * vel;
        //if (dir.magnitude < distSegue) rb.velocity = dir.normalized * vel;

    }

    private void OnCollisionStay2D(Collision2D ou)
    {
        if(ou.transform != null)
        {
            Player p = ou.transform.GetComponent<Player>();

            if (p != null && attackAble) {
                p.vida -= dano;
                attackAble = false;
                Invoke("Attack", attackTime);
            }
        }
    }

    void Attack() { attackAble = true; }
}
