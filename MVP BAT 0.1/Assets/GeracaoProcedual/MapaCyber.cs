﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapaCyber : MonoBehaviour
{

    private static List<List<MapaCyber>> MatMapaCyber;
    public static List<MapaCyber> sala;
  

    [SerializeField]
    [Range(0f, 1f)]
    private float probSala;
    [SerializeField]
    private int maxX;
    [SerializeField]
    private int maxY;
    [SerializeField]
    private float disBlocos;
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;


    [SerializeField]
    private GameObject paredeV;
    [SerializeField]
    private GameObject paredeH;

    [SerializeField]
    private GameObject[] inimigos;


    public bool mantem;

    void Awake()
    {
        if (MatMapaCyber == null)
        {
            MatMapaCyber = new List<List<MapaCyber>>();
            sala = new List<MapaCyber>();
            

            for (int i = 0; i < maxX; i++)
            {
                MatMapaCyber.Add(new List<MapaCyber>());
                for (int j = 0; j < maxY; j++)
                {
                    if ((i == 0) && (j == 0))
                    {
                        MatMapaCyber[j].Add(this);
                        x = i;
                        y = j;

                        

                        if ((x % 3 == 0) && (y % 3 == 0) && (Random.value < probSala)) sala.Add(this) ;
                    }
                    else
                    {
                        GameObject g = Instantiate(gameObject, transform.position + disBlocos * j * Vector3.down + disBlocos * i * Vector3.right, Quaternion.identity);
                        MapaCyber M = g.GetComponent<MapaCyber>();
                        MatMapaCyber[i].Add(M);
                        M.x = i;
                        M.y = j;

                        M.inimigos = inimigos;

                        M.paredeH = paredeH;
                        M.paredeV = paredeV;

                        if ((M.x%3 == 0) && (M.y % 3 == 0) && (Random.value < probSala)) sala.Add(M);

                    }


                }
            }

        }



    }

    void Start()
    {
        if (x == 0 && y == 0)
        {
            for (int n = 0; n < sala.Count - 1; n++)
            {
                
                sala[n].mantem = true;
                int iniciox = Mathf.Min(sala[n].x, sala[n + 1].x);
                int inicioy = Mathf.Min(sala[n].y, sala[n + 1].y);
                int fimx = Mathf.Max(sala[n].x, sala[n + 1].x);
                int fimy = Mathf.Max(sala[n].y, sala[n + 1].y);
                if (Random.value < 0.5f)
                {
                    for (int i = iniciox; i <= fimx; i++)
                    {
                        MatMapaCyber[i][sala[n].y].mantem = true;
                    }
                    for (int j = inicioy; j <= fimy; j++)
                    {
                        MatMapaCyber[sala[n + 1].x][j].mantem = true;
                    }
                }
                else
                {
                    for (int i = iniciox; i <= fimx; i++)
                    {
                        MatMapaCyber[i][sala[n + 1].y].mantem = true;
                    }
                    for (int j = inicioy; j <= fimy; j++)
                    {
                        MatMapaCyber[sala[n].x][j].mantem = true;
                    }
                }
            }
            for (int i = 0; i< MatMapaCyber.Count; i++)
            {
                for (int j = 0; j < MatMapaCyber[0].Count; j++)
                {
                    if (!MatMapaCyber[i][j].mantem)
                    {
                        Destroy(MatMapaCyber[i][j].gameObject);
                        MatMapaCyber[i][j] = null;
                    }
                 
                }
            }
            
        }

        if (x == 0 || MatMapaCyber[x - 1][y] == null) Instantiate(paredeV, transform.position + Vector3.back + Vector3.left * (disBlocos / 2), Quaternion.identity);
        if (x == maxX - 1 || MatMapaCyber[x + 1][y] == null) Instantiate(paredeV, transform.position + Vector3.back + Vector3.right * (disBlocos / 2), Quaternion.identity);
        if (y == 0 || MatMapaCyber[x][y-1] == null) Instantiate(paredeH, transform.position + Vector3.back + Vector3.up * (disBlocos / 2), Quaternion.identity);
        if (y == maxY - 1 || MatMapaCyber[x][y+1] == null) Instantiate(paredeH, transform.position + Vector3.back + Vector3.down * (disBlocos / 2), Quaternion.identity);
        
    }


    void Update()
    {
        if(this != sala[0])
        {
            if (Random.value < 0.4f)
            {
                Instantiate(inimigos[(int) Mathf.Round(Random.value * (inimigos.Length - 1))], transform.position + Vector3.back, Quaternion.identity);
            }

        }

        if (this == sala[sala.Count - 1])
        {
            PathFinding.instance.InitMap(new Vector2(maxX * 40, maxY * 40));
        }


        Destroy(this);
    }

    
}
