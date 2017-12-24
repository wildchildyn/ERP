using System.Collections.Generic;

using ERP.Models;
using ERP.Utils;

namespace ERP
{
    public interface IDataAccessor
    {
        IList<T> Get<T>(IList<Condition> conditions) where T : ERPData;

        // return number of updated objects
        int Post<T>(IList<T> data) where T : ERPData;

        // return number of deleted objects
        int Delete<T>(IList<T> data) where T : ERPData;
    }
}
