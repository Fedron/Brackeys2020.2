using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewinderManager : MonoBehaviour
{
    private IHaveHealth playerHealth;
    public event Action normalRevive = delegate { };
    public event Action changeDimention = delegate { };
    public event Action gameOver = delegate { };
    public int reqAmountToChangeDifficulty;
    int amountOfDeaths =  0;
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerInputHandler>().GetComponent<IHaveHealth>();
    }

    public void DeathRewind() {
        amountOfDeaths++;
        if (amountOfDeaths == 1) {
            normalRevive?.Invoke();
        } else if (amountOfDeaths == 3) {
            changeDimention?.Invoke();
        } else if (amountOfDeaths == 4) {
            gameOver?.Invoke();
        }
    }
}
