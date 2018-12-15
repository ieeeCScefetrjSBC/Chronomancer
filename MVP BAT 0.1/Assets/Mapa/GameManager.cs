using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static public GameManager GM;

    public bool gotPortalKey = true;
    public int level = 0;

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    bool[,] sequence = new bool[2, 4]
    {
        {true, true, false, false },
        {false, false, true, true }
    };
    int s;

    // Use this for initialization
    void Start () {
        s = Random.Range(0, 2);
        NextLevel();
	}

    public void NextLevel()
    {
        if (!gotPortalKey)
        {
            Debug.Log("U DoNt HaVe KeY IdIoT!");
            return;
        }
        Destroy(MapManager.MM.holder.gameObject);
        gotPortalKey = false;
        if(level < 4)
        {
            MapManager.MM.InitMap(sequence[s, level]);
            level++;
            pause.SkillsDel = false;
        }
        else
        {
            SceneManager.LoadScene("TimeLord");
        }
        
    }
	
}
