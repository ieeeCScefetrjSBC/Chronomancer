using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControleVolume : MonoBehaviour {
    public float volumeMaster, volumeFX, volumeMusica;
    public Slider slidermaster, sliderfx, slidermusicas;
	// Use this for initialization
	void Start () {
    
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public void VolumeMaster(float volume)
    {
        volumeMaster = volume;
        AudioListener.volume = volumeMaster;
        
    }
    public void VolumeMusica(float volume)
    {
        volumeMusica = volume;

        GameObject[] Musica = GameObject.FindGameObjectsWithTag("musicas");
            for(int i = 0; i<Musica.Length; i++)
        {
            Musica[i].GetComponent<AudioSource>().volume = volumeMusica;
        }
        
    }
    public void VolumeFX(float volume)
    {
        volumeFX = volume;
        GameObject[] Fxs = GameObject.FindGameObjectsWithTag("fx");
            for (int i = 0 ; i<Fxs.Length; i++)
        {
            Fxs[i].GetComponent<AudioSource>().volume = volumeFX;
        }
       
    }
}
