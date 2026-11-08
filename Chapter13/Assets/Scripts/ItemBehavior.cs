using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Item collected!");

            GameManager.Items += 1;
            GameManager.PrintLootReport();
        }
    }
}
