using UnityEngine;
using System.Collections;

public class SpawnPlatforms : MonoBehaviour {

    public int maxPlatforms = 20; // number of platform to instantiate
    public GameObject platform; // reference to hold gameobject - ground
    public float horizontalMin = 175f; // to spawn platforms horizontally from one another - minimum 6.5 units to right
    public float horizontalMax = 325f; // max 14 units to the right - this is adequate for long the player can jump forward and land on a platform
    public float verticalMin = -90f; // some for y axis - spawn above or below from one another between -6 to 6 y position
    public float verticalMax = 90f;
    public GameObject EndPlatform;

    private Vector2 originPosition; // holds original position of ground
    //private Vector2 _flagPosition;
    //private int count;

    void Start()
    {

        originPosition = transform.position; // original position of ground at start (0,0)
       // _flagPosition = flagSpawn.position;
        Spawn(); // call Spawn method
    }

    void Spawn()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
           // count++;
            float vertical = 0;
            if (Random.Range(0,2) > 0)
            {
                vertical = Random.Range(-65f, verticalMin);
            }
            else
            {
                vertical = Random.Range(65f, verticalMax);
            }

            if (i == maxPlatforms - 1)
            {
                Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), vertical); // randomPosition incrementally equals originPosition + (between x = 6.5 to x = 14) (y = -6 to y = 6)
                Instantiate(EndPlatform, randomPosition, Quaternion.identity); // instantiate Ground platform - offset from the position of the last platform
                originPosition = randomPosition; // new origin position is the last instantiated to plateform - for new randomPosition in the next loop can increment an offset from the previous spawn position 
            }
            else
            {
                Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), vertical); // randomPosition incrementally equals originPosition + (between x = 6.5 to x = 14) (y = -6 to y = 6)
                Instantiate(platform, randomPosition, Quaternion.identity); // instantiate Ground platform - offset from the position of the last platform
                originPosition = randomPosition; // new origin position is the last instantiated to plateform - for new randomPosition in the next loop can increment an offset from the previous spawn position 
            }
        }
    }
}