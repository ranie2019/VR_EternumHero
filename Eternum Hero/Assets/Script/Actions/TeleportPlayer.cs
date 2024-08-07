using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Manually teleports the player to a specific anchor.
/// </summary>
public class TeleportPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("The anchor the player is teleported to.")]
    private TeleportationAnchor anchor;

    [SerializeField, Tooltip("The provider used to request the teleportation.")]
    private TeleportationProvider provider;

    /// <summary>
    /// Ensures the anchor and provider are properly assigned.
    /// </summary>
    private void Awake()
    {
        if (anchor == null)
        {
            Debug.LogError("Teleportation failed: Anchor is not assigned.");
        }

        if (provider == null)
        {
            Debug.LogError("Teleportation failed: Provider is not assigned.");
        }
    }

    /// <summary>
    /// Initiates the teleportation process.
    /// </summary>
    public void Teleport()
    {
        if (anchor == null || provider == null)
        {
            Debug.LogWarning("Teleportation aborted: Anchor or Provider is not assigned.");
            return;
        }

        if (anchor.teleportAnchorTransform == null)
        {
            Debug.LogError("Teleportation failed: Anchor's transform is not assigned.");
            return;
        }

        TeleportRequest request = CreateRequest();
        provider.QueueTeleportRequest(request);
    }

    /// <summary>
    /// Creates a teleport request based on the anchor's transform.
    /// </summary>
    /// <returns>A new TeleportRequest.</returns>
    private TeleportRequest CreateRequest()
    {
        Transform anchorTransform = anchor.teleportAnchorTransform;

        TeleportRequest request = new TeleportRequest()
        {
            requestTime = Time.time,
            matchOrientation = anchor.matchOrientation,
            destinationPosition = anchorTransform.position,
            destinationRotation = anchorTransform.rotation
        };

        return request;
    }

    /// <summary>
    /// Allows setting the anchor from outside the class.
    /// </summary>
    /// <param name="newAnchor">The new teleportation anchor.</param>
    public void SetAnchor(TeleportationAnchor newAnchor)
    {
        anchor = newAnchor;
        if (anchor == null)
        {
            Debug.LogError("Teleportation failed: Anchor is not assigned.");
        }
    }

    /// <summary>
    /// Allows setting the provider from outside the class.
    /// </summary>
    /// <param name="newProvider">The new teleportation provider.</param>
    public void SetProvider(TeleportationProvider newProvider)
    {
        provider = newProvider;
        if (provider == null)
        {
            Debug.LogError("Teleportation failed: Provider is not assigned.");
        }
    }
}
