using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDieMechanics : MonoBehaviour
{
    public GameObject DieVFXPref;
    [SerializeField] AudioClip sound = default;
    private CreaturesHealth pHealth;
    private void Awake()
    {
        pHealth = GetComponent<CreaturesHealth>();
        pHealth.OnDie += Die;
    }

    void Die()
    {
        Instantiate(DieVFXPref, transform.position, Quaternion.identity);
        SoundManager.Instance.Play(sound, true);
        Destroy(gameObject);
    }

}

