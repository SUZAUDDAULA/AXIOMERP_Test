using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Data.Entities.MasterData
{
    public class Company:Base
    {
        public string companyName { get; set; }
        public string companyNameBn { get; set; }
        public string contactPerson { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string address { get; set; }
    }
}
