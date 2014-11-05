using UnityEngine;
using System.Collections;

public class WobbleGameActions : MonoBehaviour 
{
    public GameObject Headgear;
    public GameObject DeathCloud;
    public GameObject WaterSplash;
    public GameObject SpeedAura;

    ScoreKeeper scoreKeeper;
    WobbleMovement wobbleMovement;
    
    bool dead;    
    Rect worldBounds;

    // Unity functions
    void Start()
    {
    //    CameraControl cc = Camera.main.GetComponent<CameraControl>();       
    //    if (cc) worldBounds = cc.CameraBounds;
 //       else worldBounds = new Rect(-100f, -100f, 200f, 200f);

        scoreKeeper = GameObject.Find("GameController").GetComponent<ScoreKeeper>();
        wobbleMovement = gameObject.GetComponent<WobbleMovement>();
    }
    void Update()
    {
        //CheckWorldBounds();
        if(transform.childCount == 1)
        {
            foreach(Transform star in transform)
            {
                if (rigidbody.useGravity)
                {
                    star.localPosition = Vector3.up * collider.bounds.extents.y * 1.25f;
                }
                else
                {
                    star.localPosition = Vector3.down * collider.bounds.extents.y * 1.25f;
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        // Kill if they hit a deadly object
        if (collision.gameObject.CompareTag("Death"))
        {
            DeathWithSmoke();
            return;
        }        
    }
    void OnTriggerEnter(Collider other)
    {
        // Pick up stars
        
        if (other.gameObject.CompareTag("Star"))
        {
            scoreKeeper.addStar();
            AudioManager.Play("Score");
            GameObject.Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Pickup"))
        {
            if (other.name == "SpeedPickup")
            {
                scoreKeeper.addSpeedMult(1);
            //    wobbleMovement.wobbleConstantForce.relativeForce += new Vector3(50, 0, 0);
                Time.timeScale += 0.2f;
                GameObject.Destroy(other.gameObject);

                GameObject aura = (GameObject)Instantiate(SpeedAura, transform.position - new Vector3(0,0.75f,0), SpeedAura.transform.rotation);
                aura.transform.parent = gameObject.transform;
                Destroy(aura, 2f);
            }
        }
         
        if(other.gameObject.CompareTag("Death"))
        {
            if(other.name == "water")
            {
                CapsuleCollider capCollid = collider as CapsuleCollider;
                Instantiate(WaterSplash, transform.position + capCollid.center + Vector3.down * (collider.bounds.extents.y - 0.15f), Quaternion.Euler(90, 0f, 0f));
                AudioManager.Play("waterSplash_EQ");
            }
            else
            {
                AudioManager.Play("Poof");
                AudioManager.Play("Wobble_Die");
                Destroy(Instantiate(DeathCloud, transform.position + Vector3.forward * 0.2f, WaterSplash.transform.rotation), 1f);
            }
            DeathWithHelmet();
            return;
        }
        // Score the wobble when they hit home
        /*
        if (other.gameObject.CompareTag("Home"))
        {
            AudioManager.Play("Wobble_Score");
            foreach (Transform star in transform)
            {
                // Score carried stars
                if (star.name.Contains("Star"))
                {
                    AudioManager.Play("Score");
                    if (Camera.main) Camera.main.SendMessage("ScoreStar");
                    Destroy(star.gameObject);
                }
            }
            if (Camera.main) Camera.main.SendMessage("ScoreWobble");
        }
         * */
    }
    void OnDestroy()
    {
        /*
        if (Camera.main)
        {
            Camera.main.SendMessage("SubtractWobble");
        }
        foreach (Transform star in transform)
        {
            star.parent = null;
            star.GetComponent<tk2dSprite>().scale *= 1.333333f;
            star.Translate(Vector3.down * collider.bounds.extents.y);
        }
         * */
    }

    // Helper functions
    void CheckWorldBounds()
    {
        bool left = (transform.position.x < worldBounds.xMin - 2f);
        bool right = (transform.position.x > worldBounds.xMax + 2f);
        bool bottom = (transform.position.y < worldBounds.yMin - 2f);
        bool top = (transform.position.y > worldBounds.yMax * + 2f);
        if (left || right || top || bottom)
        {
            foreach (Transform star in transform)
            {
                Destroy(star.gameObject);
            }
            Destroy(gameObject);
        }
    }
    void DeathWithSmoke()
    {
        AudioManager.Play("Poof");
        AudioManager.Play("Wobble_Die");
        Destroy(Instantiate(DeathCloud, transform.position + Vector3.forward * 0.2f, Quaternion.identity), 1f);        
        DeathWithHelmet();
    }
    void DeathWithHelmet()
    {
        if (dead) return; else dead = true;
        Vector3 position = rigidbody.useGravity ? new Vector3(0f, collider.bounds.extents.y, 0.1f) : new Vector3(0f, -collider.bounds.extents.y, 0.1f);
        GameObject headGear = Instantiate(Headgear, collider.bounds.center + position, Quaternion.identity) as GameObject;
        headGear.rigidbody.AddTorque(Vector3.forward * Random.Range(-7f, 7f), ForceMode.VelocityChange);
        headGear.rigidbody.AddForce((rigidbody.useGravity ? Vector3.up : Vector3.down) * 3f, ForceMode.VelocityChange);
        if (!rigidbody.useGravity)
        {
            headGear.rigidbody.useGravity = rigidbody.useGravity;
            headGear.constantForce.force = headGear.rigidbody.mass * - Physics.gravity;
        }        
        Destroy(gameObject);
    }
}
