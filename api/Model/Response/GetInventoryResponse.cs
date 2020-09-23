using Model.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Response
{
    public class GetInventoryResponse : BaseResponse
    {
        public List<GInventoryMC> inventories { get; set; }
    }
}
