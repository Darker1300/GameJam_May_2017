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
    public AudioClip levelIntro;
    [Range(0.0f, 1.0f)]
    public float levelIntroVol;

    public AudioClip playerVacuumSound;
    [Range(0.0f, 1.0f)]
    public float playerVacuumVol;
    // item hit player
    public AudioClip playerAttackSound;
    [Range(0.0f, 1.0f)]
    public float playerAttackVol;
    // item grabbed
    public AudioClip playerDefaultDeathSound;
    [Range(0.0f, 1.0f)]
    public float playerDefaultDeathVol;
    //item positive
    public AudioClip playerAlternateDeathSound1;
    [Range(0.0f, 1.0f)]
    public float playerAlternateDeath1Vol;
    //item negative
    public AudioClip playerAlternateDeathSound2;
    [Range(0.0f, 1.0f)]
    public float playerAlternateDeath2Vol;
    // score positive
    public AudioClip arrowHitSound;
    [Range(0.0f, 1.0f)]
    public float arrowHitVol;
    //player collision
    public AudioClip treeHitSound;
    [Range(0.0f, 1.0f)]
    public float treeHitVol;
    // menu select
    public AudioClip buildEffectSound;
    [Range(0.0f, 1.0f)]
    public float buildEffectVol;
    // menu scroll
    public AudioClip enemyhitSound;
    [Range(0.0f, 1.0f)]
    public float enemyhitVol;
    // hovering
    public AudioClip enemyAttackingSound;
    [Range(0.0f, 1.0f)]
    public float enemyAttackingVol;

    public AudioClip enemyDeathSound;
    [Range(0.0f, 1.0f)]
    public float enemyDeathVol;

    public AudioClip fireShotSound;
    [Range(0.0f, 1.0f)]
    public float fireShotVol;

    public AudioClip iceShotSound;
    [Range(0.0f, 1.0f)]
    public float iceShotVol;

    public AudioClip lightningShotSound;
    [Range(0.0f, 1.0f)]
    public float lightningShotVol;

    public AudioClip gasEmitterSound;
    [Range(0.0f, 1.0f)]
    public float gasEmitterVol;

    public AudioClip scrapCollectSound;
    [Range(0.0f, 1.0f)]
    public float scrapCollectVol;

    public AudioClip cantAffordTurretSound;
    [Range(0.0f, 1.0f)]
    public float cantAffordTurretVol;

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

    public void playPlayerWalkSound()
    {
        GetComponent<AudioSource>().PlayOneShot(levelIntro, levelIntroVol);
    }

    public void playPlayerDamagedSound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerVacuumSound, playerVacuumVol);
    }

    public void playPlayererAttackSound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerAttackSound, playerAttackVol);
    }

    public void playPlayerDefaultDeathSound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerDefaultDeathSound, playerDefaultDeathVol);
    }

    public void playPlayerAlternateDeath1Sound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerAlternateDeathSound1, playerAlternateDeath1Vol);
    }

    public void playPlayerAlternateDeath2Sound()
    {
        GetComponent<AudioSource>().PlayOneShot(playerAlternateDeathSound2, playerAlternateDeath2Vol);
    }

    public void playArrowhitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(arrowHitSound, arrowHitVol);
    }

    public void playTreehitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(treeHitSound, treeHitVol);
    }

    public void playBuildEffectSound()
    {
        GetComponent<AudioSource>().PlayOneShot(buildEffectSound, buildEffectVol);
    }

    public void playEnemyHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(enemyhitSound, enemyhitVol);
    }

    public void playEnemyAttackingSound()
    {
        GetComponent<AudioSource>().PlayOneShot(enemyAttackingSound, enemyAttackingVol);
    }

    public void playEnemyDeathSound()
    {
        GetComponent<AudioSource>().PlayOneShot(enemyDeathSound, enemyDeathVol);
    }

    public void playFireTurretSound()
    {
        GetComponent<AudioSource>().PlayOneShot(fireShotSound, fireShotVol);
    }

    public void playIceTurretSound()
    {
        GetComponent<AudioSource>().PlayOneShot(iceShotSound, iceShotVol);
    }

    public void playLightningTurretSound()
    {
        GetComponent<AudioSource>().PlayOneShot(lightningShotSound, lightningShotVol);
    }

    public void playGasEmitterSound()
    {
        GetComponent<AudioSource>().PlayOneShot(gasEmitterSound, gasEmitterVol);
    }

    public void playScrapCollectSound()
    {
        GetComponent<AudioSource>().PlayOneShot(scrapCollectSound, scrapCollectVol);
    }

    public void playCantAffordTurretSound()
    {
        GetComponent<AudioSource>().PlayOneShot(cantAffordTurretSound, scrapCollectVol);
    }
}
