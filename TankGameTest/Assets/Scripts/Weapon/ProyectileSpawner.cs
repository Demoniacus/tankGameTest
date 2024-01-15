using UnityEngine;

public class ProyectileSpawner : MonoBehaviour
{

    public GameObject proyectile;


    public void SpawnProyectile(float thrust, AudioManager audioManager) {
        ProyectileLogic proyectileLogic = proyectile.GetComponent<ProyectileLogic>();
        proyectileLogic.thrust = thrust/10;
        proyectileLogic.audioManager = audioManager;
        Instantiate(proyectile, transform.position, transform.rotation);
    }
}
