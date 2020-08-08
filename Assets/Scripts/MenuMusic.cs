using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {
    [SerializeField] AudioClip menuMusic = default;
    void Start() {
        AudioManager.Instance.PlayMusic(menuMusic);
    }
}
