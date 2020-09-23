using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Request
{
    public class ItemInventoryRequest
    {
        public int idItem { get; set; }
        public int quantity { get; set; }
    }
}
