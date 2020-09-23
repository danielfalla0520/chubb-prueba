using Data.Core.Connect;
using Model.Core.Entities;
using Model.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Core.Class
{
    public class ItemDC
    {
        /// <summary>
        /// Conecta a la bd y ejecuta el sp de la bd que trae todos los items
        /// </summary>
        /// <returns></returns>
        public static List<ItemMC> getItems()
        {
            try
            {
                var dataservice = new DataModelService();
                List<ItemMC> items = dataservice.GetListModel<ItemMC>("[dbo].[getItems]");

                return items;
            }
            catch (Exception ex)
            {
                List<ItemMC> items = new List<ItemMC>();
                ItemMC item = new ItemMC();

                items.Add(item);

                return items;
            }
           

        }

        /// <summary>
        /// Conecta a la bd y ejecuta el sp de para la insrcion del item
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<ItemMC> saveItem(SaveItemRequest request)
        {
            try
            {
                var dataservice = new DataModelService();
                List<ItemMC> item = dataservice.GetListByParameter<ItemMC, SaveItemRequest>("[dbo].[saveItem]", request);

                return item;
            }
            catch (Exception ex)
            {
                List<ItemMC> items = new List<ItemMC>();
                ItemMC item = new ItemMC();

                items.Add(item);

                return items;
            }
        }
    }
}
