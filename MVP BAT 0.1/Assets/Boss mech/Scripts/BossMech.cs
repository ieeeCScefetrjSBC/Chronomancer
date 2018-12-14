using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMech : MonoBehaviour {

    public float Skill1Duration = 1, Skill1Call = 1;
    public float Skill2Duration = 1, Skill2Call = 1;
    public float Skill3Duration = 1, Skill3Call = 1;

    [Space]
    public float velocity;
    public List<Transform> positionsMovement;

    Rigidbody2D rb;
    Transform Alvo;
    Animator animator;

    bool skillPlaying = false;

    void Start () {
        rb  =  GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Alvo = positionsMovement[0];
	}
	
	// Update is called once per frame
	void Update () {


        if (!skillPlaying)
        {
            skillPlaying = true;
            int val = Random.Range(0, 3);

            switch (val)
            {
                case 0:
                    StartCoroutine("UseSkill1");
                    break;
                case 1:
                    StartCoroutine("UseSkill2");
                    break;
                case 2:
                    StartCoroutine("UseSkill3");
                    break;
                default:
                    break;
            }
        }
        else
        {
            Move();
        }
        


	}

    void StartMove()
    {
        Alvo = positionsMovement[Random.Range(0, positionsMovement.Count)];
        
    }

    void Move()
    {
        Vector2 dir = Alvo.position - transform.position;

        rb.velocity = dir.normalized  * ((dir.magnitude < 2)? dir.magnitude : 2) * velocity;

        if (Mathf.Approximately (dir.magnitude, 0f)) skillPlaying = false;
    }

    IEnumerator UseSkill1()
    {
        animator.SetBool("Attack1", true);
        yield return new WaitForSeconds(Skill1Call);
        if (SkillBoss.skill1 != null) SkillBoss.skill1();
        animator.SetBool("Attack1c", true);
        animator.SetBool("Attack1", false);
        
        yield return new WaitForSeconds(Skill1Duration);

        animator.SetBool("Attack1", false);
        animator.SetBool("Attack1c", false);
        StartMove();
    }

    IEnumerator UseSkill2()
    {

        animator.SetBool("Attack2", true);
        yield return new WaitForSeconds(Skill2Call);
        if (SkillBoss.skill2 != null) SkillBoss.skill2();
        animator.SetBool("Attack2c", true);
        animator.SetBool("Attack2", false);

        yield return new WaitForSeconds(Skill1Duration);

        animator.SetBool("Attack2", false);
        animator.SetBool("Attack2c", false);
        StartMove();
    }

    IEnumerator UseSkill3()
    {
        animator.SetBool("Attack3", true);
        yield return new WaitForSeconds(Skill3Call);
        if(SkillBoss.skill3 != null) SkillBoss.skill3();
        animator.SetBool("Attack3c", true);
        animator.SetBool("Attack3", false);

        yield return new WaitForSeconds(Skill1Duration);

        animator.SetBool("Attack3", false);
        animator.SetBool("Attack3c", false);
        StartMove();
    }


}
