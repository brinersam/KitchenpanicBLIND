using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasItemDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject visualObj;

    public void UpdateVisuals(LimitedSizeStack<Holdable> itemStack)
    {
        foreach (Transform child in visualObj.transform) // todo this needs to sync with last stack operation so i dont redraw the entire thing each time inventory updates
        { // also add on updated from stack to IInventory so i cant forget to update it in some niche case
            Destroy(child.gameObject);
        }

        float yoffset = 0;
        foreach (var i in itemStack)
        {
            var visual = Instantiate(i.visualObj, parent:visualObj.transform);
            visual.transform.localPosition += new Vector3(0,yoffset,0);

            yoffset += 0.1f;
        }
    }
}
