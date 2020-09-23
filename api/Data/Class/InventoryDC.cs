using Data.Core.Connect;
using Model.Core.Entities;
using Model.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Core.Class
{
    public class InventoryDC
    {
        /// <summary>
        /// Conecta a la bd y ejecuta el sp que trae todos los items en el inventario
        /// </summary>
        /// <returns></returns>
        public static List<GInventoryMC> getInventory()
        {
            try
            {
                var dataservice = new DataModelService();
                List<GInventoryMC> inventories = dataservice.GetListModel<GInventoryMC>("[dbo].[getInventory]");

                return inventories;
            }
            catch(Exception ex)
            {
                List<GInventoryMC> inventories = new List<GInventoryMC>();
                GInventoryMC inventory = new GInventoryMC();
                inventory.item = ex.Message;
                inventories.Add(inventory);

                return inventories;
            }
            
        }

        /// <summary>
        /// Conecta a la bd y ejecuta el sp de para la insercion del item en el inventario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<InventoryMC> saveItemInventory(ItemInventoryRequest request)
        {
            try
            {
                var dataservice = new DataModelService();
                List<InventoryMC> inventory = dataservice.GetListByParameter<InventoryMC, ItemInventoryRequest>("[dbo].[saveItemInventory]", request);

                return inventory;
            }
            catch (Exception ex)
            {

                List<InventoryMC> inventories = new List<InventoryMC>();
                InventoryMC inventory = new InventoryMC();
                inventory.item = ex.Message;
                inventories.Add(inventory);

                return inventories;
            }
           
          

        }

        /// <summary>
        /// Conecta a la bd y ejecuta el sp de para el descuento del item en el inventario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<InventoryMC> discountItemInventory(ItemInventoryRequest request)
        {
            try
            {
                var dataservice = new DataModelService();
                List<InventoryMC> inventory = dataservice.GetListByParameter<InventoryMC, ItemInventoryRequest>("[dbo].[discountItemInventory]", request);

                return inventory;
            }
            catch (Exception ex)
            {
                List<InventoryMC> inventories = new List<InventoryMC>();
                InventoryMC inventory = new InventoryMC();
                inventory.item = ex.Message;
                inventories.Add(inventory);

                return inventories;
            }
           

        }
    }
}
