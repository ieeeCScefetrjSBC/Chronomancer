using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalKey : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            GameManager.GM.gotPortalKey = true;
            pause.SkillsDel = false;
            SceneManager.LoadSceneAsync("TimeLord");
            SceneManager.UnloadSceneAsync("CyberPunk");
            Player.Insta.transform.position = new Vector3(0,-140,0);
            Destroy(gameObject);


        }
    }
}
