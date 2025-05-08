using UnityEditor;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // UI에 관해서 시간 스케일을 사용할 때는 Time.unscaledDeltaTime을 사용해야 한다.
    // Time.deltaTime은 시간 스케일에 영향을 받기 때문에 게임 속도가 변경되면 UI나 이펙트의 재생 속도가 달라진다.
    public EGameState GameState { get; private set; } = EGameState.Game;

    public void Pause()
    {
        if (GameState != EGameState.Pause)
        {
            Debug.Log("Open Option Popup");
            GameState = EGameState.Pause;
            Time.timeScale = 0f;
            PopupManager.Instance.Open(EPopupName.Option, closeCallBack: Continue);
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        GameState = EGameState.Game;
    }

    public void Restart()
    {
        GameState = EGameState.Ready;
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
