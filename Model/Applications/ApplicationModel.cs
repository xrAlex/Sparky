using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions.CollectionChanged;
using Common.Interfaces;

namespace Model.Applications
{
    internal partial class ApplicationModel : IApplicationModel
    {
        private readonly ApplicationsCollection.ApplicationsCollection _applications = new();
        private readonly object _eventLocker = new();

        public ApplicationModel()
        {
            _applications.CollectionChanged += CollectionChanged;
            FillApplicationsCollection();
            var asd = _applications;
        }
    }
}
