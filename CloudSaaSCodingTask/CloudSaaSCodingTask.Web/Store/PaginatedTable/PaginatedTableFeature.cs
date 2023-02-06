using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSaaSCodingTask.Store.PaginatedTable
{
    public class PaginatedTableFeature : Feature<PaginatedTableState>
    {
        public override string GetName() => nameof(PaginatedTableFeature);

        protected override PaginatedTableState GetInitialState() => new();
    }
}
