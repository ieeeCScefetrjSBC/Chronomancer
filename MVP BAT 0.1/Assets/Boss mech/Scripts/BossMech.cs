using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMech : MonoBehaviour {

    public float delaySkill1;
    public float delaySkill2;
    public float delaySkill3;

    Animator animator;

    bool skillPlaying = false;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        
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
            }
        }
        


	}

    IEnumerator UseSkill1()
    {
        animator.SetBool("Attack1", true);
        yield return new WaitForSeconds(delaySkill1);
        SkillBoss.skill1();
        animator.SetBool("Attack1c", true);
        animator.SetBool("Attack1", false);
        
        yield return new WaitForSeconds(delaySkill1);

        animator.SetBool("Attack1", false);
        animator.SetBool("Attack1c", false);
        skillPlaying = false;
    }

    IEnumerator UseSkill2()
    {

        animator.SetBool("Attack2", true);
        yield return new WaitForSeconds(delaySkill2);
        SkillBoss.skill2();
        animator.SetBool("Attack2c", true);
        animator.SetBool("Attack2", false);

        yield return new WaitForSeconds(delaySkill2);

        animator.SetBool("Attack2", false);
        animator.SetBool("Attack2c", false);      
        skillPlaying = false;
    }

    IEnumerator UseSkill3()
    {
        animator.SetBool("Attack3", true);
        yield return new WaitForSeconds(delaySkill3);
        SkillBoss.skill3();
        animator.SetBool("Attack3c", true);
        animator.SetBool("Attack3", false);

        yield return new WaitForSeconds(delaySkill3);

        animator.SetBool("Attack3", false);
        animator.SetBool("Attack3c", false);
        skillPlaying = false;
    }


}
