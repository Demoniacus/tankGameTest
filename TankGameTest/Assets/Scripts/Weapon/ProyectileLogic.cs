using UnityEngine;

public class ProyectileLogic : MonoBehaviour
{

    public float thrust;

    public AudioManager audioManager;

    [SerializeField]
    GameObject explosionSplash;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other){
        //next - check if we have collided with anything but player/enemy
        if(other.gameObject.tag == "Player"){            
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemy"){
            Destroy(gameObject);
        } else {
            audioManager.PlayHitTargetSound();            
            Instantiate(explosionSplash, other.GetContact(0).point, new Quaternion(0,0,0,0));
            Destroy(gameObject);
        }
    }


}
