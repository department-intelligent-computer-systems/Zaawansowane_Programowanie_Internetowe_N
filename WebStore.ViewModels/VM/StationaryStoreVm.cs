using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels.VM;

namespace WebStore.Model
{
    public class StationaryStoreVm
    {
        public string Name {get;set;}
        public IList<AddressVm> Address {get;set;}      
    }
}