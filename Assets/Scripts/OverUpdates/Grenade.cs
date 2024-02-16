using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 10f; // Скорость полета гранаты
    private Vector2 targetDirection; // Направление полета гранаты

    private void Start()
    {
        targetDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        targetDirection.Normalize();
    }

    private void Update()
    {
        transform.position += (Vector3)targetDirection * speed * Time.deltaTime;
    }
}
