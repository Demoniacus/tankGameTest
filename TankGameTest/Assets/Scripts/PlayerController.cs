using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    float reloadTime;
    
    public Transform towerTransform;

    public GameManager gameManager {get; set;}
    
    public bool canFire  {get; set;}

    public bool canMove  {get; set;}

    public bool isMoving  {get; set;}

    public bool gameStarted  {get; set;}

    public AudioManager audioManager {get; set;}

    public NavMeshAgent navMeshAgent  {get; private set;}

    private bool isPlayersTurn;

    public Image projectileThrustBar;

    private float projectileThrust, minProjectileThrust, maxProjectileThrust;


    private ProyectileSpawner proyectileSpawner;
    // Start is called before the first frame update
    void Start()
    {
        projectileThrust = 150f;
        minProjectileThrust = 20f;
        maxProjectileThrust = 220f;
        canMove = true;
        isPlayersTurn = true;
        Cursor.visible = true;
        navMeshAgent = GetComponent<NavMeshAgent>();    
        proyectileSpawner = GetComponentInChildren<ProyectileSpawner>(); 
        projectileThrustBar.fillAmount = projectileThrust / maxProjectileThrust;   
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayersTurn && gameStarted){
            //Player Initial Movement Logic
            if(canMove) {
                if(isMoving) {
                    if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 1.6f) {
                        audioManager.PlayBreakingSound();
                        isMoving = false;
                    }

                }
                //Check if player has pressed left click to move
                if(Input.GetMouseButtonDown(0)) { 
                    audioManager.PlayMovingSound();        
                    StartCoroutine(MovePlayer());
                }

            } else {
                int isMovingLeft, isMovingUp;
                float increasedPower;
                //Checking if the player is rotating
                if(IsRotatingTank(out isMovingLeft)) {
                    transform.Rotate(0.15f * isMovingLeft * Vector3.up);
                    //towerTransform.Rotate(0.15f * isMovingLeft * Vector3.forward);
                } 
                //
                else if (IsElevatingTower(out isMovingUp)) {
                    towerTransform.Rotate(0.15f * isMovingUp * Vector3.right);
                    /*Quaternion newRotation;
                    newRotation = Quaternion.AngleAxis(isMovingUp*30, Vector3.right);
                    print(newRotation);
                    Quaternion q = Quaternion.Slerp(towerTransform.rotation, newRotation, Time.deltaTime);
                    float angle = Quaternion.Angle(q, newRotation);
                    print(angle);
                    towerTransform.rotation = q;
                    
                    if((towerTransform.rotation.y < -1f && towerTransform.rotation.z < 0)|| (towerTransform.rotation.y > -0.7f && towerTransform.rotation.z < -0.7f)) {
                        print("oN LIMIT");
                        towerTransform.Rotate(-1 * 0.15f * isMovingUp * Vector3.right);
                    } else {
                        towerTransform.Rotate(0.15f * isMovingUp * Vector3.right);
                    }*/
                } else if(IsIncreasingPower(out increasedPower)) {
                    projectileThrust += increasedPower;
                    projectileThrust = Mathf.Clamp(projectileThrust, minProjectileThrust, maxProjectileThrust);
                    projectileThrustBar.fillAmount = projectileThrust / maxProjectileThrust;
                }

                if(canFire && Input.GetKeyDown(KeyCode.Space)) {
                    canFire = false;
                    audioManager.PlayFireSound();
                    proyectileSpawner.SpawnProyectile(projectileThrust, audioManager);
                    gameManager.PlayerFiredShot();
                }       
            }
           
        }
        
    }

    //This method moves the tank to the place clicked by the player
    IEnumerator MovePlayer() {
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

    //Return true if the player is Rotating the tankwith the keys A or D, returns in as out the direction as well
    private bool IsIncreasingPower(out float increasedPower) {
        //If player is pressing the D key he is rotating the tank to the Right
        if(Input.GetKey(KeyCode.Q)) {
            increasedPower = 0.2f;
            return true;
        }        
        //If player is pressing the A key he is rotating the tank to the Left
        if(Input.GetKey(KeyCode.E)) {
            increasedPower = -0.2f;
            return true;
        }

        //The player is not rotating the tank
        increasedPower = 0;
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
