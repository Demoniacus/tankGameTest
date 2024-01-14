using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField]
    CanvasGroup mainMenuCanvas;
    public void OnClickPlayGame() {
        LeanTween.alphaCanvas(mainMenuCanvas,0,0.8f).setEase(LeanTweenType.easeInQuad).setOnComplete(DisableMainMenuCanvas);
    }

    public void DisableMainMenuCanvas() {     
        SceneManager.LoadScene("TankBattleScene");
    }

    public void OnClickExitGame() {
        Application.Quit();
    }

}