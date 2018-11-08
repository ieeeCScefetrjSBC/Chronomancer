using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    // Use this for initialization

    public GameObject player;
    public GameObject floor;
    public GameObject paredeV;
    public GameObject paredeH;
    public GameObject[] inimigos;
    public Tunnelers.Config config;


    private void Start()
    {
        Tunnelers map = new Tunnelers();
        map.c = config;
        map.Generate();

        for (int x = 0; x < config.width; x++)for(int y = 0; y < config.height; y++) if (map.map[x, y] == 2)
                {
                    GameObject instance = Instantiate(floor, new Vector3(x, y, 0) * 40, Quaternion.identity);

                    if (map.map[x - 1, y] != 2) Instantiate(paredeV, instance.transform.position  + Vector3.back + Vector3.left * (40 / 2), Quaternion.identity);
                    if (map.map[x + 1, y] != 2) Instantiate(paredeV, instance.transform.position + Vector3.back + Vector3.right * (40 / 2), Quaternion.identity);
                    
                    if (map.map[x, y + 1] != 2) Instantiate(paredeH, instance.transform.position + Vector3.back + Vector3.up * (40 / 2), Quaternion.identity);
                    if (map.map[x, y - 1] != 2) Instantiate(paredeH, instance.transform.position + Vector3.back + Vector3.down * (40 / 2), Quaternion.identity);

                    if (Random.value < 0.05f)
                        Instantiate(inimigos[(int)Mathf.Round(Random.value * (inimigos.Length - 1))], instance.transform.position + Vector3.back, Quaternion.identity);
                }

        player.transform.position = (Vector2)map.begin * 40;

        PathFinding.instance.InitMap(new Vector2(config.width * 40, config.height * 40));
    }
    


}
