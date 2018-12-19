using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour {

    Image image;
    int n = 0;

    // Use this for initialization
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    
    public void SetSkillData(Sprite sprite, int num)
    {
        image.sprite = sprite;
        n = num;
    }

    public void SetFill(float fill)
    {
        Debug.Log(fill);
        image.fillAmount = fill;
    }
	
}
