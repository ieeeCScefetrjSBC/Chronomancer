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
    [SerializeField]
    private float lockTime;

    private bool attackAble = true, attacking;
    private bool procPos = true;
    private Vector3 Mpos;
    private Vector3 playerLast;
    private Player pt;
    private LineRenderer ln;


    public Transform alvo;

    void Start()
    {
        StartCode();
        ln = GetComponent<LineRenderer>();

    }

    void Update()
    {
        UpdateCode();

        RaycastHit2D visao = Physics2D.Raycast(transform.position, Player.position - transform.position, distSegue);




        if (visao)
        {
            if (attacking)ln.SetPositions(new Vector3[] {transform.position + Vector3.back, (Vector3) visao.point + Vector3.back});
            else if (!attackAble) ln.SetPositions(new Vector3[] {transform.position + Vector3.back, playerLast + Vector3.back});
            else ln.SetPositions(new Vector3[] {transform.position});



            if (visao.transform.tag == "Player" && attackAble)
            {
                attackAble = false;
                attacking = true;
                

                Invoke("Lock", lockTime);
                Invoke("Attack", callTime);
                Invoke("FimAttack", attackTime);

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
                            }
                        }
                    }

                    alvo.position = Mpos;
                    procPos = false;
                    Invoke("procurar", 5f);
                }

                if ((transform.position - Mpos).magnitude > 1f)
                {
                    if (!PathFinding.instance.FindPath(Mpos, Player.position)) return;
                    Vector2 dir = (PathFinding.instance.path[0] - transform.position);
                    rb.velocity = dir.normalized * vel;
                }
            }
        }
    }

    void procurar() { procPos = true; }

    void Attack()
    {
        RaycastHit2D visao = Physics2D.Raycast(transform.position, Player.position - transform.position, distSegue);

        if (visao)
        {
            if (visao.transform.tag == "Player")
            {
                Player.CausarDano(dano);
                Debug.Log("Atirei");
            }
        }
    }

    void FimAttack() { attackAble = true; }

    void Lock() { playerLast = Player.position; attacking = false; }


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