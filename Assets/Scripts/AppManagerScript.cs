using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleSheetsToUnity;
using System.Collections;

public static class AppManager
{
    public static int DECK_SIZE = 13;
    public static int CARD_IN_HAND = 5;

    public static void startGame()
    {
        SceneManager.LoadScene("game");
    }

    public static void closeApp()
    {
        Application.Quit();
    }

    public static void toMenu()
    {
        MainScript.game = null;
        SceneManager.LoadScene("start");
    }

    public static void restartGame(MonoBehaviour instance)
    {
        MainScript.StartGame(instance);
    }
}

public class AppManagerScript : MonoBehaviour
{
    private void Awake()
    {
        //CardManager.Init(onLoadSheets);
        AppManager.toMenu();
    }

    private void onLoadSheets(GstuSpreadSheet ss)
    {
        //CardManager.parseSpreadSheet(ss);
        AppManager.toMenu();
    }
}
