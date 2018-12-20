using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour {

    Image image;
    public bool s1;
    static SkillSlot k1, k2;

    // Use this for initialization
    private void Awake()
    {
        image = GetComponent<Image>();

        if (s1) k1 = this;
        else k2 = this;
    }
    
    public static void SetSkillData(Sprite sprite, bool su)
    {
        if(su) k1.image.sprite = sprite;
        else k2.image.sprite = sprite;
    }

    void Update()
    {
        if (s1) image.fillAmount = 1f - ((Player.Insta.skill1cdt + Player.Insta.skill1CoolDown - Time.time) / Player.Insta.skill1CoolDown);
        else image.fillAmount = 1f - ((Player.Insta.skill2cdt + Player.Insta.skill2CoolDown - Time.time) / Player.Insta.skill2CoolDown);
    }
	
}
