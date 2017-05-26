using UnityEngine;
/////////////////////////////////////////////////////////////////////////////////////////
//
//	Name:		AudioManager
//	Author:		David Floyd
//	Date:		23-10-2016
//	
//	Brief:		Manages Sound effects individually , offers balancing of volume
//
/////////////////////////////////////////////////////////////////////////////////////////

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [Space]
    // level begin
    public AudioClip levelIntroSound;
    [Range(0.0f, 1.0f)]
    public float levelIntroVol;

    // level begin
    public AudioClip levelEndSound;
    [Range(0.0f, 1.0f)]
    public float levelEndVol;

    //player vacuum sound

    public AudioClip playerVacuumSound;
    [Range(0.0f, 1.0f)]
    public float playerVacuumVol;

    // item hit player
    public AudioClip itemHitSound;
    [Range(0.0f, 1.0f)]
    public float playerAttackVol;

    //itemPositive
    public AudioClip itemPositiveSound;
    [Range(0.0f, 1.0f)]
    public float itemPositiveVol;

    //itemNegative
    public AudioClip itemNegativeSound;
    [Range(0.0f, 1.0f)]
    public float itemNegativeVol;

    // scorePositive
    public AudioClip scorePositiveSound;
    [Range(0.0f, 1.0f)]
    public float scorePositiveVol;

    //playerCollision
    public AudioClip playerCollisionSound;
    [Range(0.0f, 1.0f)]
    public float playerCollisionVol;

    // menu select
    public AudioClip menuSelectSound;
    [Range(0.0f, 1.0f)]
    public float menuSelectVol;

    // menu scroll
    public AudioClip menuScrollSound;
    [Range(0.0f, 1.0f)]
    public float menuScrollVol;

    // hovering
    public AudioClip hoveringSound;
    [Range(0.0f, 1.0f)]
    public float hoveringVol;



    void Awake()
    {
        if (instance != null) // Check if singelton has already been instantiated
        {
            Debug.LogError("More than one AudioManager");
            return;
        }
        instance = this;
    }

    void Update()
    {

    }

    public void playLevelIntroSound()
    {
        GetComponent<AudioSource>().PlayOneShot(levelIntroSound, levelIntroVol);
    }

    public void playLevelEndSound()
    {
        GetComponent<AudioSource>().PlayOneShot(levelEndSound, levelEndVol);
    }

    public void playPlayerVacuumSound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerVacuumSound, playerVacuumVol);
    }

    public void playItemHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(itemHitSound, playerAttackVol);
    }

    public void playItemPositiveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(itemPositiveSound, itemPositiveVol);
    }

    public void playItemNegativeSound()
    {
        GetComponent<AudioSource>().PlayOneShot(itemNegativeSound, itemNegativeVol);
    }

    public void playScorePositiveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(scorePositiveSound, scorePositiveVol);
    }

    public void playPlayerCollisionSound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerCollisionSound, playerCollisionVol);
    }

    public void playMenuSelectSound()
    {
        GetComponent<AudioSource>().PlayOneShot(menuSelectSound, menuSelectVol);
    }

    public void playMenuScrollSound()
    {
        GetComponent<AudioSource>().PlayOneShot(menuScrollSound, menuScrollVol);
    }

    public void playHoveringSound()
    {
        GetComponent<AudioSource>().PlayOneShot(hoveringSound, hoveringVol);
    }


}
