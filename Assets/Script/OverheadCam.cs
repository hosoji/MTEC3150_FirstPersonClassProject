using UnityEngine;

public class OverheadCam : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
        
    }
}
