using DigiX.Web.Global.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiX.Web.Api.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public IDeals Deals { get; set; }
    }
}