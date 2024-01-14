using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{

    public float range;

    public Transform spawnPoint;

    public GameManager gameManager {get; set;}

    public AudioManager audioManager {get; set;}
    
    public bool gameStarted  {get; set;}

    private NavMeshAgent navMeshAgent;

    private bool hasMoved;

    public bool canFire {get; set;}

    private bool isFiring;


    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
        isFiring = false;
        gameStarted = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
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

        //Duration of the shooting
        yield return new WaitForSeconds(3f);
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


}
