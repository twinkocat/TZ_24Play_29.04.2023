using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    START,
    GAME,
    END
}

public class Main : MonoBehaviour
{
    #region Params
    public static Main              S_Main; // singleton
    public GameState                gameState;
    #endregion


    #region Initialization
    private void Awake()
    {
        print("WARNING: Gravity scale modify by 3!");
        if (S_Main != null)
        { 
            print("Main: Instance already set. It should be Singleton"); 
            return; 
        }
        S_Main = this;
    }

    private void Start()
    {
        InitNewGame();
    }
    #endregion


    #region Main Logic
    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && gameState == GameState.START)
        {
            Touch touch = Input.GetTouch(0);
            float touchDeltaX = touch.deltaPosition.x;
            if (touchDeltaX != 0)
            {
                PlayGame();
            }
        }
    }

    private void InitNewGame()
    {
        gameState = GameState.START;
        UIController.S_UIController.SetUIFrame(gameState);
        TrackGroundLogic.S_TrackGrLogic.InitializeLevel();
    }

    private void PlayGame()
    {
        gameState = GameState.GAME;
        UIController.S_UIController.SetUIFrame(gameState);
    }

    public void EndGame()
    {
        gameState = GameState.END;
        UIController.S_UIController.SetUIFrame(gameState);
    }


    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
