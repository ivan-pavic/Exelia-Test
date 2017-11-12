using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Test
{
    public static class BillModelHandler
    {
        private static BillModel billModel = new BillModel();

        public static BillModel BillModel
        {
            get
            {
                return billModel;
            }
        }
    }
}
