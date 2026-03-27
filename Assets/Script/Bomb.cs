using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 2f;
    public float radius = 5f;
    public float force = 100f;
    public GameObject explosionFX;

    public AudioClip explosionSound;
    private AudioSource audioSource;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
      

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();


        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
    

        if (explosionFX != null)
        {
            GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(fx, 2f);
        }

        if (explosionSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(explosionSound);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Destructible"))
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb == null)
                {
                    rb = hit.gameObject.AddComponent<Rigidbody>();
                }

                rb.isKinematic = false;

                // ปรับค่าฟิสิกส์ให้กระเด็นแรงขึ้น
                rb.linearDamping = 0.5f;


                rb.AddExplosionForce(force, transform.position, radius, 2f, ForceMode.Impulse);

                // delay destroy เพื่อให้เห็นฟิสิกส์
                Destroy(hit.gameObject, 10f);

                if (scoreManager != null)
                {
                    scoreManager.AddScore(100);
                }
            }
        }
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        Destroy(gameObject);
    }
}