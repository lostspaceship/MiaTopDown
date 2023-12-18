using UnityEngine;

public class Key : MonoBehaviour
{
    public static int totalKeys = 0;
    public static int collectedKeys = 0;

    public GameObject door;
    public int requiredKeys = 3; 

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("Key picked up");

            collectedKeys++;

            UnityEngine.Debug.Log("Collected Keys: " + collectedKeys + " out of " + totalKeys);

            gameObject.SetActive(false);

            if (collectedKeys >= requiredKeys)
            {
                UnityEngine.Debug.Log("Opening the door");
                door.SetActive(false);
            }
        }
    }
}