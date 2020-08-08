using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDieMechanics : MonoBehaviour
{
    [SerializeField] string deathSound = default;
    public GameObject DieVFXPref;
    private CreaturesHealth pHealth;
    private void Awake()
    {
        pHealth = GetComponent<CreaturesHealth>();
        pHealth.OnDie += Die;
    }

    void Die()
    {
        Instantiate(DieVFXPref, transform.position, Quaternion.identity);
        //SoundManager.Instance.Play(sound, true);
        AudioManager.Instance.PlaySound2D(deathSound);
        Destroy(gameObject);
    }

}

