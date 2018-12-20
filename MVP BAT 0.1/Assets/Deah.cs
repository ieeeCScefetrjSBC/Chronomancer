using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deah : MonoBehaviour {

    public bool more;

    private static Deah d, v;

    private void Awake()
    {
        if (more) d = this;
        else v = this;
        gameObject.SetActive(false);
    }

    public static void deah (bool morte) {
        
        if (morte) d.gameObject.SetActive(true);
        else
        { 

        }

    }

    private void Update()
    {
        if (Input.anyKey && more)
        {
            Player.Insta.enabled = true;
            Player.Insta.vida = 100;
            pause.SkillsDel = false;
            SceneManager.LoadScene("CyberPunk");

        }

        if (Input.anyKey && !more) SceneManager.LoadScene("Credits");
    }

}
