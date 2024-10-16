using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBoundary : MonoBehaviour
{
    public float Speed;

    private float playerX;

    [SerializeField]
    private Transform target;

    void Start()
    {
            transform.position = new Vector3(transform.position.x, (target.position.y - ((Score.playerHeight / 5) + 5)), transform.position.z);
    }

    void Update()
    {
        transform.Translate(0, Time.deltaTime * Speed, 0, Space.World);

        if (target != null)
        {
            playerX = target.position.x;

            transform.position = new Vector3(playerX, transform.position.y, transform.position.z);
        }
    }
}
