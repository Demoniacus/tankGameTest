using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    float countDown;

    [SerializeField]
    GameManager gameManager;

    private float elapsedTime = 0f;

    private bool hasStarted = false;

    private bool hasFireTimeStarted = false;

    public void StartTimer() {
        hasStarted = true;
    }

    void Start() {
        int minutes = Mathf.FloorToInt(countDown / 60);        
        int seconds = Mathf.FloorToInt(countDown % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStarted) {
            if(countDown > 0) {
                countDown -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(countDown / 60);        
                int seconds = Mathf.FloorToInt(countDown % 60);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            } else if(hasFireTimeStarted) {
                elapsedTime += Time.deltaTime;

                int minutes = Mathf.FloorToInt(elapsedTime / 60);        
                int seconds = Mathf.FloorToInt(elapsedTime % 60);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            } else {
                gameManager.FinishMovingTime();
                hasFireTimeStarted = true;
            }
        }        
    }
}
