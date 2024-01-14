using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;

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

    private bool isPlayersTurn;

    private bool arePlayersInMovingPhase;

    private bool gameStarted;

    void Start() {
        InitializePlayers();
    }

    public void StartGame()
    {
        timer.StartTimer();
        gameStarted = true;
        arePlayersInMovingPhase = true;
        player.gameStarted = true;
        npc.gameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If they aren't in moving round 
        if(!arePlayersInMovingPhase && gameStarted) {
            if(isPlayersTurn) {
                if(!player.canFire) {
                    player.canFire = true;
                }
            } else {
                npc.canFire = true;
            }
        }
    }

    private void InitializePlayers() {
        player.gameManager = this;
        player.audioManager = audioManager;
        player.transform.position = playerSpawnPoint.position;

        npc.gameManager = this;
        npc.audioManager = audioManager;
        npc.spawnPoint = npcSpawnPoint;
        npc.transform.position = npcSpawnPoint.position;
    }

    public void PlayerFiredShot() {
        isPlayersTurn = false;
    }

    public void NPCFiredShot () {
        isPlayersTurn = false;
    }


    public void FinishMovingTime() {
        player.canMove = false;
        if(player.isMoving) {
            player.isMoving = false;
            audioManager.PlayBreakingSound();
            player.navMeshAgent.isStopped = true;
        }
        arePlayersInMovingPhase = false;
        isPlayersTurn = true;

    }

    public void PlayerIsDead() {
        _HUDmanager.PlayerIsDead();
    }

    public void PlayerWon() {        
        _HUDmanager.PlayerWon();
    }

    public void RestartGame() {
        print("AAS");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
