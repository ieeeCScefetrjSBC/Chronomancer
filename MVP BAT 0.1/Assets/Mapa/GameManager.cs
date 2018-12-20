using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static public GameManager GM;

    public bool gotPortalKey = true;
    public int level = 0;
    public GameObject cy, vi;

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
            
            if (sequence[s, level])
            {
                vi.SetActive(true);
                cy.SetActive(false);
            }
            else
            {
                vi.SetActive(false);
                cy.SetActive(true);
            }

            level++;
        }
        else
        {
            pause.SkillsDel = false;
            SceneManager.LoadSceneAsync("TimeLord");
            SceneManager.UnloadSceneAsync("CyberPunk");
            Player.Insta.transform.position = new Vector3(0, -140, 0);
        }
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.PageUp))
        {
            gotPortalKey = true;
            NextLevel();
        }
    }

}
