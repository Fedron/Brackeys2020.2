using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    [SerializeField] Animator animator = default;

    public void ChangeScene(int index) {
        StartCoroutine(LoadScene(index));
    }

    private IEnumerator LoadScene(int index) {
        animator.SetTrigger("Fade");
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }
}
