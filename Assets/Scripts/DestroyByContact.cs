using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Tetromino")
        {
            Destroy(collision.gameObject);
        }
    }
}
