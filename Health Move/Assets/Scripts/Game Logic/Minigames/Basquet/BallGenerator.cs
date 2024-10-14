using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PsMoveAPI;
using System.Linq;

public class BallGenerator : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    BallThrowTrigger ballThrowTrigger;

    private void Start()
    {
        ballThrowTrigger = FindObjectOfType<BallThrowTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCollisionIdentifier player = other.GetComponent<PlayerCollisionIdentifier>();
        if (player == null || ballThrowTrigger.ball != null)
            return;

        Transform ballHolder = GameObject.Find("BallHolder").transform;
        GameObject ball = Instantiate(ballPrefab, ballHolder.position, Quaternion.identity);
        ball.transform.parent = ballHolder;
        ball.GetComponent<PlayerCollisionIdentifier>().PlayerIdentifier = player.PlayerIdentifier;
        ballThrowTrigger.ball = ball;
    }
}
