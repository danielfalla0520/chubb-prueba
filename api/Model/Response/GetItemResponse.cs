using Model.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Response
{
    public class GetItemResponse : BaseResponse
    {
        public List<ItemMC> items { get; set; }
    }
}
