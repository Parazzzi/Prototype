using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private Tween bonusTween;
    private float bonusSpeed = 0.4f;

    private void OnEnable()
    {
        MoveBonus();
    }

    public void MoveBonus()
    {
        float positoinY = -18;

        if (bonusTween != null && bonusTween.IsActive())
        {
            bonusTween.Kill();
        }

        bonusTween = transform.DOMoveY(positoinY, 1f / bonusSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(DestroyBonus);
    }

    private void DestroyBonus()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        bonusTween.Kill();
    }
}
