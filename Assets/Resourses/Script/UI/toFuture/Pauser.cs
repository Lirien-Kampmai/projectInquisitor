using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    /// <summary>
    /// Добей паузер, он в сделку не входил. Ну или оставь, если поток будет асинхронным.
    /// </summary>
    public event UnityAction<bool> PauseStateChange;

    private bool isPause;
    public bool  IsPause => isPause;

    private void Awake() { SceneManager.sceneLoaded += SceneManager_sceneLoaded; }
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) { UnPause(); }

    public void ChangePauseState()
    {
        if (isPause == true)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (isPause == true) return;

        Time.timeScale = 0.0f;
        isPause = true;
        PauseStateChange?.Invoke(isPause);
    }

    public void UnPause()
    {
        if (isPause == false) return;

        Time.timeScale = 1.0f;
        isPause = false;
        PauseStateChange?.Invoke(isPause);
    }
}