using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{

    public float range;

    public Transform spawnPoint;

    private NavMeshAgent navMeshAgent;

    private bool hasMoved;

    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasMoved) {
            Vector3 point;
            if(RandomPoint(spawnPoint.position, range, out point)) {
                navMeshAgent.SetDestination(point);
                hasMoved = true;
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


}
