using UnityEngine;
public class ShieldController : MonoBehaviour
{
    [SerializeField] private float duration = 5f; 
    private float timer;

    private void Start()
    {
        timer = duration;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject); 
        }
    }
}