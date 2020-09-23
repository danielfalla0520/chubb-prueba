using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Core.Request;
using Model.Core.Response;

namespace InventoryChubb.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Llama al Bussines Core al metodo que retorna el listado de items
        /// </summary>
        /// <returns>class GetItemResponse</returns>
        [HttpGet]
        public ActionResult<GetItemResponse> getItems()
        {
            return ItemBC.getItems();
        }

        /// <summary>
        /// Llama al Bussines Core al metodo que guarda los items
        /// </summary>
        /// <param name="request"></param>
        /// <returns>class SaveItemResponse</returns>
        [HttpPost]
        public ActionResult<SaveItemResponse> saveItem(SaveItemRequest request)
        {
            return ItemBC.saveItem(request);
        }

        /// <summary>
        /// Llama al Bussines Core al metodo que retorna todo lo del inventario
        /// </summary>
        /// <returns>class GetInventoryResponse</returns>
        [HttpGet]
        public ActionResult<GetInventoryResponse> getInventory()
        {
            return InventoryBC.getInventory();
        }

        /// <summary>
        /// Llama al Bussines Core al metodo que guarda los items en el inventario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ItemInventoryResponse> saveItemInventory(ItemInventoryRequest request)
        {
            return InventoryBC.saveItemInventory(request);
        }

        /// <summary>
        /// Llama al Bussines Core al metodo que descuenta la cantidad de items del inventario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ItemInventoryResponse> discountItemInventory(ItemInventoryRequest request)
        {
            return InventoryBC.discountItemInventory(request);
        }

    }
}