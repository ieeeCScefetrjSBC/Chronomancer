using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour {

    Image image;

    // Use this for initialization
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void SetSkillData(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetFill(float fill)
    {
        image.fillAmount = fill;
    }
	
}
