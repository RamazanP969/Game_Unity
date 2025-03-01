using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; 
    [SerializeField] float distance = 10f; 

    private Vector3 startPosition; 
    private bool movingRight = true; 

    void Start()
    {
      
        startPosition = transform.position;
    }

    void Update()
    {
      
        if (movingRight)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, startPosition + Vector3.right * distance, moveSpeed * Time.deltaTime);

       
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false;
            }
        }
        else
        {
         
            transform.position = Vector3.MoveTowards(transform.position, startPosition - Vector3.right * distance, moveSpeed * Time.deltaTime);

      
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true;
            }
        }
    }
}
