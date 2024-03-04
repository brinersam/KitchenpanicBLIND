using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject visualObj;

    public void UpdateVisuals(IHoldable holdable)
    {
        if (visualObj.transform.childCount > 0)
            Destroy(visualObj.transform.GetChild(0).gameObject);

        if (holdable != null)
        {
            Instantiate(holdable.visualObj, parent:visualObj.transform);
        }
    }
}
