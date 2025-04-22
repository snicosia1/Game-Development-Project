using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 1f;
    [SerializeField] private float jumpInterval = 2f;

    private Vector3 startPosition;
    private float jumpTimer;
    private bool isJumping;

    private void Start()
    {
        startPosition = transform.position;
        jumpTimer = jumpInterval;
    }

    private void Update()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0f && !isJumping)
        {
            StartCoroutine(Jump());
            jumpTimer = jumpInterval;
        }
    }

    private System.Collections.IEnumerator Jump()
    {
        isJumping = true;
        float timeElapsed = 0f;
        Vector3 peakPosition = startPosition + Vector3.up * jumpHeight;

        while (timeElapsed < jumpDuration)
        {
            float t = timeElapsed / jumpDuration;
            float height = Mathf.Sin(t * Mathf.PI);
            transform.position = Vector3.Lerp(startPosition, peakPosition, height);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPosition;
        isJumping = false;
    }
}