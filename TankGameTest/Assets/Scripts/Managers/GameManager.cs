using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    HUDManager _HUDmanager;

    [SerializeField]
    PlayerController _player;

    [SerializeField]
    NPCController _npc;

    [SerializeField]
    Transform _playerSpawnPoint;

    [SerializeField]
    Transform _npcSpawnPoint;

    [SerializeField]
    Timer _timer;

    [SerializeField]
    Logger _logger;

    void Start() {
        InitializePlayers();
    }

    public void StartGame()
    {
        Log("Game Is Starting!");
        _timer.StartTimer();
        _player.gameStarted = true;
        _npc.gameStarted = true;
    }

    private void InitializePlayers() {
        Log("InitalizingPlayers");
        _player.gameManager = this;
        _player.transform.position = _playerSpawnPoint.position;

        _npc.gameManager = this;
        _npc.spawnPoint = _npcSpawnPoint;
        _npc.transform.position = _npcSpawnPoint.position;
    }

    public void PlayerFiredShot() {
        Log("Player Fired a Shot now is NPCs turn");
        _npc.canFire = true;
        _HUDmanager.IsEnemiesTurn();
    }

    public void NPCFiredShot () {
        Log("NPC Fired a Shot now is Players turn");
        _player.canFire = true;
        _HUDmanager.IsPlayersTurn();
    }


    public void FinishMovingTime() {
        _player.canMove = false;
        if(_player.isMoving) {
            _player.Break();
            _player.navMeshAgent.isStopped = true;
        }
        _player.canFire = true;
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

    void Log(object message) {
        if(_logger)
            _logger.Log(message, this);
    }
}
