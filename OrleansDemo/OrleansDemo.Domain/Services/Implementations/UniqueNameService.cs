using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrleansDemo.Domain.Services.Implementations
{
    internal class UniqueNameService: IUniqueNameService
    {
        public string UniqueName { get; private set; }

        public UniqueNameService()
        {
            UniqueName = (new NameGenerator.Generators.RealNameGenerator()).Generate();
        }
    }
}
