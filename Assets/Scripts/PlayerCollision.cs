using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            GetComponent<StackManager>().AddBrick();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {
            GetComponent<StackManager>().RemoveBrick();
        }
    }
}
