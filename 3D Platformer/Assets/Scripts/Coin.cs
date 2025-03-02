using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public int coinValue = 1;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(coinValue);
            Destroy(gameObject);
        }
    }
}
