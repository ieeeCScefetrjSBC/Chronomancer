using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss : MonoBehaviour
{
    public const int idle = 0, slowM = 1, invokeE = 2, dropW = 3;

    public bool TimeLord;
    public int timeDilation, watchNmuber, enemieNumber;
    public GameObject[] enemies, watches;
    public Transform spawPoints;

    [Space] [Space] public bool CyberPunk;

    [Space] [Space] public bool Viking;

    public delegate void SkillB();

    public static SkillB skill1, skill2, skill3;


    private int state;

	void Start ()
	{
	    if (TimeLord)
	    {
	        skill1 = SlowMotion;
	        skill2 = InvokeEnemies;
	        skill3 = DropWatches;
	    }
	}
	
	void Update () {



	    switch (state)
	    {
            case idle:
	            break;
	        case slowM:
	            SlowMotion();
	            break;
	        case invokeE:
	            InvokeEnemies();
                break;
	        case dropW:
	            DropWatches();
                break;
            default:
                state = 0;
                break;

        }
	}

    void SlowMotion()
    {
        Player.Insta.maxRunVel *= timeDilation;
        Player.Insta.maxVel *= timeDilation;

        state = 0;

        Invoke("SlowMotionEnd", 5f);
    }

    void SlowMotionEnd()
    {
        Player.Insta.maxRunVel /= timeDilation;
        Player.Insta.maxVel /= timeDilation;
    }

    void InvokeEnemies()
    {
        for (int i = 0; i < enemieNumber; i++)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], Random.insideUnitCircle.normalized * Random.Range(2, 10), Quaternion.identity);
        }

        state = 0;
    }

    void DropWatches()
    {
        for (int i = 0; i < watchNmuber; i++)
        {
            Instantiate(watches[Random.Range(0, watches.Length)], Random.insideUnitCircle.normalized * Random.Range(2, 10), Quaternion.identity);
        }

        state = 0;
    }
}
