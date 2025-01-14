using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float offsetY = 0f; 
    public float fixedX = 0f; 
    public float fixedZ = -14f; 

    void Update()
    {
        if (target != null)
        {
            
            Vector3 newPosition = new Vector3(fixedX, target.position.y + offsetY, fixedZ);

            
            transform.position = newPosition;
        }
    }
}
