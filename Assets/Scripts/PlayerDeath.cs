using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PlayerDeath : MonoBehaviour
{
    [Header("Explosion Settings")]
    public int cubeCount = 10;
    public float explosionForce = 500f;
    public float explosionRadius = 2f;
    public Material debrisMaterial;
    [SerializeField] private List<GameObject> listPlayerPart;

    public void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<StackManager>().enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        //SpawnDebris();
        PlayerExlosion();

        Camera.main.transform.DOShakePosition(0.5f, 1f, 10, 90);

        Time.timeScale = 0.2f;

        DOVirtual.DelayedCall(0.5f, () =>
        {
            Time.timeScale = 1f;
        });

        GameManager.Instance.GameOver();
        Debug.Log("💀 PLAYER DEAD!");
    }

    void PlayerExlosion()
    {
        foreach (GameObject part in listPlayerPart)
        {
            part.transform.SetParent(null);

            part.transform.position = transform.position + Random.insideUnitSphere * 0.5f;

            Rigidbody rb = part.AddComponent<Rigidbody>();
            rb.mass = 0.5f;

            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            rb.AddTorque(Random.insideUnitSphere * 500f);

            Destroy(part, 3f);
        }
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