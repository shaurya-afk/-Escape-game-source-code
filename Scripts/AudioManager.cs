using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip z_roar, flashLightSwitch, keys, humanHurt, zGrowl, runn;
    static AudioSource audSrc;
    // Start is called before the first frame update
    void Start()
    {
        z_roar = Resources.Load<AudioClip>("ZRoar");
        runn = Resources.Load<AudioClip>("Running");
        zGrowl = Resources.Load<AudioClip>("ZGrowl");
        flashLightSwitch = Resources.Load<AudioClip>("FlashlightSwitch");
        keys = Resources.Load<AudioClip>("Keys");
        humanHurt = Resources.Load<AudioClip>("HumanHurt");

        audSrc=GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayAudio(string clip)
    {
        switch (clip)
        {
            case "roar":
                audSrc.PlayOneShot(z_roar); 
                break;
            case "growl":
                audSrc.PlayOneShot(zGrowl);
                break;
            case "run":
                audSrc.PlayOneShot(runn);
                break;
            case "light":
                audSrc.PlayOneShot(flashLightSwitch); 
                break;
            case "key":
                audSrc.PlayOneShot(keys);
                break;
            case "pain":
                audSrc.PlayOneShot(humanHurt);
                break;
            default:
                break;
        }
    }
}
