using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerJump : MonoBehaviour
{
    private PlayerAnimation playerAnimation;
    public float jumpForce = 4f; // Сила прыжка
    private bool isGrounded = true; // Проверка, на земле ли игрок
    private Rigidbody rb; // Ссылка на Rigidbody

    private void Reset()
    {
        GetComponent<BoxCollider>().center = Vector3.up * .5f;
    }

    void Start()
    {
        // Получаем компонент Rigidbody
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateJump()
    {
        // Прыжок по нажатию пробела
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Добавляем импульс вверх
            isGrounded = false; // Пока в прыжке, игрок не на земле
            playerAnimation.UpdateJump(isGrounded);
        }
    }

    // Проверяем, находится ли игрок на земле
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Проверяем столкновение с объектом, у которого тег "Ground"
        {
            isGrounded = true; // Если столкнулись с землей, разрешаем прыгать снова
            playerAnimation.UpdateJump(isGrounded);
        }
    }
}
