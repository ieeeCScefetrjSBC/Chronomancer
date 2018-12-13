using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour {

    Transform port;
    Transform player;

	void Start () {
        port = FindObjectOfType<MapManager>().portal.transform;
        player = Player.Insta.transform;
	}
	
	void Update () {
        Vector2 a = port.position - player.position;
        transform.rotation = Quaternion.Euler(0,0, Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90);
	}
}
