using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float endYPoz;
    [SerializeField] private float startYPoz;


    private void Update()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        transform.Translate(Vector2.down * Mathf.Abs(speed) * Time.deltaTime);

        if (transform.position.y <= endYPoz)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        Vector2 newPosition = new Vector2(transform.position.x, startYPoz);
        transform.position = newPosition;
    }
}

