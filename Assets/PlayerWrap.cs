using UnityEngine;

public class PlayerWrap : MonoBehaviour
{
    public float screenLimitX = 4f; 

    void Update()
    {
        CheckWrapAround();
    }

    void CheckWrapAround()
    {
        
        if (transform.position.x > screenLimitX)
        {
            transform.position = new Vector3(-screenLimitX, transform.position.y, transform.position.z);
        }
        
        else if (transform.position.x < -screenLimitX)
        {
            transform.position = new Vector3(screenLimitX, transform.position.y, transform.position.z);
        }
    }
}

