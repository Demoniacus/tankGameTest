using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    public float reloadTime;

    public Transform towerTransform;

    public AudioManager audioManager;

    private NavMeshAgent navMeshAgent;

    private bool canMove;

    private bool isPlayersTurn;

    private float reloadTimeTimer;

    private bool isMoving;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        isPlayersTurn = true;
        Cursor.visible = true;
        navMeshAgent = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayersTurn){
            //Player Initial Movement Logic
            if(canMove) {
                if(isMoving) {
                    if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 1.6f) {
                        print("breakiiing");
                        audioManager.playBreakingSound();
                        isMoving = false;
                    }

                }
                //Check if player has pressed left click to move
                if(Input.GetMouseButtonDown(0)) { 
                    audioManager.playMovingSound();        
                    StartCoroutine(movePlayer());
                }

            }
            int isMovingLeft;
            int isMovingUp;
            //Checking if the player is rotating
            if(IsRotatingTank(out isMovingLeft)) {
                transform.Rotate(Vector3.up * isMovingLeft * 0.15f);
            } 
            //
            else if (IsElevatingTower(out isMovingUp)) {
                print(towerTransform.rotation.x);
                if(towerTransform.rotation.x >= 0 && towerTransform.rotation.x <= 45) {
                    towerTransform.Rotate(Vector3.right * isMovingUp * 0.15f);
                }
            }           
        }
        
    }

    //This method moves the tank to the place clicked by the player
    IEnumerator movePlayer() {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if(Physics.Raycast(cameraRay, out hit, 150, LayerMask.GetMask("Terrain"))) {
                        //Stop the tank completlely to give a feel of new command recieved
                        navMeshAgent.velocity = Vector3.zero; 

                        //Little delay of 0.3f just for game feel
                        yield return new WaitForSeconds(0.3f);     

                        //Moving the tank to the new destination
                        navMeshAgent.SetDestination(hit.point);                            
                        isMoving = true;
                    }
    }

    //Return true if the player is Rotating the tankwith the keys A or D, returns in as out the direction as well
    private bool IsRotatingTank(out int rotatingLeft) {
        //If player is pressing the D key he is rotating the tank to the Right
        if(Input.GetKey(KeyCode.D)) {
            rotatingLeft = 1;
            return true;
        }        
        //If player is pressing the A key he is rotating the tank to the Left
        if(Input.GetKey(KeyCode.A)) {
            rotatingLeft = -1;
            return true;
        }

        //The player is not rotating the tank
        rotatingLeft = 0;
        return false;
    }

    //Return true if the player is Elevating the tank tower with the keys A or D, returns in as out the direction as well
    private bool IsElevatingTower(out int isMovingUp) {
        //If player is pressing the W key he is elevating the tank tower to the Up
        if(Input.GetKey(KeyCode.W)) {
            isMovingUp = 1;
            return true;
        }        
        //If player is pressing the S key he is elevating the tank tower to the Down
        if(Input.GetKey(KeyCode.S)) {
            isMovingUp = -1;
            return true;
        }

        //The player is not elevating the tank tower
        isMovingUp = 0;
        return false;
    }

}
