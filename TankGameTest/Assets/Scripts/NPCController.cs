using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    Transform towerTransform;
    public float range;

    public Transform spawnPoint;

    public GameManager gameManager {get; set;}

    public AudioManager audioManager {get; set;}
    
    public bool gameStarted  {get; set;}

    private NavMeshAgent navMeshAgent;

    private bool hasMoved;

    public bool canFire {get; set;}

    private bool isFiring;

    private ProyectileSpawner proyectileSpawner;

    private float projectileThrust, minProjectileThrust, maxProjectileThrust;



    // Start is called before the first frame update
    void Start()
    {
        projectileThrust = 150f;
        minProjectileThrust = 20f;
        maxProjectileThrust = 220f;
        hasMoved = false;
        isFiring = false;
        gameStarted = false;
        navMeshAgent = GetComponent<NavMeshAgent>();    
        proyectileSpawner = GetComponentInChildren<ProyectileSpawner>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted) {
            if(!hasMoved) {
                Vector3 point;
                if(RandomPoint(spawnPoint.position, range, out point)) {
                    navMeshAgent.SetDestination(point);
                    hasMoved = true;
                }
            }
            if(canFire && !isFiring) {
                isFiring = true;    
                canFire = false;
                StartCoroutine(FireShot());
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;

    }

    IEnumerator FireShot() {
        yield return new WaitForSeconds(3f);
        Rotate();        
        yield return new WaitForSeconds(0.8f);
        ElevateTower();        
        yield return new WaitForSeconds(0.8f);
        IncreasePower();
        audioManager.PlayFireSound();
        proyectileSpawner.SpawnProyectile(projectileThrust, audioManager);
        //Duration of the shooting
        yield return new WaitForSeconds(1f);
        gameManager.NPCFiredShot();        
        isFiring = false;
    }

    void OnCollisionEnter(Collision other){
        //next - check if we have collided with anything but player/enemy
        if(other.gameObject.tag == "Projectile"){
            audioManager.PlayHitTargetSound();
            gameManager.PlayerWon();
        }
    }

    private void Rotate() {
        float newRotation = Random.Range(-20,20);
        transform.Rotate(newRotation * Vector3.up);
    }

    private void ElevateTower() {        
        float newElevation = Random.Range(-20,20);
        towerTransform.Rotate(newElevation * Vector3.right);
    }

    private void IncreasePower() {
        projectileThrust += Random.Range(-18,18);
        projectileThrust = Mathf.Clamp(projectileThrust, minProjectileThrust, maxProjectileThrust);
    }

}
