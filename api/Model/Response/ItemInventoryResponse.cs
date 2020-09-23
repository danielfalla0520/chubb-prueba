using Model.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Response
{
    public class ItemInventoryResponse : BaseResponse
    {
        public List<InventoryMC> inventory { get; set; }
    }
}
