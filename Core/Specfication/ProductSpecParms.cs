using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specfication
{
    public class ProductSpecParms
    {
        public int? BrandId { get; set; }

        public int? TypetId { get; set; }
        public string? Sort { get; set; }

        private const int MAXPAGESIZE = 50;

        private int _pageSize = 6;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value; 
        }

        public int PageIndex { get; set; } = 1;
        private string? _serach;
        public string? Serach
        {
            get => _serach;
            set => _serach = value.ToLower();
        } 
    }
}
