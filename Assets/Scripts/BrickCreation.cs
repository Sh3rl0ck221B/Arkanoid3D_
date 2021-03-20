using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

public class BrickCreation : MonoBehaviour
{ 
    // Start is called before the first frame update
    public GameObject brickPrefab;
    public GameObject itemPrefab;

    void Start()
    {
        for (float x = -15; x <= 15; x+=3)
        {
            for (float z = -5; z <= 10  ; z+=2f)
            {
                GameObject brick = Instantiate(brickPrefab, new Vector3(x, 0, z), Quaternion.identity);
                brick.AddComponent<Brick>();
            }
        }
    }
    
}
