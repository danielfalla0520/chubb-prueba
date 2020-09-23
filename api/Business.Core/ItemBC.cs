using Data.Core.Class;
using Model.Core.Request;
using Model.Core.Response;
using System;

namespace Business.Core
{
    public class ItemBC
    {
        /// <summary>
        /// Llama al Data Core para el retorno del listado de los items, arma la entidad (json) que va a retornar
        /// </summary>
        /// <returns></returns>
        public static GetItemResponse getItems()
        {
            GetItemResponse response = new GetItemResponse();

            response.items =ItemDC.getItems();

            response.code = 100;
            response.message = "OK";

            return response;
        }

        /// <summary>
        /// Llama al Data Core para el retorno del item que se esta creando, arma la entidad (json) con la informacion que va a retornar
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SaveItemResponse saveItem(SaveItemRequest request)
        {
            SaveItemResponse response = new SaveItemResponse();

            response.item = ItemDC.saveItem(request);

            response.code = 100;
            response.message = "OK";

            return response;
        }
    }
}
