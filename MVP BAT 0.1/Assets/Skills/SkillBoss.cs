using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss : MonoBehaviour
{
    public const int idle = 0, slowM = 1, invokeE = 2, dropW = 3;

    public int timeDilation, watchNmuber, enemieNumber;
    public GameObject[] enemies, watches;
    public Transform spawPoints;

    private int state;

	void Start () {
		
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
