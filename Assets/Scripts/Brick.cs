using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

public class Brick : MonoBehaviour
{
    public int destructionCounter { get; set; }
    private Paddle paddle;

    private void Start()
    {
        tag = "Brick";
        Debug.Log("Created Brick");
        destructionCounter = Random.Range(0, 4);

        Renderer rend = GetComponent<Renderer>();
        rend.material.color = DetermineColour();
        BoxCollider boxcollider = gameObject.AddComponent<BoxCollider>();
        boxcollider.isTrigger = true;
        
        paddle = GameObject.Find("Paddle").GetComponent<Paddle>();
        
    }


    private void OnDestroy()
    {
        paddle.AddPoints(50);
        
        if (Random.Range(0,4) == 3)
        {
            createItem();
        }
    }

    private void createItem()
    {
        var loadedPrefabResource = LoadPrefabFromFile("Item");
        GameObject item = (GameObject) Instantiate(loadedPrefabResource,
            new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        item.AddComponent<Item>();
    }
    
    private Object LoadPrefabFromFile(string filename)
    {
        Debug.Log("Trying to load LevelPrefab from file ("+filename+ ")...");
        var loadedObject = Resources.Load("Item");
        if (loadedObject == null)
        {
            throw new FileNotFoundException("...no file found - please check the configuration");
        }
        return loadedObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            if (destructionCounter == 0)
            {
                Destroy(this.gameObject);
            }

            destructionCounter--;
            paddle.AddPoints(10);
            
            GetComponent<Renderer>().material.color = DetermineColour();;
        }
    }

    public Color DetermineColour()
    {
        switch (destructionCounter)
        {
            case 0:
                return Color.gray;
            case 1:
                return Color.green;
            case 2:
                return Color.cyan;
            case 3:
                return Color.blue;
            default:
                return Color.black;
        }
    }
}