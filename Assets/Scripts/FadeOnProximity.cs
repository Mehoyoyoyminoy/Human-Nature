using UnityEngine;
using System.Collections;

public class FadeOnProximity : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duration of the fade effect in seconds
    private Material material;
    private Color startColor;
    private bool isFading = false;
    public string playerTag = "Player"; // Tag used to identify the player GameObject

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material;
            startColor = material.color;
        }
        else
        {
            Debug.LogError("Renderer not found. A Renderer with a transparent-capable material is required.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isFading && other.CompareTag(playerTag))
        {
            StartCoroutine(FadeOutRoutine());
        }
    }

    IEnumerator FadeOutRoutine()
    {
        isFading = true;
        float elapsed = 0.0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1.0f, 0.0f, elapsed / fadeDuration);
            material.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        gameObject.SetActive(false); // Or Destroy(gameObject) to remove it completely
    }
}
