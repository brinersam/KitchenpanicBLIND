using System;
using System.Collections.Generic;
using System.Linq;

public static class RecipeHashClass
{
    public static int ExtractHash<T>(IEnumerable<T> inv, Func<T,ItemInfo> fun = null, int[] itemAmountArray = null)
    {
        int cumHash = 0;

        int idx = 0;
        foreach (var i in inv)
        {
            unchecked
            {   
                int curElementHash = 0;
                if (fun == null)
                    curElementHash += i.GetHashCode(); 
                else
                    curElementHash += fun(i).GetHashCode();


                if (itemAmountArray != null)
                    curElementHash *= itemAmountArray[idx];

                cumHash += curElementHash;
            }
            idx++;
        }
        return cumHash;
    }
}