using Data.Core.Class;
using Model.Core.Request;
using Model.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Core
{
    public class InventoryBC
    {
        /// <summary>
        /// Llama al Data Core para el retorno del listado de los que se encuentra en el inventario, arma la entidad (json) que va a retornar
        /// </summary>
        /// <returns></returns>
        public static GetInventoryResponse getInventory()
        {
            GetInventoryResponse response = new GetInventoryResponse();

            response.inventories = InventoryDC.getInventory();

            response.code = 100;
            response.message = "OK";

            return response;
        }

        /// <summary>
        /// Llama al Data Core para el retorno del item que se esta guardando en el inventario, arma la entidad (json) con la informacion que va a retornar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ItemInventoryResponse saveItemInventory(ItemInventoryRequest request)
        {
            ItemInventoryResponse response = new ItemInventoryResponse();

            response.inventory = InventoryDC.saveItemInventory(request);

            response.code = 100;
            response.message = "OK";

            return response;
        }

        /// <summary>
        /// Llama al Data Core para el retorno del item que se esta descontando en el inventario, arma la entidad (json) con la informacion que va a retornar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ItemInventoryResponse discountItemInventory(ItemInventoryRequest request)
        {
            ItemInventoryResponse response = new ItemInventoryResponse();

            response.inventory = InventoryDC.discountItemInventory(request);

            response.code = 100;
            response.message = "OK";

            return response;
        }
    }
}
