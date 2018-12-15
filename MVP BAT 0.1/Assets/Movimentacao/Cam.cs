using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {

    public float Vel;
    private float zIni, yIni;

    void Start()
    {
        Camera.main.aspect = 16 / 9;
        zIni = -transform.position.z;
        yIni = -transform.position.z;
    }

	void Update () {
        transform.position += (Player.position + yIni*Vector3.down/2 + ((Player.vel.magnitude + zIni)*Vector3.back) + Player.vel / 2 - transform.position) * Vel;
	}
}
