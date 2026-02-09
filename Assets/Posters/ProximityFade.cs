using UnityEngine;

public class ProximityFade : MonoBehaviour
{
    public Transform playerCamera;      // Drag your ARCamera here
    public SpriteRenderer posterSprite; // Drag the SpriteRenderer of your poster here
    
    [Header("Distance Settings")]
    public float fullTransparentDist = 0.2f; // Distance where you see ONLY the diorama
    public float fullOpaqueDist = 0.8f;      // Distance where you see ONLY the painting

    void Update() {
        if (playerCamera == null || posterSprite == null) return;

        // Calculate the distance between the camera and the marker
        float distance = Vector3.Distance(playerCamera.position, transform.position);

        // Map the distance to an alpha value (0 to 1)
        // Inverse Lerp calculates where 'distance' sits between our two points
        float alpha = Mathf.InverseLerp(fullTransparentDist, fullOpaqueDist, distance);

        // Apply the alpha to the SpriteRenderer's color
        Color currentColor = posterSprite.color;
        currentColor.a = alpha;
        posterSprite.color = currentColor;
    }
}
