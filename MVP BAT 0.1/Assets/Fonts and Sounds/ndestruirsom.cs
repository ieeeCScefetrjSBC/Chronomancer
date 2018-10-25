using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ndestruirsom : MonoBehaviour {
    
 


        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
