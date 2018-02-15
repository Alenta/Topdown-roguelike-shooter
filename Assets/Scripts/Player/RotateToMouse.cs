using UnityEngine;
using System.Collections;

public class RotateToMouse : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public GameObject spawnPoint1;
    public Quaternion offset;
    public float speed;
    //private PlayerMovement playerMovement;
    public Vector2 direction;
    private Quaternion rotation;
    public Transform weaponPos;

    private void Start() {
        //playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update() {
        Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 direction = target - playerPos;

        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);

        
         weaponPos.rotation = rotation;
        







    }
    
}
