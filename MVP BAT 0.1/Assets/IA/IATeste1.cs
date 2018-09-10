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
    private float skill1time;

    private SkillUser skills;
    private bool skill1Able = true;


    void Start () {
        StartCode();
        skills = GetComponent<SkillUser>();
    }
	
	void Update () {
        UpdateCode();

        Vector2 dir = (Player.position - transform.position);
        if (dir.magnitude > distSegue.x && dir.magnitude < distSegue.y) rb.velocity = dir.normalized*vel;
        if (dir.magnitude <= distSegue.x) rb.velocity *= 0.9f;

        if (dir.magnitude <= distSegue.x && rb.velocity.magnitude < 0.1f && skill1Able)
        {
            skills.Ice_Block((Vector2)transform.position + dir.normalized, dir.normalized, 3, 10, false);
           
            skill1Able = false;
            Invoke("skill1", skill1time);
        }

    }

    void skill1()
    {
        skill1Able = true;
    }
}
