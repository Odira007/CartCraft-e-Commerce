using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Common.Helpers
{
    public class Utils
    {
        public static int Max { get; set; } = 10;
        public static List<T> Paginate<T>(List<T> data, ref int pageSize, ref int pageNumber)
        {
            if (pageSize > Max || pageSize == 0) pageSize = Max;
            if (pageNumber == 0) pageNumber = 1;

            return data.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
    }
}
