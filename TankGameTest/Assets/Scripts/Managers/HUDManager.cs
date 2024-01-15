using UnityEngine;

public class HUDManager : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    CanvasGroup victoryCanvas, gameOverCanvas, tutorialCanvas, gameCanvas, playersTurn, enemiesTurn;

    void Start() {
        LeanTween.init(1200);
    }

    public void OnClickStartRound() {
        gameManager.StartGame();
        FadeOutTutorialCanvas();
    }

    public void OnClickExitGame() {
        Application.Quit();
    }

    public void PlayerIsDead() {
        gameOverCanvas.interactable = true;
        victoryCanvas.blocksRaycasts = true;
        LeanTween.alphaCanvas(gameOverCanvas,1,0.8f).setEase(LeanTweenType.easeInQuad);
    }

    public void PlayerWon() {        
        victoryCanvas.interactable = true;
        victoryCanvas.blocksRaycasts = true;
        LeanTween.alphaCanvas(victoryCanvas,1,0.8f).setEase(LeanTweenType.easeInQuad);
    }

    private void FadeOutTutorialCanvas() {        
        tutorialCanvas.interactable = false;
        tutorialCanvas.blocksRaycasts = false;
        LeanTween.alphaCanvas(tutorialCanvas,0,0.8f).setEase(LeanTweenType.easeInQuad).setOnComplete(FadeInGameCanvas);

    }

    public void FadeInGameCanvas() {        
        LeanTween.alphaCanvas(gameCanvas,1,0.8f).setEase(LeanTweenType.easeInQuad);
    }

    public void IsPlayersTurn() {
        LeanTween.alphaCanvas(enemiesTurn,0,0.6f).setEase(LeanTweenType.easeInQuad).setOnComplete(FadeInPlayersTurnCanvas);
    }

    public void IsEnemiesTurn(){
        LeanTween.alphaCanvas(playersTurn,0,0.6f).setEase(LeanTweenType.easeInQuad).setOnComplete(FadeInEnemiessTurnCanvas);
    }

    public void FadeInPlayersTurnCanvas() {        
        LeanTween.alphaCanvas(playersTurn,1,0.8f).setEase(LeanTweenType.easeInQuad);
    }

    public void FadeInEnemiessTurnCanvas() {        
        LeanTween.alphaCanvas(enemiesTurn,1,0.8f).setEase(LeanTweenType.easeInQuad);
    }
}
