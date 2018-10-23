﻿using System.Collections;
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
    [SerializeField]
    private float callTime;

    private bool attackAble = true;
    private Player pt;

    void Start()
    {
        StartCode();
        
    }

    void Update()
    {
        UpdateCode();

        if ((Player.position - transform.position).magnitude > distSegue) return;

        if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
        Vector2 dir = (PathFinding.instance.path[0] - transform.position);
        rb.velocity = dir.normalized * vel;

    }

    private void OnCollisionStay2D(Collision2D ou)
    {
        if(ou.transform != null)
        {
            Player p = ou.transform.GetComponent<Player>();

            if (p != null && attackAble)
            {
                pt = p;
                attackAble = false;
                Invoke("Attack", callTime);
                Invoke("FimAttack", attackTime);
                Debug.Log("Vou Bater");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D ou)
    {
        if (ou.transform != null)
        {
            Player p = ou.transform.GetComponent<Player>();

            if (p != null)
            {
                pt = null;
            }
        }
    }

    void Attack(){
        if (pt != null)
        {
            pt.vida -= dano;
            pt = null;
            Debug.Log("Bati");
        }
    }

    void FimAttack() { attackAble = true; }
}
