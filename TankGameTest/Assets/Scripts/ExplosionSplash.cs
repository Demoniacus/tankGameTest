using System.Collections;
using UnityEngine;

public class ExplosionSplash : MonoBehaviour
{

    public GameObject explosionSpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveExplosionLayer());
    }

    IEnumerator RemoveExplosionLayer() {
        yield return new WaitForSeconds(1.8f);
        Destroy(explosionSpriteRenderer);
    }


}
