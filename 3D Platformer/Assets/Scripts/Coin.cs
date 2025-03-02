using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public int coinValue = 1;

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
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
