using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject visualObj;

    private GameObject itemSpecificUi;
    bool initScheduled;
    
    const int MAX_DRAW_DEPTH = 1;

    public void UpdateVisuals(Inventory inv) // todo this needs to sync with last stack operation so i dont redraw the entire thing each time inventory updates
    {// also add on updated from stack to IInventory so i cant forget to update it in some niche case
        initScheduled = false;
        CleanUp();
        DrawInventory(inv);
        CleanUpUI();
    }

    private void DrawInventory(Inventory inv ,float yoffset = 0, int drawDepth = 0)
    {
        if (drawDepth > MAX_DRAW_DEPTH)
            return;

        foreach (var i in inv)
        {
            var visual = Instantiate(i.Info.visualObj, parent:visualObj.transform);
            visual.transform.localPosition += new Vector3(0,yoffset,0);
            yoffset += 0.1f;

            if (i.Info.invCapacity > 0)
            {
                initScheduled = true;
                DrawInventory(i.Inventory, yoffset, drawDepth + 1);
            }
        }

        if (drawDepth == 0 &&
            itemSpecificUi == null &&
            initScheduled == true
            )
        {
            itemSpecificUi = Instantiate(RuntimeUImngmt.ReceivePlateUi(), parent:visualObj.transform);
            itemSpecificUi.transform.localPosition += Vector3.up * 1; 
        }
    }

    private void CleanUp()
    {
        foreach (Transform child in visualObj.transform) 
        { 
            Destroy(child.gameObject);
        }
    }

    private void CleanUpUI() // if we put this before drawInv (aka put it in cleanup), it will be destroyed when not needed
    {
        if (initScheduled == false && itemSpecificUi != null)
        {
            Destroy(itemSpecificUi);
            Debug.Log("ui marked for destruction");
        }
    }
}
