using Skeeetch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeeetch.Logic
{
    public interface ISort
    {
        List<Business> BoringSort(List<Business> businesses);
        List<Business> FunSort(List<Business> businesses);
        List<Business> ExcitingSort(List<Business> businesses);
        List<Business> SketchSort(List<Business> businesses);
        List<Business> MiddleSort(List<Business> businesses);
    }
}
