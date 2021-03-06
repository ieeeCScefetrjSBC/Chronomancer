﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selecttheinput : MonoBehaviour {
    public GameObject selectedObject;
    public EventSystem eventSystem;

    private bool buttonSelected;

    public GameObject cy, vi;
    public bool ddol;

    // Use this for initialization
    void Start () {
        if (cy != null && vi != null)
        {
            if (Random.Range(0, 2) == 1)
            {
                vi.SetActive(false);
                if (ddol) DontDestroyOnLoad(cy);
            }
            else
            {
                cy.SetActive(false);
                if (ddol) DontDestroyOnLoad(vi);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;

        }
	}
    private void OnDisable()
    {
        buttonSelected = false;
    }
}
