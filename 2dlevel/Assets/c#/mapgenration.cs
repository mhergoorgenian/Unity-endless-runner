using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgenration : MonoBehaviour
{
    [SerializeField] private GameObject[] groundPrefab;
    private Transform playerTransform;
    private float spawnZ = 0;
    private float groundLength = 100f;
    private int numofGroundOnScreen = 3;
    private int maps = 0;
    private List<GameObject> activeGrounds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        playerTransform = GameObject.FindGameObjectWithTag("player").transform;

        for (int i = 0; i < numofGroundOnScreen; i++)
        {
            SpawnGround();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //check player postion 
        if (playerTransform.position.z > activeGrounds[0].transform.position.z+50)
        {
            SpawnGround();
            RemoveGround();
        }
    }

    // making grounds
    private void SpawnGround()
    {
        
        if (maps == 3)
        {
            maps = 0;
        }
        
        GameObject go;
        go = Instantiate(groundPrefab[maps]);
        activeGrounds.Add(go);
        go.transform.SetParent(transform);
        go.transform.position = new Vector3(go.transform.position.x, 0, spawnZ);
        spawnZ += groundLength;
        maps++;

    }
   
    private void RemoveGround()
    {
        Destroy(activeGrounds[0],5);
        activeGrounds.RemoveAt(0);
    }
}
