using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_momve : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage = 10;
    public Transform[] waypoints;//����|�C���g
    public Transform player;//�ǐՑΏ�
    [SerializeField] private float chaseRange = 5f; // �ǐՊJ�n�̋���

    private int currentWaypoint = 0;
    private bool isChasing = false;

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // �v���C���[�𔭌�������ǐՃ��[�h
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
            // �v���C���[��ǂ�������
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            // ���񃂁[�h
            Patrol();
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypoint];//����|�C���g���^�[�Q�b�g�ɂ��Ă����܂ňړ�����
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)//����|�C���g�ɂ����玟�̏���|�C���g�Ɉړ�����
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


