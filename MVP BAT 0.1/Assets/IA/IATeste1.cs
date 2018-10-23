using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IATeste1 : Inimigo {

    [Space]
    [SerializeField]
    private float vel;
    [SerializeField]
    private Vector2 distSegue;
    [SerializeField]
    private float skill1Time;
    [SerializeField]
    private float skill1TellTime;

    private SkillUser skills;
    private bool skill1Able = true;
    private Vector2 dirF;


    void Start () {
        StartCode();
        skills = GetComponent<SkillUser>();
    }
	
	void Update () {
        UpdateCode();

        Vector3 dir = (Player.position - transform.position);
	    if (dir.magnitude > distSegue.x && dir.magnitude < distSegue.y)
	    {
	        if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
	        Vector2 dir1 = (PathFinding.instance.path[0] - transform.position);
	        rb.velocity = dir1.normalized * vel;
        }

        if (dir.magnitude <= distSegue.x) rb.velocity *= 0.5f;



        if (dir.magnitude <= distSegue.x && skill1Able)
        {


            dirF = dir;
            skill1Able = false;
            Invoke("skill1", skill1TellTime);
            Invoke("skill1Fim", skill1Time);
        }

    }

    void skill1()
    {
        skills.Ice_Block((Vector2)transform.position + dirF.normalized, dirF.normalized, 3, 10, false);
    }

    void skill1Fim()
    {
        skill1Able = true;
    }
}
