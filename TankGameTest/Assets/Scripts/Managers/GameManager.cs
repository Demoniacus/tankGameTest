using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    HUDManager _HUDmanager;

    [SerializeField]
    PlayerController player;

    [SerializeField]
    NPCController npc;

    [SerializeField]
    Transform playerSpawnPoint;

    [SerializeField]
    Transform npcSpawnPoint;

    [SerializeField]
    Timer timer;

    void Start() {
        InitializePlayers();
    }

    public void StartGame()
    {
        timer.StartTimer();
        player.gameStarted = true;
        npc.gameStarted = true;
    }

    private void InitializePlayers() {
        player.gameManager = this;
        player.transform.position = playerSpawnPoint.position;

        npc.gameManager = this;
        npc.spawnPoint = npcSpawnPoint;
        npc.transform.position = npcSpawnPoint.position;
    }

    public void PlayerFiredShot() {
        npc.canFire = true;
        _HUDmanager.IsEnemiesTurn();
    }

    public void NPCFiredShot () {
        player.canFire = true;
        _HUDmanager.IsPlayersTurn();
    }


    public void FinishMovingTime() {
        player.canMove = false;
        if(player.isMoving) {
            player.Break();
            player.navMeshAgent.isStopped = true;
        }
        player.canFire = true;
        _HUDmanager.FadeInPlayersTurnCanvas();

    }

    public void PlayerIsDead() {
        _HUDmanager.PlayerIsDead();
    }

    public void PlayerWon() {        
        _HUDmanager.PlayerWon();
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
