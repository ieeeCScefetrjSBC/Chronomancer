﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour {

    public readonly string[] skills = { "Meteor", "Ice_Block", "Heavy_Rain", "Chain_Lightning", "Crippling_Oil" };
    private static GameObject GeloG;
    private static GameObject OleoG;
    private static GameObject AreaIceBlockG;
    private static GameObject AreaRainG;
    private static GameObject AreaMeteorG;

    private static GameObject RainEffectG;
    private static GameObject ChainlG;

    [SerializeField]
    private GameObject Gelo;
    [SerializeField]
    private GameObject Oleo;
    [SerializeField]
    private GameObject AreaIceBlock;
    [SerializeField]
    private GameObject AreaRain;
    [SerializeField]
    private GameObject AreaMeteor;
    [SerializeField]
    private GameObject RainEffect;
    [SerializeField]
    private GameObject Chainl;

    public static Player.SKILL Ice_Block_del;
    public static Player.SKILL Chain_Lightning_del;
    public static Player.SKILL Crippling_Oil_del;
    public static Player.SKILL Heavy_Rain_del;
    public static Player.SKILL Meteor_del;

    public static void Lord() {
        var p = Player.Insta.GetComponent<SkillUser>();
        GeloG = p.Gelo;
        OleoG = p.Oleo;
        AreaIceBlockG = p.AreaIceBlock;
        AreaRainG = p.AreaRain;
        AreaMeteorG = p.AreaMeteor;
        RainEffectG = p.RainEffect;
    }

    void Awake() {
        if (Gelo != null && GeloG == null) GeloG = Gelo;
        if (Oleo != null && OleoG == null) OleoG = Oleo;
        if (AreaIceBlock != null && AreaIceBlockG == null) AreaIceBlockG = AreaIceBlock;
        if (AreaRain != null && AreaRainG == null) AreaRainG = AreaRain;
        if (AreaMeteor != null && AreaMeteorG == null) AreaMeteorG = AreaMeteor;
        if (RainEffect != null && RainEffectG == null) RainEffectG = RainEffect;
        if (Chainl != null && ChainlG == null) ChainlG = Chainl;
    }

    void Start(){
        if (Gelo == null) Gelo = GeloG;
        if (Oleo == null) Oleo = OleoG;
        if (AreaIceBlock == null) AreaIceBlock = AreaIceBlockG;

        if (AreaRain == null) AreaRain = AreaRainG;
        if (AreaMeteor == null) AreaMeteor = AreaMeteorG;
        if (RainEffect == null) RainEffect = RainEffectG;
        if (Chainl == null) Chainl = ChainlG;

        if (Ice_Block_del == null && GetComponent<Player>() != null){
            Ice_Block_del = Ice_Block;
            Chain_Lightning_del = Chain_Lightning;
            Crippling_Oil_del = Crippling_Oil;
            Heavy_Rain_del = Heavy_Rain;
            Meteor_del = Meteor;
            
        }
    }
	
	void Update () {
		
	}

    public void Ice_Block_Raycast(Vector2 pos, Vector2 dir,float tempo, float dano, bool pl) {
        RaycastHit2D r = Physics2D.Raycast(pos, dir);
        if (r.transform != null){
            Quimica qui = r.transform.GetComponent<Quimica>();
            if (qui != null)  qui.humidade = 1;

            Inimigo i = r.transform.gameObject.GetComponent<Inimigo>();
            if (i != null){
                Rigidbody2D b = r.transform.gameObject.GetComponent<Rigidbody2D>();
                b.constraints = RigidbodyConstraints2D.FreezeAll;
                b.velocity = Vector2.zero;
                b.angularVelocity = 0;
                Instantiate(GeloG, r.transform);
                i.descongelar(tempo);


                i.vida -= dano;//Causar Dano
            }
        }
    }

    public void Ice_Block(Vector2 pos, Vector2 dir, float tempo, float dano, bool pl)
    {

        GameObject g = Instantiate(AreaIceBlockG, transform.position + 15 * ((Vector3) dir), Quaternion.identity);
        IceBlockRegion r = g.GetComponent<IceBlockRegion>();
        r.Gelo = GeloG;
        r.tempo = tempo;
        r.dano = dano;
        r.pl = pl;
        r.Invoke("Finish", tempo);
        Audiomanagerscript.PlaySound("ice");
    }

    public void Chain_Lightning(Vector2 pos, Vector2 dir, float tempo, float dano, bool pl)
    {
        RaycastHit2D r = Physics2D.Raycast(pos + 5*dir, dir);
        
        if (r.transform != null)
        {
            Quimica qui = r.transform.GetComponent<Quimica>();
            if (qui != null){
                qui.fonteTensao = true;
                qui.deseltrizar(tempo);
            }
            Inimigo i = r.transform.gameObject.GetComponent<Inimigo>();
            if (i != null)
            {
                i.vida -= dano;
                
            }
            Audiomanagerscript.PlaySound("chain");
        }

        var cl = Instantiate(Chainl, transform.position,Quaternion.Euler(Mathf.Rad2Deg * -Mathf.Atan2(dir.y, dir.x), 90,0));
        Destroy(cl, tempo);
    }

    public void Crippling_Oil(Vector2 pos, Vector2 dir, float tempo, float dano, bool pl)
    {
        
        Instantiate(OleoG, transform.position + 15*((Vector3)dir) + Vector3.back*2, Quaternion.identity);
        Audiomanagerscript.PlaySound("oil");
    }

    public void Meteor(Vector2 pos, Vector2 dir, float tempo, float dano, bool pl)
    {
        GameObject g = Instantiate(AreaMeteorG, transform.position + 15 * ((Vector3)dir) + Vector3.back * 2, Quaternion.identity);
        MeteorRegion r = g.GetComponent<MeteorRegion>();
        r.dano = dano;
        r.pl = pl;
        r.Invoke("Finish", tempo);
        Audiomanagerscript.PlaySound("meteor");
    }

    public void Heavy_Rain(Vector2 pos, Vector2 dir, float tempo, float dano, bool pl)
    {
       
        GameObject g = Instantiate(AreaRainG, transform.position + 15 * ((Vector3)dir), Quaternion.identity);
        RainRegion r = g.GetComponent<RainRegion>();
        r.dano = dano;
        r.pl = pl;
        r.Invoke("Finish", tempo);
        Audiomanagerscript.PlaySound("rain");
    }

}
