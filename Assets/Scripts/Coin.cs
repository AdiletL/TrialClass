using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Coin : MonoBehaviour
{
    public static event Action OnCoin;
    private void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true;
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            OnCoin?.Invoke();
            Destroy(gameObject);
        }
    }
}
