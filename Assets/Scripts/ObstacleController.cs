using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private bool _hasCollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_hasCollided) return;

        if (other.CompareTag("Player"))
        {
            StackManager stackManager = other.GetComponent<StackManager>();

            if (stackManager != null)
            {
                _hasCollided = true;

                if (stackManager.GetBrickCount() > 0)
                {
                    stackManager.RemoveBrick();

                    ObstacleVFX vfx = GetComponent<ObstacleVFX>();
                    if (vfx != null)
                    {
                        AudioManager.Instance.PlayCrash();
                        vfx.Shatter(); 
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();
                    if (playerDeath != null)
                    {
                        AudioManager.Instance.PlayCrash();
                        playerDeath.Die();
                    }
                }
            }
        }
    }
}