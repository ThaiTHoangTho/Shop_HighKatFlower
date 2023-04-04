using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop_HighKatFlower.Models.EF
{
    public abstract class CommonAbstract
    {
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Modifiedby { get; set; }
    }
}