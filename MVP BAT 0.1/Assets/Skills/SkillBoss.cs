using System;
using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEngine;
using Random = UnityEngine.Random;

//using UnityEditor;

public class SkillBoss : MonoBehaviour
{

    public bool TimeLord;

    public int timeDilation, watchNmuber, enemieNumber;
    public GameObject[] enemies, watches;
    

    [Space] [Space] public bool CyberPunk;

    public Transform centerArena;

    private TrailRenderer tr;
    private EdgeCollider2D ec;


    [Space] [Space] public bool Viking;

    public Transform spawPoints;



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

	    if (CyberPunk)
	    {
	        tr = GetComponent<TrailRenderer>();
	        ec = GetComponent<EdgeCollider2D>();
            InvokeRepeating("CalcPoints", 0.5f, 0.03f);

	    }
	}

    void CalcPoints()
    {
        Vector2[] pontos = new Vector2[tr.positionCount];
        for (int i = 0; i < tr.positionCount; i++)
        {
            pontos[i] = tr.GetPosition(i)-transform.position;
        }

        ec.points = pontos;

        Debug.Log("as");
        foreach (var p in pontos)
        {
            Debug.Log(p);
        }
    }

    void SlowMotion()
    {
        Player.Insta.maxRunVel *= timeDilation;
        Player.Insta.maxVel *= timeDilation;


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

    }

    void DropWatches()
    {
        for (int i = 0; i < watchNmuber; i++)
        {
            Instantiate(watches[Random.Range(0, watches.Length)], Random.insideUnitCircle.normalized * Random.Range(2, 10), Quaternion.identity);
        }

       
    }
}

//[CustomEditor(typeof(SkillBoss))]
//public class SkillBossEditor : Editor
//{
//    override public void OnInspectorGUI()
//    {
//        var sb = target as SkillBoss;

//        sb.TimeLord = GUILayout.Toggle(sb.TimeLord, " TimeLord");

//        if (sb.TimeLord)
//            sb.timeDilation = EditorGUILayout.IntSlider("Time Dilation:", sb.timeDilation, 1, 10);

//    }
//}