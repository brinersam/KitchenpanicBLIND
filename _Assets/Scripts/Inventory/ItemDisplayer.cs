using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayer : MonoBehaviour
{
    [SerializeField] private GameObject visualObj;

    const int MAX_DRAW_DEPTH = 1;
    const float ITEM_UI_HEIGHT_MOD = .8f;

    public void UpdateVisuals(Inventory inv) // todo this needs to sync with last stack operation so i dont redraw the entire thing each time inventory updates
    {// also add on updated from stack to IInventory so i cant forget to update it in some niche case
        CleanUp();
        DrawInventory(inv);
    }

    private void CleanUp()
    {
        foreach (Transform child in visualObj.transform) 
        { 
            Destroy(child.gameObject);
        }
    }

    private float DrawInventory(Inventory inv ,float yoffset = 0, int drawDepth = 0)
    {
        if (drawDepth > MAX_DRAW_DEPTH) return 0;

        foreach (var i in inv)
        {
            var visual = Instantiate(i.Info.VisualObj, parent:visualObj.transform);
            visual.transform.localPosition += new Vector3(0,yoffset,0);
            yoffset += 0.1f;

            //if (i.Info.invCapacity > 0)
            if (i.Inventory is null == false)
            {
                if (drawDepth == 0 && i.Info.uiObj != null)
                {
                    DrawSubInvUI(i);
                }

                yoffset = DrawInventory(i.Inventory, yoffset, drawDepth + 1);
            }
        }
        return yoffset;
    }

    private void DrawSubInvUI(Item item)
    {
        if (item.Inventory.IsEmpty) return;

        var guiObj = Instantiate(item.Info.uiObj, parent:visualObj.transform);
        guiObj.transform.localPosition += Vector3.up * ITEM_UI_HEIGHT_MOD; 

        if (guiObj.TryGetComponent(out InvGUI itemGUI) == false) return;
        itemGUI.UpdateVisuals(item.Inventory);
    }
}
