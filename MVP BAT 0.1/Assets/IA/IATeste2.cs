using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATeste2 : Inimigo
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
    private bool procPos = true;
    private Vector3 Mpos;
    private Player pt;

    public Transform alvo;

    void Start()
    {
        StartCode();

    }

    void Update()
    {
        UpdateCode();

        RaycastHit2D visao = Physics2D.Raycast(transform.position, Player.position - transform.position, distSegue);

        if (!visao) return;
        if (visao.transform.tag == "Player" && attackAble)
        {

            attackAble = false;
        }
        
        else
        {
            if (procPos)
            {
                Mpos = transform.position;
                float dismax = 0;

                for (float i = 0; i < 360; i++)
                {
                    Vector2 v = Vector2.up.Rotate(i);
                    RaycastHit2D volta = Physics2D.Raycast(Player.position, v);

                    if (volta)
                    {
                        if (volta.distance > dismax)
                        {
                            dismax = volta.distance;
                            Mpos = volta.point - v;
                            alvo.position = Mpos;
                            procPos = false;
                            Invoke("procurar", 5f);
                        }
                    }
                }
            }

            if ((transform.position - Mpos).magnitude > 1f)
            {
                if (!PathFinding.instance.FindPath(Mpos, Player.position)) return;
                Vector2 dir = (PathFinding.instance.path[0] - transform.position);
                rb.velocity = dir.normalized * vel;
            }
        }

        

    }

    void procurar() {   procPos = true; }

    private void OnCollisionStay2D(Collision2D ou)
    {
        if (ou.transform != null)
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

    void Attack()
    {
        if (pt != null)
        {
            pt.vida -= dano;
            Debug.Log("Bati");
        }
    }

    void FimAttack() { attackAble = true; }
}

public static class Vector2Extension
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
}