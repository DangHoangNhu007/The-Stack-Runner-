using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private bool _isCollected = false;

    void OnTriggerEnter(Collider other)
    {
        if (_isCollected) return; 

        if (other.CompareTag("Player")) 
        {
            _isCollected = true;
            StackManager stack = other.GetComponentInParent<StackManager>();
            
            if (stack != null)
            {
                stack.AddBrick();
                Destroy(gameObject); 
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StackManager stack = GetComponentInParent<StackManager>();
            if (stack != null)
            {
                stack.RemoveBrick();
                collision.collider.enabled = false; 
            }
        }
    }
}