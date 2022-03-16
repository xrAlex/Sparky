using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions.CollectionChanged;

namespace Common.Interfaces
{
    public interface IApplicationModel
    {
        void MoveToIgnored(List<string> applicationsName);
        void RefreshApplications();
        List<string> GetAllNames();
        event EventHandler<ApplicationCollectionChangedArgs>? ApplicationCollectionChanged;
    }
}
