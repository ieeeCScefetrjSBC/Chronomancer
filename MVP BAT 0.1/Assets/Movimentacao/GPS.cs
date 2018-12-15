using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour {

    Transform key;
    Transform port;
    Transform player, alvo;

	void Start () {
        key = FindObjectOfType<PortalKey>().transform;
        port = FindObjectOfType<MapManager>().portal.transform;
        player = Player.Insta.transform;
	}
	
	void Update () {
        if(FindObjectOfType<PortalKey>() != null) key = FindObjectOfType<PortalKey>().transform;

        if (key == null)
        {
            if (FindObjectOfType<MapManager>() != null) port = FindObjectOfType<MapManager>().portal.transform;

            if (port == null) {
                alvo = transform;
            }
            else
            {
                alvo = port;
            }
        }
        else
        {
            alvo = key;
        }

        Vector2 a = alvo.position - player.position;
        transform.rotation = Quaternion.Euler(0,0, Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90);
	}
}
