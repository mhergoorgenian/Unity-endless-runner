using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmove : MonoBehaviour
{

    public List<GameObject> boxes = new List<GameObject>();
    int selectedbox = 0;
    Color[] colors = new[] {
    Color.blue,
    Color.magenta,
    Color.red,
    Color.green,
    Color.yellow};
    public GameObject scoreplus;
    // Start is called before the first frame update
    void Start()
    {


    }
        // Update is called once per frame
    void Update()
    {

       boxes = GameObject.Find("player").GetComponent<playerrun>().boxes;
       if (transform.parent == null && boxes.Count != 0)
       {
        transform.position = Vector3.MoveTowards(transform.position, boxes[selectedbox].transform.position, 1f);
       }

    }
    
    private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "boxes")
            {

                var randcol = Random.Range(0, colors.Length);
                collision.gameObject.GetComponent<Renderer>().material.color = colors[randcol];
               var scoretext= Instantiate(scoreplus, new Vector3(boxes[selectedbox].transform.position.x, boxes[selectedbox].transform.position.y+3, boxes[selectedbox].transform.position.z),Quaternion.identity);
               
            Debug.Log("created 100score");
                Destroy(gameObject);
                Debug.Log("deleted");
                GameObject.Find("player").GetComponent<playerrun>().bulletnum++;
                GameObject.Find("player").GetComponent<playerrun>().boxes.RemoveAt(selectedbox);
            }
        }

    
}
