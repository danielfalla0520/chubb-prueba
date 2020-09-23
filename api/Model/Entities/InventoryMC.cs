using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Entities
{
    public class InventoryMC
    {
        public string item { get; set; }
        public int numItem { get; set; } 
        public int quantity { get; set; }
        public int withdrawal { get; set; }
        public string resp { get; set; }
    }
}
