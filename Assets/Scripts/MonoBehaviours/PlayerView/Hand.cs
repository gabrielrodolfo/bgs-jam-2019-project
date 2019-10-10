using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private GameObject freeHand;
    [SerializeField]
    private GameObject grabbingHand;
    [SerializeField]
    private Transform holdingBone;
    [SerializeField]
    private Transform throwingPoint;
    [SerializeField]
    private int itemLayerNumber;
    [SerializeField]
    private int playerViewLayerNumber;

    private RequestItem holdingItem;

    public bool IsGrabbing { get => grabbingHand.activeInHierarchy; }
    public RequestItem HoldingItem
    {
        get => holdingItem;
        private set 
        {
            if (value == null)
            {
                grabbingHand.SetActive(false);
                freeHand.SetActive(true);
            }
            else
            {
                grabbingHand.SetActive(true);
                freeHand.SetActive(false);
            }

            holdingItem = value;
        }
    }

    public bool GrabItem(RequestItem item)
    {
        if (HoldingItem != null) return false;

        HoldingItem = item;
        HoldingItem.transform.SetParent(holdingBone);
        HoldingItem.transform.localPosition = Vector3.zero;
        HoldingItem.Rigidbody.isKinematic = true;
        RecursiveSetLayer(HoldingItem.transform, playerViewLayerNumber);

        return true;
    }

    public bool ReleaseItem()
    {
        if (HoldingItem != null)
        {
            HoldingItem.transform.SetParent(null);
            HoldingItem.transform.position = throwingPoint.position;
            HoldingItem.GetComponent<Rigidbody>().AddForce(throwingPoint.forward * 3f);
            HoldingItem.Rigidbody.isKinematic = false;
            RecursiveSetLayer(HoldingItem.transform, itemLayerNumber);

            HoldingItem = null;

            return true;
        }

        return false;
    }

    private void Start()
    {
        if (HoldingItem == null)
        {
            grabbingHand.SetActive(false);
            freeHand.SetActive(true);
        }
        else
        {
            grabbingHand.SetActive(true);
            freeHand.SetActive(false);
        }
    }

    private void Update()
    {
        if (HoldingItem == null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                grabbingHand.SetActive(true);
                freeHand.SetActive(false);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                grabbingHand.SetActive(false);
                freeHand.SetActive(true);
            }
        }
    }

    private void RecursiveSetLayer(Transform point, int layerNumber)
    {
        foreach (Transform child in point)
        {
            RecursiveSetLayer(child, layerNumber);
        }
        point.gameObject.layer = layerNumber;
    }

}