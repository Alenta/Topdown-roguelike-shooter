using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

    enum PlayerAbilities
    {
        PiercingShot, BouncingShot, ShotSize, BoomerangShot, ZigZagShot, SpiralShot
    }
    
    public Vector2 direction;
    public float speedMin = 1;
    public float speedMax = 2;
    public float rangeMin = 3;
    public float rangeMax = 5;
    public int totalDamage;
    private Vector2 returnDirection;
    public float directionOffset = 10;
    public float life = 100f;
    private float lifetime;
    private float age;
    private List<Vector3> line;
    public float lineWidth = 0.025f;
    public float rotOffset;
    public int splitShot;
    public bool boomerangShot;
    public Vector2 tar;
    public bool piercingShot;
    public bool bouncing;
    
    private bool addedForce;
    private Rigidbody2D rb;

    private LineRenderer lineRenderer;

    public bool playerOwned;

    // Use this for initialization
    void Start () {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        line = new List<Vector3> { transform.position, transform.position };
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(line.ToArray());
        lineRenderer.startWidth = 0;
        lineRenderer.endWidth = lineWidth;

        
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (age < lifetime * 2 && life != 0)
        {
            age += Time.deltaTime;
            if (!addedForce)
            {
                AddForce();
                addedForce = true;
            }
            //transform.position += (Vector3)direction * Time.deltaTime;
            line.Add(transform.position);
            lineRenderer.positionCount = line.Count;
            lineRenderer.SetPositions(line.ToArray());

            lineRenderer.endWidth = lineWidth * ((age / lifetime * -1) + 1);
        }
        
        else if (age > lifetime) //life set to zero bypasses lifetime
        {
            Destroy(gameObject);
            
        }
        
    }
    public void AddForce()
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    public void Fire(Vector2 target, bool playerOwned, int damage, int splitshot, bool boomerang, bool piercing, bool bouncingShot)
    {
        this.playerOwned = playerOwned;
        tar = target;
        totalDamage = damage;
        direction = (target - (Vector2)transform.position);
        
        direction = Rotate(direction, Random.Range(-directionOffset, directionOffset)).normalized;
        float speed = Random.Range(speedMin, speedMax);
        direction *= speed;
        splitShot = splitshot;
        boomerangShot = boomerang;
        piercingShot = piercing;
        bouncing = bouncingShot;
        //float distance = Random.Range(rangeMin, rangeMax);
        lifetime = life/*distance / speed*/;
        age = 0;

        Vector3 diff = (Vector3)target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - rotOffset);
        
        
    }

    private Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
    
    

}
