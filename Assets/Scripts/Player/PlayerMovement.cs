using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private PlayerConfiguration playerConfiguration;
    private Tween playerMoveTween;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float newXPosition = transform.position.x + horizontalInput * playerConfiguration.currentMoveSpeed * Time.fixedDeltaTime;

        newXPosition = Mathf.Clamp(newXPosition, minX, maxX);

        Vector3 newPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);

        playerMoveTween = transform.DOMove(newPosition, 0.02f);
    }

    private void OnDisable()
    {
        playerMoveTween.Kill();
    }
}
