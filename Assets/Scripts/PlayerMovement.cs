using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float forwardSpeed = 5f;
    public float sensitivity = 10f;
    public float xLimit = 4f; 

    private float _lastMouseX;

    void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            _lastMouseX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - _lastMouseX;
            _lastMouseX = Input.mousePosition.x;

            Vector3 newPos = transform.position;
            newPos.x += deltaX * sensitivity * Time.deltaTime / Screen.width * 100; 
            
            newPos.x = Mathf.Clamp(newPos.x, -xLimit, xLimit);
            
            transform.position = newPos;
        }
    }
}