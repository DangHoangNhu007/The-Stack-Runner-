using UnityEngine;
using DG.Tweening; 

public class PlayerDeath : MonoBehaviour
{
    [Header("Explosion Settings")]
    public int cubeCount = 10; 
    public float explosionForce = 500f; 
    public float explosionRadius = 2f; 
    public Material debrisMaterial; 

    public void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<StackManager>().enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        SpawnDebris();

        Camera.main.transform.DOShakePosition(0.5f, 1f, 10, 90);

        Time.timeScale = 0.2f;

        DOVirtual.DelayedCall(0.5f, () =>
        {
            Time.timeScale = 1f;
        });

        GameManager.Instance.GameOver();
        Debug.Log("💀 PLAYER DEAD!");
    }

    void SpawnDebris()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

            piece.transform.position = transform.position + Random.insideUnitSphere * 0.5f;
            piece.transform.localScale = Vector3.one * 0.4f; 

            if (debrisMaterial != null)
                piece.GetComponent<Renderer>().material = debrisMaterial;

            Rigidbody rb = piece.AddComponent<Rigidbody>();
            rb.mass = 0.5f;

            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            rb.AddTorque(Random.insideUnitSphere * 500f);

            Destroy(piece, 3f);
        }
    }
}