using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Services
{
    public class ServiceDescription
    {
        public Type ServiceType { get; private set; }
        public IEnumerable<ContractDescription> Contracts { get; private set; }
        public IEnumerable<OperationDescription> Operations => Contracts.SelectMany(c => c.Operations);

        public ServiceDescription(Type serviceType)
        {
            ServiceType = serviceType;
            var contracts = new List<ContractDescription>();
            foreach (var contractType in ServiceType.GetInterfaces())
            {
                foreach (var serviceContract in contractType.GetTypeInfo().GetCustomerAttributes<ServiceContractAttribute>())
                {
                    contracts.Add(new ContractDescription(this, contractType, serviceContract));
                }
            }
            Contracts = contracts;
        }
    }
}
