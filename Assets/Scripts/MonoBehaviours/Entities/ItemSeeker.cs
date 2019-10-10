using UnityEngine;
using UnityEngine.Events;

public class ItemSeeker : MonoBehaviour
{
    [SerializeField]
    private LayerMask acceptLayers;

    public event UnityAction<RequestItem> OnCrosshairEnterItem;
    public event UnityAction<RequestItem> OnCrosshairExitItem;

    public bool IsCrosshairOnItem { get; private set; } = false;
    public RequestItem ItemAimedAt { get; private set; } = null;

    private void Update()
    {
        RaycastAtCrosshair();
        TryGrabOrRelease();
    }

    private void RaycastAtCrosshair()
    {
        Ray r = new Ray(transform.position, transform.forward);
        RaycastHit rh;

        LayerMask lm = ~(~acceptLayers.value);
        
        if (Physics.Raycast(r, out rh, float.PositiveInfinity, lm))
        {
            if (!IsCrosshairOnItem)
            {
                ItemAimedAt = rh.collider.GetComponentInParent<RequestItem>();

                if (ItemAimedAt == null) ItemAimedAt = rh.collider.GetComponentInChildren<RequestItem>();

                IsCrosshairOnItem = true;
                OnCrosshairEnterItem?.Invoke(ItemAimedAt);
            }
            
        }
        else
        {
            if (IsCrosshairOnItem)
            {
                OnCrosshairExitItem?.Invoke(ItemAimedAt);
                ItemAimedAt = null;
                IsCrosshairOnItem = false;
            }
        }
    }

    private void TryGrabOrRelease()
    {
        if (IsCrosshairOnItem)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.PlayerHand.GrabItem(ItemAimedAt);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.PlayerHand.ReleaseItem();
            }
        }
    }
}