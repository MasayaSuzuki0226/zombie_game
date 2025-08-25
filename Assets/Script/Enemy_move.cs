using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_momve : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage = 10;

    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0,-speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Move>().Damage(damage);
        }
    }

}
