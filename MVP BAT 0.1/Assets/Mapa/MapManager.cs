using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // Use this for initialization

    static public MapManager p; 


    public GameObject player;
    public GameObject floor;
    public GameObject cruzamento;
    public GameObject paredeV;
    public GameObject paredeH;
    public GameObject portal;
    public GameObject[] inimigos;
    public Tunnelers.Config config;

    public Transform holder;

    private void Awake()
    {
        if(p == null){
            p = this;
        }else{
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        InitMap();
    }
    

    public void InitMap()
    {
        holder = new GameObject("LevelHolder").transform;

        Tunnelers map = new Tunnelers();
        map.c = config;
        map.Generate();

        GameObject instance;

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

                    


                    if (Random.value < 0.05f)
                    {
                        instance = Instantiate(inimigos[(int)Mathf.Round(Random.value * (inimigos.Length - 1))], reference.transform.position + Vector3.back, Quaternion.identity);
                        instance.transform.parent = holder;
                    }

                }

        player.transform.position = (Vector2)map.begin * 40;
        portal.transform.position = (Vector2)map.end * 40;

        PathFinding.instance.InitMap(new Vector2(config.width * 40, config.height * 40));
    }


}
