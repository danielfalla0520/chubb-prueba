using Model.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Response
{
    public class SaveItemResponse : BaseResponse
    {
        public List<ItemMC> item { get; set; }
    }
}
