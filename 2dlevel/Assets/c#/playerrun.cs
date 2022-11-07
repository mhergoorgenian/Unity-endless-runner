using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class playerrun : MonoBehaviour
{
   
    float speed=10;
    float jumpforce=250f;
    int numofbullets = 0;
    [SerializeField] private bool jumped;
    [SerializeField] private bool dead;
    [SerializeField] private bool phase2;
    [SerializeField] private bool attack;
    [SerializeField] private Transform aimpos;
    [SerializeField] private Transform bulletspawn;
    [SerializeField] private GameObject bullet;
    public List<GameObject> boxes = new List<GameObject>();
    [SerializeField] private bool isCooldown;
    [SerializeField] private bool bulletcreated;
    
    private Rigidbody rb;
    private Vector2 startpostion, endpostion;
    private Animator anim;
    public GameObject cam1,cam2, cam3;
    private GameObject bullets;
    
    public int bulletnum=0;
    
    public GameObject panelrest;
    private AudioSource aud;


    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // bulletpos();
        Animation();
        //player movements
        Vector3 run = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + run);

     
        
        //jump with touch
        //checking touched positions
        if (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began)
        {
            startpostion = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endpostion = Input.GetTouch(0).position;
            if (endpostion.y > startpostion.y && !jumped && !phase2)
            {
                jumped = true;
                rb.AddForce(Vector2.up * jumpforce);

                startpostion = Vector2.zero;
                endpostion = Vector2.zero;
                
            }
            else if (endpostion.y > startpostion.y && phase2 && !isCooldown && bullets != null)
            {

                attack = true;
                StartCoroutine(Cooldown());
                


            }
        }
        if (bullets != null && bullets.transform.parent == null)
        {
            cam3.SetActive(true);
            cam3.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = bullets.gameObject.transform;
            cam2.SetActive(false);
        }
        
        else if(bullets!=null)
        {
            
            bullets.transform.position = bulletspawn.transform.position;
            
        }
        else if(bullets==null&&phase2)
        {
            cam3.SetActive(false);
            cam2.SetActive(true);
        }
        if (bulletnum == 4)
        {
            phase2 = false;
            bulletnum = 0;
        }

    }
    //checking collisions
    private void OnCollisionEnter(Collision collision)
    {
        //collison with ground
        if (collision.gameObject.tag == "ground")
        {
            jumped = false;
        }
        if (collision.gameObject.tag == "obs")
        {
            dead = true;
            
        }
        if (collision.gameObject.tag == "phase")
        {
            
            StartCoroutine(waitforcamphase());
            Debug.Log("phasestart");
            var phasestation= GameObject.FindGameObjectWithTag("phase");
            phasestation.GetComponent<Collider>().isTrigger = true;
            foreach(Transform child in phasestation.transform)
            {

                boxes.Add(child.gameObject);
            }

        }
    }
    //animation controlling
    private void Animation()
    {
        //jump animations
        if (jumped)
        {
            anim.SetBool("animjump", true);
        }
        else
        {
            anim.SetBool("animjump", false);

        }
        //dead animation
        if (dead)
        {
            anim.SetBool("dead", true);
            speed = 0;
            jumpforce = 0;
            Debug.Log("dead");
            Time.timeScale = 0;
            panelrest.SetActive(true);
            
        }
        //phase playing
        if (phase2)
        {
            speed = 0;
            jumpforce = 0;

            anim.SetBool("readytr", true);
            startphase();
            
        }
        //after phase
        else if(!phase2 && !dead)
        {
            anim.SetBool("readytr", false);
            cam2.SetActive(false);
            cam1.SetActive(true);
            
            speed = 10;
            jumpforce = 250;
            
        }
        //attack checking
        if (attack)
        {
            //cam.GetComponent<Animator>().SetBool("phase", true);
            anim.SetBool("throw", true);
           


        }
        else
        {
            anim.SetBool("throw", false);
        }
       

        
    }

    //start attacking
    private void attackfunc()
    {
        
           numofbullets = 0;
            
            bullets.transform.parent = null;
             attack = false;
    }
    //start phase idle mode
    private void startphase()
    {
        
        if (!bulletcreated)
        { 
            Debug.Log("bullet created");
            bullets = Instantiate(bullet, bulletspawn.position, bullet.transform.rotation);
            bullets.transform.SetParent(bulletspawn.transform);
            
            numofbullets++;
            bulletcreated = true;
        }
       



    }
  
    private IEnumerator Cooldown()
    {
        // Start cooldown
        isCooldown = true;
        
        // Wait for time you want
        yield return new WaitForSeconds(2f);
        // Stop cooldown
        
        bulletcreated = false;
        
        isCooldown = false;
        
    }
    private IEnumerator waitforcamphase()
    {
        
        cam1.SetActive(false);
        cam2.SetActive(true);
        phase2 = true;
        bulletcreated = true;


        // Wait for time you want
        yield return new WaitForSeconds(1f);
        // Stop cooldown

        
        bulletcreated = false;

    }
    
   


}
