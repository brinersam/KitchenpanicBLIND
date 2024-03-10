using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField]
    private GameObject visualObj;

    public void UpdateVisuals(Inventory itemStack)
    {
        foreach (Transform child in visualObj.transform) // todo this needs to sync with last stack operation so i dont redraw the entire thing each time inventory updates
        { // also add on updated from stack to IInventory so i cant forget to update it in some niche case
            Destroy(child.gameObject);
        }

        float yoffset = 0;
        foreach (var i in itemStack)
        {
            var visual = Instantiate(i.Info.visualObj, parent:visualObj.transform);
            visual.transform.localPosition += new Vector3(0,yoffset,0);
            yoffset += 0.1f;

            if (i.Info.type == ItemType.Container) // todo
            {
                var ySubOffset = 0f;
                foreach (var j in i.Inventory)
                {
                    var subVisual = Instantiate(j.Info.visualObj, parent:visual.transform);
                    subVisual.transform.localPosition += new Vector3(0,ySubOffset,0);
                    ySubOffset+= 0.1f;
                }
            }
        }
    }
}
