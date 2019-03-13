using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skeeetch.Logic
{
    public class SortFactory
    {
        public SortBase Create(int count)
        {
            if (count <=5 )
            {
                return new SmallSort();
            }

            else if (count > 5 && count < 12)
            {
                return new MediumSort();
            }

            else
            {
                return new LargeSort();
            }
        }
    }
}