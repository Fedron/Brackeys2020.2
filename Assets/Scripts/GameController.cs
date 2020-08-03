using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    [SerializeField] AnimationCurve difficultyCurve = default;
    [SerializeField] float maxDifficultyMultiplier = 3f;
    [SerializeField] int floorMaxDifficulty = 10;

    [Space, SerializeField] int schrodingerFloorStart = 6;

    float difficultyMultiplier = 1f;
    public float Difficulty {
        get { return difficultyMultiplier; }
        private set { difficultyMultiplier = value; }
    }

    int floor = 0;
    public int Floor {
        get { return floor; }
        private set { floor = value; }
    }

    DungeonManager dungeonManager;

    private void Awake() {
        Instance = this;
        Difficulty = 1f;
        dungeonManager = FindObjectOfType<DungeonManager>();
        GoToNextFloor();
    }

    public void GoToNextFloor() {
        Floor++;

        float perc = (float)floor / floorMaxDifficulty;
        float baseMultiplier = difficultyCurve.Evaluate(perc);
        difficultyMultiplier = (baseMultiplier * maxDifficultyMultiplier) + 1f;

        dungeonManager.GenerateDungeon(floor >= schrodingerFloorStart ? 1 : 0);
    }
}
