using UnityEngine;

public class CubeColorTrigger : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] private Color originalColor = Color.white;
    [SerializeField] private Color triggerColor = Color.cyan;
    [SerializeField] private float transitionSpeed = 2f;

    [Header("Components")]
    [SerializeField] private Renderer cubeRenderer1;
    [SerializeField] private Renderer cubeRenderer2;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip enterSound;

    [Header("Manual Detection (if physics detection fails)")]
    [SerializeField] private bool useManualDetection = false;
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask playerLayer = 1; // Default layer

    private Material cubeMaterial1;
    private Material cubeMaterial2;
    private bool playerInside = false;
    private bool isTransitioning = false;
    private Color targetColor;
    private Color currentColor;
    private Transform playerTransform;

    // Audio control variables
    private AudioSource audioSource;

    void Start()
    {
        // Get the first renderer if not assigned
        if (cubeRenderer1 == null)
        {
            cubeRenderer1 = GetComponent<Renderer>();
        }

        if (cubeRenderer1 == null)
        {
            Debug.LogError("No Renderer found on " + gameObject.name);
            return;
        }

        // Get materials and set initial colors
        cubeMaterial1 = cubeRenderer1.material;

        // Handle second renderer if assigned
        if (cubeRenderer2 != null)
        {
            cubeMaterial2 = cubeRenderer2.material;
        }

        currentColor = originalColor;
        targetColor = originalColor;

        // Set initial colors for both materials
        cubeMaterial1.color = originalColor;
        if (cubeMaterial2 != null)
        {
            cubeMaterial2.color = originalColor;
        }

        // Setup audio source
        audioSource = GetComponent<AudioSource>();

        // Create audio source if none exists
        if (audioSource == null && enterSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            Debug.Log("Created AudioSource component on " + gameObject.name);
        }

        // Make sure we have a trigger collider
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("No Collider found on " + gameObject.name + ". Please add a Box Collider and set it as trigger.");
        }
    }

    void Update()
    {
        // Manual detection fallback
        if (useManualDetection)
        {
            ManualPlayerDetection();
        }

        // Handle color lerping
        if (isTransitioning && cubeMaterial1 != null)
        {
            currentColor = Color.Lerp(currentColor, targetColor, transitionSpeed * Time.deltaTime);

            // Apply color to both materials
            cubeMaterial1.color = currentColor;
            if (cubeMaterial2 != null)
            {
                cubeMaterial2.color = currentColor;
            }

            // Check if we've reached the target (close enough)
            if (Vector4.Distance(currentColor, targetColor) < 0.01f)
            {
                currentColor = targetColor;
                cubeMaterial1.color = targetColor;
                if (cubeMaterial2 != null)
                {
                    cubeMaterial2.color = targetColor;
                }
                isTransitioning = false;
            }
        }

        // Check if enter audio finished playing
        if (audioSource != null && audioSource.isPlaying && audioSource.clip == enterSound)
        {
            // Audio is still playing
        }
    }

    private void PlayEnterAudio()
    {
        if (audioSource == null || enterSound == null) return;

        audioSource.PlayOneShot(enterSound);
        Debug.Log("Playing enter audio");
    }

    private void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Stopped audio on exit");
        }
    }

    private void ManualPlayerDetection()
    {
        // Find player if not cached
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerTransform = player.transform;
        }

        if (playerTransform == null) return;

        // Check distance
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool wasInside = playerInside;
        playerInside = distance <= detectionRadius;

        // State changed
        if (playerInside && !wasInside)
        {
            Debug.Log("Player entered cube area (manual detection)");
            targetColor = triggerColor;
            isTransitioning = true;
            PlayEnterAudio();
        }
        else if (!playerInside && wasInside)
        {
            Debug.Log("Player left cube area (manual detection)");
            targetColor = originalColor;
            isTransitioning = true;
            StopAudio();
        }
    }

    // Option A: Use these if you have Rigidbody on cube or kinematic rigidbody on player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered cube trigger");
            playerInside = true;
            targetColor = triggerColor;
            isTransitioning = true;
            PlayEnterAudio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited cube trigger");
            playerInside = false;
            targetColor = originalColor;
            isTransitioning = true;
            StopAudio();
        }
    }

    // Option B: Use these if you want collision detection instead (disable trigger checkbox)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player touched cube");
            playerInside = true;
            targetColor = triggerColor;
            isTransitioning = true;
            PlayEnterAudio();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left cube");
            playerInside = false;
            targetColor = originalColor;
            isTransitioning = true;
            StopAudio();
        }
    }

    // Helper method to set colors from inspector or other scripts
    public void SetColors(Color original, Color trigger)
    {
        originalColor = original;
        triggerColor = trigger;

        if (!playerInside)
        {
            targetColor = originalColor;
            if (cubeMaterial1 != null)
            {
                currentColor = originalColor;
                cubeMaterial1.color = originalColor;
            }
            if (cubeMaterial2 != null)
            {
                cubeMaterial2.color = originalColor;
            }
        }
    }

    // Method to instantly change to trigger color (useful for testing)
    [ContextMenu("Test Trigger Color")]
    public void TestTriggerColor()
    {
        if (cubeMaterial1 != null)
        {
            cubeMaterial1.color = triggerColor;
        }
        if (cubeMaterial2 != null)
        {
            cubeMaterial2.color = triggerColor;
        }
    }

    // Method to instantly change back to original color
    [ContextMenu("Test Original Color")]
    public void TestOriginalColor()
    {
        if (cubeMaterial1 != null)
        {
            cubeMaterial1.color = originalColor;
        }
        if (cubeMaterial2 != null)
        {
            cubeMaterial2.color = originalColor;
        }
    }

    private void OnDestroy()
    {
        // Clean up materials if we created copies
        if (cubeMaterial1 != null && Application.isPlaying)
        {
            Destroy(cubeMaterial1);
        }
        if (cubeMaterial2 != null && Application.isPlaying)
        {
            Destroy(cubeMaterial2);
        }
    }
}