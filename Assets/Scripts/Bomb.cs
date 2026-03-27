using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay = 2f;
    public float radius = 5f;
    public float force = 100f;
    public GameObject explosionFX;

    public AudioClip explosionSound;
    private AudioSource audioSource;

    public AudioClip fuseSound;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // fuse sound
        if (fuseSound != null)
        {
            audioSource.clip = fuseSound;
            audioSource.loop = false;
            audioSource.Play();
        }

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
                // ฟิสิกส์ทำงานเสมอ
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb == null)
                    rb = hit.gameObject.AddComponent<Rigidbody>();

                rb.isKinematic = false;
                rb.linearDamping = 0.5f;

                rb.AddExplosionForce(force, transform.position, radius, 2f, ForceMode.Impulse);

                // คะแนน (เช็คก่อน)
                Destructible d = hit.GetComponent<Destructible>();

                if (d != null && !d.hasScored)
                {
                    if (scoreManager != null)
                        scoreManager.AddScore(100);

                    d.hasScored = true; //  ล็อกไม่ให้ได้อีก
                }

                Destroy(hit.gameObject, 10f);
            }
        }
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        Destroy(gameObject);
    }
}