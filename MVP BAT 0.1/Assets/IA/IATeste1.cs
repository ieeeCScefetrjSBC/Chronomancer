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
    private int skillIdentifier = 0;
    [SerializeField]
    private float skill1Time;
    [SerializeField]
    private float skill1TellTime;

    private SkillUser skills;
    private Animator anim;
    private bool skill1Able = true;
    private Vector2 dirF;


    void Start () {
        StartCode();
        skills = GetComponent<SkillUser>();
        anim = GetComponent<Animator>();
        }
	
	void Update () {
        UpdateCode();

        Vector3 dir = (Player.position - transform.position);
	    if (dir.magnitude > distSegue.x && dir.magnitude < distSegue.y)
	    {
            anim.SetInteger("Velocity", 1);

            Vector2 dir1;

            var h = Physics2D.Raycast(transform.position, dir);

            if (h != null)
            {
                if (h.transform.tag != "Player")
                {
                    if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
                    dir1 = (PathFinding.instance.path[0] - transform.position);
                }
                else
                {
                    dir1 = dir;
                }
                
            }
            else
            {
                if (!PathFinding.instance.FindPath(transform.position, Player.position)) return;
                dir1 = (PathFinding.instance.path[0] - transform.position);
            }

            rb.velocity = dir1.normalized * vel;

            float zAng = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            int dirVar = (zAng < 45 && zAng > -45) ? 0 : ((zAng < 135 && zAng > 45) ? 1 : (zAng < -135 || zAng > 135) ? 2 : 3);
            anim.SetInteger("Direction", dirVar);
        }
        else
        {
            anim.SetInteger("Velocity", 0);
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
        if (skillIdentifier != 0)
        {
            switch (skillIdentifier)
            {
                case (1):
                    skills.Ice_Block((Vector2)transform.position + dirF.normalized, dirF.normalized, 3, 10, false);
                    break;
                case (2):
                    skills.Chain_Lightning((Vector2)transform.position + dirF.normalized, dirF.normalized, 3, 10, false);
                    break;
                case (3):
                    skills.Crippling_Oil((Vector2)transform.position + dirF.normalized, dirF.normalized, 3, 10, false);
                    break;
                case (4):
                    skills.Meteor((Vector2)transform.position + dirF.normalized, dirF.normalized, 3, 10, false);
                    break;
                case (5):
                    skills.Heavy_Rain((Vector2)transform.position + dirF.normalized, dirF.normalized, 3, 10, false);
                    break;
            }

        }
        
    }

    void skill1Fim()
    {
        skill1Able = true;
    }
}
