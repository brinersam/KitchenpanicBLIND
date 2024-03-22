using System;
using System.Collections.Generic;
using System.Linq;

public static class RecipeHashClass
{
    public static int ExtractHash(IEnumerable<ItemInfo> inv, int[] itemAmountArray = null)
    {
        int cumHash = 0;

        int idx = 0;
        foreach (var i in inv)
        {
            unchecked
            {   
                int curElementHash = 0;

                curElementHash += i.GetHashCode(); 

                if (itemAmountArray != null)
                    curElementHash *= itemAmountArray[idx];

                cumHash += curElementHash;
            }
            idx++;
        }
        return cumHash;
    }
}