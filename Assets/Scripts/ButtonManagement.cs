using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour {

    GameParams myGameParams;

    void Awake()
    {
        myGameParams = (GameParams)GameObject.FindObjectOfType(typeof(GameParams));
    }

    public void ButtonOnePlayer()
    {
        SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_CLICK);
        myGameParams.SetNumberOfPlayers(1);
        SceneManager.LoadScene("Game");
    }
    public void ButtonTwoPlayers()
    {
        SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_CLICK);
        myGameParams.SetNumberOfPlayers(2);
        SceneManager.LoadScene("Game");
    }
    public void ButtonRules()
    {
        SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_CLICK);
        SceneManager.LoadScene("Rules");
    }
    public void ButtonQuit()
    {
        SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_CLICK);
        Application.Quit();
    }
    public void ButtonBack()
    {
        SoundManager.gameSoundManager.PlaySound(SoundManager.SOUND_CLICK);
        SceneManager.LoadScene("Menu");
    }
}
