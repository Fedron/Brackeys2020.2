using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    [SerializeField] AnimationCurve difficultyCurve = default;
    [SerializeField] float maxDifficultyMultiplier = 3f;
    [SerializeField] int floorMaxDifficulty = 10;

    [Header("UI, temporary")]
    [SerializeField] TextMeshProUGUI floorText = default; // Eventually move to a UI Manager

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
        GoToNextFloor(0);

        FindObjectOfType<RewinderManager>().changeDimention += GoToNextDimension;
    }

    public void GoToNextDimension() {
        GoToNextFloor(1);
    }

    public void GoToNextFloor(int preset = -1) {
        Floor++;
        floorText.text = string.Concat("Floor: ", Floor.ToString());

        float perc = (float)floor / floorMaxDifficulty;
        float baseMultiplier = difficultyCurve.Evaluate(perc);
        difficultyMultiplier = (baseMultiplier * maxDifficultyMultiplier) + 1f;

        dungeonManager.GenerateDungeon(preset);
    }
}
