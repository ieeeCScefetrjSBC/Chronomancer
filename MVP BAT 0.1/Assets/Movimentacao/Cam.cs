using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    public float Vel;
    public float zIni;

    void Start()
    {
        zIni = -transform.position.z;
    }

	void Update () {
        transform.position += (Player.position + ((Player.vel.magnitude + zIni)*Vector3.back) + Player.vel / 2 - transform.position) * Vel;
	}
}
