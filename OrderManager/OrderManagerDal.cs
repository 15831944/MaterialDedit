using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public class OrderManagerDal
    {
        private static readonly object objLock = new object();
        private static OrderManagerDal _Instance;
        public static OrderManagerDal Instance
        {
            get
            {
                lock(objLock)
                {
                    if(_Instance == null)
                    {
                        lock (objLock)
                            _Instance = new OrderManagerDal();
                    }

                    return _Instance;
                }
            }
        }

        private Dictionary<long, string> dictOrder;
        public OrderManagerDal()
        {
            dictOrder = new Dictionary<long, string>();
        }


        public void AddOrder(long id, string order)
        {
            if (dictOrder.ContainsKey(id) == true)
                dictOrder.Remove(id);

            dictOrder.Add(id, order);
        }

        public void RemoveOrder(long id)
        {
            dictOrder.Remove(id);
        }

        public string GetOrder(long id)
        {
            if (dictOrder.ContainsKey(id) == false)
                return string.Empty;

            return dictOrder[id];
        }
    }
}
