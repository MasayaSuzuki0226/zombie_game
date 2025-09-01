using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_momve : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage = 10;
    public Transform[] waypoints;//巡回ポイント
    public Transform player;//追跡対象
    [SerializeField] private float chaseRange = 5f; // 追跡開始の距離

    private int currentWaypoint = 0;
    private bool isChasing = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // プレイヤーを発見したら追跡モード
        if (distanceToPlayer < chaseRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            // プレイヤーを追いかける
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            // 巡回モード
            Patrol();
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];//巡回ポイントをターゲットにしてそこまで移動する
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)//巡回ポイントについたら次の巡回ポイントに移動する
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Move>().Damage(damage);
        }
    }

}


