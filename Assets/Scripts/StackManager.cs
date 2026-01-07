using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{
    public Transform brickPrefab; 
    public Transform blockContainer; 

    [SerializeField] private List<Transform> _bricks = new List<Transform>();
    private float _brickHeight = 0.715f; 

    void Start()
    {

        if (transform.childCount > 0)
        {
            _bricks.Add(transform.GetChild(0));
        }
    }

    public void AddBrick()
    {
        Transform lastBrick = _bricks[_bricks.Count - 1];
        Vector3 spawnPos = lastBrick.localPosition;
        spawnPos.y += _brickHeight;

        Transform newBrick = Instantiate(brickPrefab, blockContainer);
        newBrick.localPosition = spawnPos;
        newBrick.localRotation = Quaternion.identity;

        newBrick.DOPunchScale(Vector3.one * 0.3f, 0.2f);

        _bricks.Add(newBrick);
    }

    public void RemoveBrick()
    {
        if (_bricks.Count > 1)
        {
            Transform lastBrick = _bricks[_bricks.Count - 1];
            _bricks.RemoveAt(_bricks.Count - 1);

            lastBrick.SetParent(null);

            Vector3 dropPosition = lastBrick.position - transform.forward * 3f - transform.up * 2f;

            Sequence seq = DOTween.Sequence();

            seq.Join(lastBrick.DOJump(dropPosition, 2f, 1, 0.5f));

            seq.Join(lastBrick.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), 0), 0.5f, RotateMode.FastBeyond360));

            seq.Join(lastBrick.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack));

            seq.OnComplete(() =>
            {
                Destroy(lastBrick.gameObject);
            });
        }
    }
    public int GetBrickCount()
    {
        return Mathf.Max(0, _bricks.Count - 1); 
    }

}