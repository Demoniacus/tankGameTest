using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    AudioClip breakingSoundVFX, movingSoundVFX;
    
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

    public Image elevationBar;

    private float projectileThrust, minProjectileThrust, maxProjectileThrust;


    private ProyectileSpawner proyectileSpawner;

    private float currentElevation, minElevation, maxElevation;
    // Start is called before the first frame update
    void Start()
    {
        projectileThrust = 150f;
        minProjectileThrust = 20f;
        maxProjectileThrust = 220f;
        currentElevation = 0f;
        minElevation = 0f;
        maxElevation = 60f;
        canMove = true;
        isPlayersTurn = true;
        Cursor.visible = true;
        navMeshAgent = GetComponent<NavMeshAgent>();    
        proyectileSpawner = GetComponentInChildren<ProyectileSpawner>(); 
        projectileThrustBar.fillAmount = projectileThrust / maxProjectileThrust;   
        elevationBar.fillAmount = currentElevation  / maxElevation / (1/0.225f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayersTurn && gameStarted){
            //Player Initial Movement Logic
            if(canMove) {
                if(isMoving) {
                    if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance + 1.6f) {
                        Break();
                    }

                }
                //Check if player has pressed left click to move
                if(Input.GetMouseButtonDown(0)) {    
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
                    //If I'm between the limits update tower elevation
                    if(currentElevation >= minElevation && currentElevation <= maxElevation){
                        UpdateTowerElevation(isMovingUp);
                    } 
                    //Else if I'm over the max elevation only update it if it is going down
                    else if(currentElevation > maxElevation && isMovingUp == -1) {                        
                        UpdateTowerElevation(isMovingUp);
                    }                    
                    //Else if I'm over the min elevation only update it if it is going up
                    else if(currentElevation < minElevation && isMovingUp == 1) {                        
                        UpdateTowerElevation(isMovingUp);
                    } 
                } else if(IsIncreasingPower(out increasedPower)) {
                    projectileThrust += increasedPower;
                    projectileThrust = Mathf.Clamp(projectileThrust, minProjectileThrust, maxProjectileThrust);
                    projectileThrustBar.fillAmount = projectileThrust / maxProjectileThrust;
                }

                if(canFire && Input.GetKeyDown(KeyCode.Space)) {
                    canFire = false;
                    proyectileSpawner.SpawnProyectile(projectileThrust);
                    gameManager.PlayerFiredShot();
                }       
            }
           
        }
        
    }

    public void Break(){
        AudioManager.instance.PlayVFX(breakingSoundVFX,0.1f,0.6f,false);
        isMoving = false;
    }


    private void UpdateTowerElevation(float isMovingUp) {
        float newElevation = 0.15f * isMovingUp;
        currentElevation += newElevation;
        towerTransform.Rotate(newElevation * Vector3.right);
        elevationBar.fillAmount = currentElevation / maxElevation / (1/0.225f);
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
                        
                        AudioManager.instance.PlayVFX(movingSoundVFX,0.9f,0.4f,true);      
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
