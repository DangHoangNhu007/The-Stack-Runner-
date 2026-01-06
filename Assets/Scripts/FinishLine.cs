using UnityEngine;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{
    private bool _isFinished = false;

    void OnTriggerEnter(Collider other)
    {
        if (_isFinished) return;

        if (other.CompareTag("Player"))
        {
            _isFinished = true;
            Debug.Log("WIN!");

            GameManager.Instance.player.enabled = false;
            
            GameManager.Instance.LevelComplete();

            other.transform.DORotate(new Vector3(0, 1800, 0), 2f, RotateMode.FastBeyond360);
        }
    }
}