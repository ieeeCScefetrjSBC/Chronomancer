using System;
using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using UnityEngine;
using Random = UnityEngine.Random;

//using UnityEditor;

public class SkillBoss : Inimigo
{

    public bool TimeLord;

    public int timeDilation, watchNmuber, enemieNumber;
   
    public GameObject[] enemies, watches;
   
    

    [Space] [Space] public bool CyberPunk;

    public Transform centerArena;
    public List<Transform> positionsMovement; 
    public GameObject Gelo;
    public float danoGelo, tempoGelo;

    private TrailRenderer tr;
    private EdgeCollider2D ec;




    public delegate void SkillB();
    public static SkillB skill1, skill2, skill3;


    private int state;

	void Start ()
	{
        StartCode();
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
            InvokeRepeating("CalcPoints", 0.5f, 0.02f);

	    }
	}

    void Update()
    {
        UpdateCode();
        if (vida <= 0) Deah.deah(false); //Vioria
    }

    void CalcPoints()
    {
        Vector2[] pontos = new Vector2[tr.positionCount];
        for (int i = 0; i < tr.positionCount; i++)
        {
            pontos[i] = tr.GetPosition(i)-transform.position;
        }

        ec.points = pontos;


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

    private void OnTriggerEnter2D(Collider2D ou)
    {
        if (CyberPunk)
        {
            Quimica qui = ou.transform.GetComponent<Quimica>();
            if (qui != null) qui.humidade = 1;
            Player i = ou.transform.gameObject.GetComponent<Player>();
            if (i != null)
            {
                i.enabled = false;
                Instantiate(Gelo, ou.transform);
                i.descongelar(tempoGelo);


                Player.CausarDano(danoGelo);//Causar Dano
            }
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