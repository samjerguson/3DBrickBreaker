using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    Vector3 position;
    Quaternion rotation;
    public GameObject block;
    public float prob;
    //maybe multiply these values by the corresponding plane scale when we want a larger play area?
    float height;
    float xCheck = -4.5f;
    float yCheck = .5f;
    float zCheck = -4.5f;


    // Start is called before the first frame update
    void Start()
    {
        rotation = Quaternion.identity;
        height = 10; //change this to be dynamic
        for(float i = yCheck; i <= height; i++)
        {
            for(float j = zCheck; j <= zCheck * -1; j++)
            {
                for(float k = xCheck; k <= xCheck * -1; k++)
                {
                    if(Random.value < prob / 100)
                    {
                        position = new Vector3(k, i, j);
                        GameObject newBlock = Instantiate(block, position, rotation);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
