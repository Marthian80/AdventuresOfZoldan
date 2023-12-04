using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;

    private Camera mainCamera;
    private Vector2 startPosition;
    private Vector2 travel => (Vector2)mainCamera.transform.position - startPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPosition + travel * parallaxOffset;
    }
}
