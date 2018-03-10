using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplehook : MonoBehaviour {
    public GameObject hook;
    public GameObject bulletsOut;
    public GameObject rope;
    public LineRenderer line;
    public float step = 0.2f;
    public int projectileCountMax = 1;
    public int projectileCountMin = 1;
    public Hook hookScript;
    public float shootInterval = 0.2f;
    public bool readyToFire;
    public int baseDamage;

    
    //private AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    private Vector3 targetPos;
    DistanceJoint2D joint;
    RaycastHit2D hit;
    public LayerMask mask;
    public bool hookCreated;

    // Use this for initialization
    void Start()
    {
        
        readyToFire = true;
        //audioSource = gameObject.GetComponent<AudioSource>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RayCast();
        if (joint.distance > 1f)
            joint.distance -= step;
        else if (hookCreated && hit)
        {
            hookScript.hookComplete = true;
            hookScript = null;
            line.enabled = false;
            joint.enabled = false;
            hookCreated = false;
        }
        if (Input.GetMouseButtonDown(1) && !hookCreated)
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            Fire(targetPos);
            
        }
        
        
        if (hookCreated)
        {

            line.SetPosition(0, transform.position);
            line.SetPosition(1, hookScript.transform.position);
            line.enabled = true;
            readyToFire = false;
        }
        else
        {
            readyToFire = true;
        }
        

        
        
    }
    public virtual void Fire(Vector2 target)
    {
        if (!readyToFire)
        {
            return;
        }
        
        var projectileGO = Instantiate(hook, bulletsOut.transform.position, Quaternion.identity, null);
        
        projectileGO.SetActive(true);
        projectileGO.name = "Hook";

        var newProjectile = projectileGO.GetComponent<BaseProjectile>();
        hookScript = projectileGO.GetComponent<Hook>();
        newProjectile.Fire(target, true,baseDamage,1,false,true,false);
        hookCreated = true;



    }
    private void RayCast()
    {
        if (hookCreated && hookScript.hitWall)
        {
            hit = Physics2D.Raycast(transform.position, hookScript.transform.position - transform.position, 100f, mask); //100f = distance
            if (hit.collider.gameObject.GetComponent<Hook>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(transform.position, hit.point);
                joint.connectedAnchor = hit.point - new Vector2(transform.position.x, hit.collider.transform.position.y);

            }
        }
    }
}
