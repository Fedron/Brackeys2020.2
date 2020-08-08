using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    [HideInInspector] public bool paused = false;
    [HideInInspector] public bool gameOver = false;

    [SerializeField] AnimationCurve difficultyCurve = default;
    [SerializeField] float maxDifficultyMultiplier = 3f;
    [SerializeField] int floorMaxDifficulty = 10;

    [Header("SFX")]
    [SerializeField] AudioClip dungeonTheme = default;
    [SerializeField] AudioClip corruptedTheme = default;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI floorText = default;
    [SerializeField] TextMeshProUGUI gameOverFloorText = default;
    [SerializeField] Image healthbar = default;
    [SerializeField] GameObject gameUI = default;
    [SerializeField] GameObject gameOverUI = default;
    [SerializeField] GameObject pauseScreenUI = default;

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

        RewinderManager rm = FindObjectOfType<RewinderManager>();
        rm.changeDimention += GoToNextDimension;
        rm.gameOver += GameOver;

        AudioManager.Instance.PlayMusic(dungeonTheme);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    private void TogglePause() {
        if (gameOver) return;

        paused = !paused;
        Time.timeScale = paused ? 0f : 1f;
        pauseScreenUI.SetActive(paused);
    }

    private void GameOver() {
        gameOver = true;
        Time.timeScale = 0f;
        gameOverFloorText.text = string.Concat("You reached floor ", floorText.text.Split(null)[1]);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void UpdateHealthbar(float cur, float max) {
        healthbar.fillAmount = cur / max;
    }

    public void GoToNextDimension() {
        GoToNextFloor(1);
        AudioManager.Instance.PlayMusic(corruptedTheme);
    }

    public void GoToNextFloor(int preset = -1) {
        Floor++;
        floorText.text = string.Concat("Floor: ", Floor.ToString());

        float perc = (float)floor / floorMaxDifficulty;
        float baseMultiplier = difficultyCurve.Evaluate(perc);
        difficultyMultiplier = (baseMultiplier * maxDifficultyMultiplier) + 1f;

        if (Floor > 1) AudioManager.Instance.PlaySound2D("Stairs");
        dungeonManager.GenerateDungeon(preset);
    }
}
