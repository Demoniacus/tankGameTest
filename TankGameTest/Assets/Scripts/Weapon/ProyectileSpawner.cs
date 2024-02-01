using UnityEngine;

public class ProyectileSpawner : MonoBehaviour
{

    public GameObject proyectile;


    public void SpawnProyectile(float thrust) {
        ProyectileLogic proyectileLogic = proyectile.GetComponent<ProyectileLogic>();
        proyectileLogic.thrust = thrust/10;
        Instantiate(proyectile, transform.position, transform.rotation);
    }
}
