﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // Use this for initialization

    static public MapManager MM;

    [Range(.05f, 1f)]
    public float inimigosPercentual;

    public GameObject background;
    public GameObject player;
    public GameObject paredeV;
    public GameObject paredeH;
    public GameObject portal;
    public GameObject portalKey;

    public bool viking;

    public Color32 cyberpunkBackgroundColor;
    public GameObject cyberpunkFloor;
    public GameObject cyberpunkCruzamento;
    public GameObject[] cyberpunkScenario;
    public GameObject[] cyberpunkInimigos;

    public Color32 vikingBackgroundColor;
    public GameObject vikingFloor;
    public GameObject vikingCruzamento;
    public GameObject[] vikingScenario;
    public GameObject[] vikingInimigos;
    
    public Tunnelers.Config config;

    public Transform holder;

    Vector2Int[] dirs = new Vector2Int[4]
    {
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right,
        Vector2Int.up
    };

	Vector3[] rotate = new Vector3[4]
	{
	    Vector3.down,
	    Vector3.left,
	    Vector3.right,
	    new Vector3(.1f, 10, 0)
	};

    private void Awake()
    {
        if(MM == null){
            MM = this;
        }else{
            Destroy(gameObject);
        }
        holder = new GameObject("LevelHolder").transform;
    }
    

    public void InitMap(bool vi)
    {
        holder = new GameObject("LevelHolder").transform;

        Tunnelers map = new Tunnelers();
        map.c = config;
        map.Generate();

        GameObject instance;
        GameObject cruzamento;
        GameObject floor;

        viking = vi;

        bool genKey = true;

        bool[] eSpawn = new bool[map.tCount];

        for (int zifoda = 0; zifoda < eSpawn.Length; zifoda++) eSpawn[zifoda] = false;
        for (int z = 0; z < eSpawn.Length * inimigosPercentual; z++)
        {
            eSpawn[z] = true;
        }

        ShuffleArray(ref eSpawn);

        if (viking)
        {
            background.GetComponent<SpriteRenderer>().color = vikingBackgroundColor;
            floor = vikingFloor;
            cruzamento = vikingCruzamento;
        }
        else
        {
            background.GetComponent<SpriteRenderer>().color = cyberpunkBackgroundColor;
            floor = cyberpunkFloor;
            cruzamento = cyberpunkCruzamento;
        }

        int fodaze = 0;

        for (int x = 0; x < config.width; x++) for (int y = 0; y < config.height; y++) if (map.map[x, y] == 2)
                {

                    bool h = false;
                    bool v = false;

                    if (map.map[x, y - 1] == 2) h = true;
                    if (map.map[x, y + 1] == 2) h = true;
                    if (map.map[x - 1, y] == 2) v = true;
                    if (map.map[x + 1, y] == 2) v = true;

                    GameObject reference;

                    if (h && v)
                    {
                        reference = Instantiate(cruzamento, new Vector3(x, y, 0) * 40, Quaternion.identity);
                    }
                    else
                    {
                        reference = Instantiate(floor, new Vector3(x, y, 0) * 40, Quaternion.identity);
                    }

                    reference.transform.parent = holder;

                    if (map.map[x, y - 1] != 2 && map.map[x, y + 1] != 2)
                    {
                        reference.transform.Rotate(Vector3.forward, 90);
                    }

                    if (map.map[x - 1, y] != 2)
                    {
                        instance = Instantiate(paredeV, reference.transform.position + Vector3.back + Vector3.left * (40 / 2), Quaternion.identity);
                        instance.transform.parent = holder;
                    }
                    if (map.map[x + 1, y] != 2)
                    {
                        instance = Instantiate(paredeV, reference.transform.position + Vector3.back + Vector3.right * (40 / 2), Quaternion.identity);
                        instance.transform.parent = holder;
                    }

                    if (map.map[x, y + 1] != 2)
                    {
                        instance = Instantiate(paredeH, reference.transform.position + Vector3.back + Vector3.up * (40 / 2), Quaternion.identity);
                        instance.transform.parent = holder;
                    }
                    if (map.map[x, y - 1] != 2)
                    {
                        instance = Instantiate(paredeH, reference.transform.position + Vector3.back + Vector3.down * (40 / 2), Quaternion.identity);
                        instance.transform.parent = holder;
                    }
                    

                    if (eSpawn[fodaze] && (x != map.begin.x || y != map.begin.y))
                    {
                        GameObject inimigo = (viking ? vikingInimigos[Random.Range(0, vikingInimigos.Length)] : cyberpunkInimigos[Random.Range(0, cyberpunkInimigos.Length)]);
                        instance = Instantiate(inimigo, reference.transform.position + Vector3.back, Quaternion.identity);
                        instance.transform.parent = holder;
                        if(genKey)
                        {
                            instance = Instantiate(portalKey, reference.transform.position + Vector3.back, Quaternion.identity);
                            instance.transform.parent = holder;
                            genKey = false;
                        }
                    }

                    fodaze++;

                }
                else
                {
                    GameObject i;

                    if (viking)
                    {

                        //bool teste = false;

                        for (int j = 0; j < dirs.Length; j++)
                        {
                            if (map.InBounds(x + dirs[j].x, y + dirs[j].y) && map.map[x + dirs[j].x, y + dirs[j].y] == 2)
                            {
                                int index = Random.Range(0, vikingScenario.Length);
                                i = Instantiate(vikingScenario[index]);
                                
                                if (index == 1) Arvrsssss(i);
                                else
                                {
                                    Vector3 scale = i.transform.localScale;
                                    scale.z = Random.Range(2, 4);
                                    i.transform.localScale = scale;
                                    i.transform.rotation = Quaternion.FromToRotation(rotate[0], rotate[j]);
                                }
                                i.transform.position = new Vector3(x, y, 0) * 40 - new Vector3(0, 0, 2 * i.transform.localScale.z);
                                i.transform.parent = holder;
                                //teste = true;
                                break;
                            }
                        }

                 
                        //SE A FASE VIKING ESTIVER TRAVANDO COMENTE O IF ABAIXO
                        //if (!teste)
                        //{
                        //    i = Instantiate(casaviking[1]);
                        //    i.transform.position = new Vector3(x, y, 0) * 40 - new Vector3(0, 0, 2 * i.transform.localScale.z);
                        //    Arvrsssss(i);
                        //    i.transform.parent = holder;
                        //}

                    }
                    else
                    {
                        for (int j = 0; j < dirs.Length; j++)
                        {
                            if (map.InBounds(x + dirs[j].x, y + dirs[j].y) && map.map[x + dirs[j].x, y + dirs[j].y] == 2)
                            {
                                
                                i = Instantiate(cyberpunkScenario[Random.Range(0, cyberpunkScenario.Length)]);
                                Vector3 scale = i.transform.localScale;
                                scale.z = Random.Range(2, 4);
                                i.transform.localScale = scale;

                                i.transform.position = new Vector3(x, y, 0) * 40 - new Vector3(0, 0, 5 * i.transform.localScale.z);

                                i.transform.rotation = Quaternion.FromToRotation(rotate[0], rotate[j]);
                                i.transform.parent = holder;

                                break;
                            }
                        }
                    }

                    



                }

        Player.Insta.transform.position = (Vector2)map.begin * 40;
        portal.transform.position = (Vector3)(Vector2)map.end * 40 + 7 * Vector3.back;

        PathFinding.instance.InitMap(new Vector2(config.width * 40, config.height * 40));
    }


    void Arvrsssss(GameObject arvrs)
    {
        for(int i = 0; i < arvrs.transform.childCount; i++)
        {
            float x = Random.Range(-15, 15);
            float y = Random.Range(-15, 15);
            arvrs.transform.GetChild(i).localScale = arvrs.transform.GetChild(i).localScale * Random.Range(.5f, 1.5f);
            arvrs.transform.GetChild(i).localPosition = new Vector3(x, y, 0);
        }
    }


    void ShuffleArray(ref bool[] ts)
    {
        var count = ts.Length;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

