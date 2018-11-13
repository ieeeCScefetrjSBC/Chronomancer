using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Destroy(MapManager.p.holder.gameObject);
            MapManager.p.InitMap();
        }

    }

}
