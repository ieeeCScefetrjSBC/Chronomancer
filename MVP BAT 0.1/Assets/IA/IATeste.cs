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

    private Animator anim;
    private bool attackAble = true;
    private bool attacking = false;

    void Start()
    {
        StartCode();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateCode();

        Vector3 dir = (Player.position - transform.position);

        float zAng = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        int dirVar = (zAng < 45 && zAng > -45) ? 0 : ((zAng < 135 && zAng > 45) ? 1 : (zAng < -135 || zAng > 135) ? 2 : 3);
        anim.SetInteger("Direction", dirVar);

        if (dir.magnitude > distSegue.y)
        {
            anim.SetInteger("Velocity", 0);
            return;
        }

        if (dir.magnitude < distSegue.x && attackAble)
        {
            attackAble = false;
            Invoke("Attack", callTime);
            Invoke("FimAttack", attackTime);
        }

        if (!attackAble){
            anim.SetInteger("Velocity", 0);
            return;
        }

        anim.SetInteger("Velocity", 1);

        if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
        Vector2 dir1 = (PathFinding.instance.path[0] - transform.position);
        rb.velocity = dir1.normalized * vel;

    }

    private void OnCollisionEnter2D(Collision2D ou)
    {
        if(ou.transform != null)
        {
            Player p = ou.transform.GetComponent<Player>();

            if (p != null && attacking)
            {
                Player.CausarDano(dano);
            }
        }
    }

  

    void Attack(){
        attacking = true;
        rb.AddForce((Player.position - transform.position)*100);
        
        
    }

    void FimAttack() { attackAble = true; attacking = false; }
}
