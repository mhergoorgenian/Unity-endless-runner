using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class scoreanimation : MonoBehaviour
{
   public Transform score;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        score = GameObject.Find("scoretext").transform;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(score.position.x, score.position.y, score.position.z), 4);
        if (transform.position ==score.position)
        {
            GameObject.Find("gamemanager").GetComponent<gamemanager>().scorecount+= 100; ;
            Destroy(gameObject);
            
        }
        
    }



   
}
