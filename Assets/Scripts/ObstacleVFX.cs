using UnityEngine;

public class ObstacleVFX : MonoBehaviour
{
    [Header("Settings")]
    public int pieceCount = 8; 
    public float explosionForce = 10f; 
    public float scaleSize = 0.5f; 

    [Header("Prefab")]  
    public GameObject shatterPiecePrefab;

    public void Shatter()
    {

        Color wallColor = Color.red; 
        Renderer wallRenderer = GetComponent<Renderer>();
        if (wallRenderer != null)
        {
            wallColor = wallRenderer.material.color;

            wallRenderer.enabled = false;
        }
        
        Collider col = GetComponent<Collider>();
        if(col != null) col.enabled = false;

        for (int i = 0; i < pieceCount; i++)
        {
            SpawnPiece(wallColor);
        }

        Destroy(gameObject, 2f);
    }

    void SpawnPiece(Color color)
    {
        GameObject piece = Instantiate(shatterPiecePrefab);
    
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-0.5f, 0.5f), 
            Random.Range(0f, 1f), 
            Random.Range(-0.2f, 0.2f)
        );
        
        piece.transform.position = randomPos;
        piece.transform.localScale = Vector3.one * scaleSize;

        piece.GetComponent<Renderer>().material.color = color;

        Rigidbody rb = piece.AddComponent<Rigidbody>();
        rb.mass = 0.2f; 
        
        Vector3 forceDir = (piece.transform.position - transform.position).normalized;
        forceDir += Vector3.forward; 
        
        rb.AddForce(forceDir * explosionForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse); 

        Destroy(piece, 2f);
    }
}