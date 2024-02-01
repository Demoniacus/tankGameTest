using UnityEngine;

public class ProyectileLogic : MonoBehaviour
{
    public float thrust;

    [SerializeField]
    GameObject explosionSplash;

    [SerializeField]
    AudioClip firedVFX,onHitVFX;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.forward * thrust, ForceMode.Impulse);
        AudioManager.instance.PlayVFX(firedVFX, 1f, 1f, false);
    }

    void OnCollisionEnter(Collision other){
        //next - check if we have collided with anything but player/enemy
        if(other.gameObject.tag == "Player"){            
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemy"){
            AudioManager.instance.PlayVFX(onHitVFX, 1f, 1f, false);    
            Destroy(gameObject);
        } else {
            AudioManager.instance.PlayVFX(onHitVFX, 1f, 1f, false);       
            Instantiate(explosionSplash, other.GetContact(0).point, new Quaternion(0,0,0,0));
            Destroy(gameObject);
        }
    }


}
