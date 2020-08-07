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
        if (amountOfDeaths < reqAmountToChangeDifficulty) {
            amountOfDeaths++;
            //todo remove debug log
            Debug.Log("Player did rewind successfully and counter of deaths now: " + amountOfDeaths);
            normalRevive?.Invoke();
        } else if (amountOfDeaths == reqAmountToChangeDifficulty) {
            amountOfDeaths++;
            changeDimention?.Invoke();
        } else {
            gameOver?.Invoke();
        }
    }
}
