using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanagerscript : MonoBehaviour {
    public static AudioClip walksound, iceblocksound, chainsound, oilsound, meteorsound, rainsound,pausesound;
    static AudioSource audioSrc;

    
    // Use this for initialization
	void Start () {
        walksound = Resources.Load<AudioClip>("walk");
        iceblocksound = Resources.Load<AudioClip>("ice");
        chainsound = Resources.Load<AudioClip>("chain");
        rainsound = Resources.Load<AudioClip>("rain");
         oilsound = Resources.Load<AudioClip>("oil");
        meteorsound = Resources.Load<AudioClip>("meteor");
        pausesound = Resources.Load<AudioClip>("pause");

        audioSrc = GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update() {
    }
        public static void PlaySound (string clip)
        {
            switch (clip)
            {
                case "walk":
                    audioSrc.PlayOneShot(walksound);
                    break;
                case "ice":
                    audioSrc.PlayOneShot(iceblocksound);
                    break;
                case "chain":
                    audioSrc.PlayOneShot(chainsound);
                    break;
                case "rain":
                    audioSrc.PlayOneShot(rainsound);
                    break;
                case "oil":
                    audioSrc.PlayOneShot(oilsound);
                    break;
                case "meteor":
                    audioSrc.PlayOneShot(meteorsound);
                    break;
            case "pause":
                audioSrc.PlayOneShot(pausesound);
                break;
        }
        }
		
	}

