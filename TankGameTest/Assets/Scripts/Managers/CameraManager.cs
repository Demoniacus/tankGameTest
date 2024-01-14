using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private GameObject player;

    public float zoom;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + zoom, player.transform.position.z);
    }
}
