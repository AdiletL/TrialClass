using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public Vector3 offset; // Смещение камеры относительно игрока
    public float speed = 5;

    private void Start()
    {
        offset = transform.position;
    }
    private void LateUpdate()
    {
        // Плавное перемещение камеры к позиции игрока
        Vector3 desiredPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
    }
}
