using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Params
    public static UIController      S_UIController;
    private Transform               _StartMenu;
    private Transform               _EndGame;
    #endregion

    #region Initialization
    private void Awake()
    {
        if (S_UIController != null)
        { 
            print("UIController: Instance already set. It should be Singleton"); 
            return; 
        }

        S_UIController = this;

        _StartMenu = transform.Find("StartMenu");
        _EndGame = transform.Find("EndGame");
    }
    #endregion

    #region Main Logic
    public void SetUIFrame(GameState typeMenu)
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (typeMenu == GameState.START)
        {
            _StartMenu.gameObject.SetActive(true);
        } else if (typeMenu == GameState.END)
        {
            _EndGame.gameObject.SetActive(true);
        } else if (typeMenu == GameState.GAME)
        {
            //... Empty because not have any logic in game case in UI, but need for trigger
        }
        else
        {
            print(typeMenu + "can not set");
        }
    }
    #endregion
}
