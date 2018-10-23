using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;

    public static Vector3 position;
    public static Vector3 vel;

    public float vida;
    public float Vel;
    public float RunVel;
    public float maxVel;
    public float maxRunVel;
    public float skill1CoolDown;
    public float skill2CoolDown;
    public float skill3CoolDown;

    private float skill1cdt;
    private float skill2cdt;
    private float skill3cdt;
    private Animator anim;
    private SpriteRenderer spr;
    private Sprite sp;
    private SkillUser skills;

    public delegate void SKILL(Vector2 pos, Vector2 dir, float tempo, float dano, bool pl);

    public static SKILL skill1;
    public static SKILL skill2;
    public static SKILL skill3;
    public static SKILL skill4;



    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        skills = GetComponent<SkillUser>();
        sp = spr.sprite;

        skill1 = skills.Heavy_Rain;
        skill3 = skills.Chain_Lightning;
        skill2 = skills.Meteor;

        MapaCyber mc = FindObjectOfType<MapaCyber>();
        if (mc != null) transform.position = MapaCyber.sala[0].transform.position + Vector3.back;
	}
	
	void Update () {
        position = transform.position;
        vel = rb.velocity;
        if (vida <= 0) enabled = false;



	}

    void FixedUpdate()
    {
        float x = 0, y = 0, v = rb.velocity.magnitude;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && v < maxRunVel) y += RunVel;
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && v < maxRunVel) y -= RunVel;
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && v < maxRunVel) x += RunVel;
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && v < maxRunVel) x -= RunVel;

        }
        else
        {
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && v < maxVel) y += Vel;
            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && v < maxVel) y -= Vel;
            if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && v < maxVel) x += Vel;
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && v < maxVel) x -= Vel;
        }
        rb.velocity += new Vector2(x, y);

        if (v != 0) anim.enabled = true;
        else
        {
            anim.enabled = false;
            spr.sprite = sp;
        }

        Vector3 a = (GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position);

        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(a.y, a.x) * Mathf.Rad2Deg - 90);

        //Skills
        Vector2 dir = (GetWorldPositionOnPlane(Input.mousePosition, 0) - transform.position).normalized;
        if (skill1 != null)
        {
            if (Input.GetMouseButton(0) && Time.time > skill1cdt + skill1CoolDown)
            {
                skill1cdt = Time.time;
                skill1((Vector2)transform.position, dir, 3, 10, true);
            }
        }

        if (skill2 != null)
        {
            if (Input.GetMouseButton(1) && Time.time > skill2cdt + skill2CoolDown)
            {
                skill2cdt = Time.time;
                skill2((Vector2)transform.position, dir, 15, 10, true);
            }
        }

        if (skill3 != null)
        {
            if (Input.GetMouseButton(2) && Time.time > skill3cdt + skill3CoolDown)
            {
                skill3cdt = Time.time;
                skill3((Vector2)transform.position, dir, 15, 10, true);
            }
        }
    }

    public void descongelar(float tempo)
    {
        Player.vel = Vector3.zero;
        Invoke("descongelar2", tempo);
    }

    public void descongelar2()
    {
        enabled = true;
        foreach (Transform t in transform)
        {
            if (t.tag == "Gelo")
            {
                Destroy(t.gameObject);
            }
        }
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
