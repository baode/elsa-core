using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flowsharp.Persistence.Models;
using Flowsharp.Persistence.Specifications;

namespace Flowsharp.Persistence.InMemory
{
    public class InMemoryWorkflowDefinitionStore : IWorkflowDefinitionStore
    {
        public InMemoryWorkflowDefinitionStore()
        {
            WorkflowDefinitions = new List<WorkflowDefinition>();
        }

        public IList<WorkflowDefinition> WorkflowDefinitions { get; }
        
        public Task<IEnumerable<WorkflowDefinition>> GetManyAsync(ISpecification<WorkflowDefinition, IWorkflowDefinitionSpecificationVisitor> specification, CancellationToken cancellationToken)
        {
            var query = WorkflowDefinitions.AsQueryable().Where(x => specification.IsSatisfiedBy(x));
            var matches = query.Distinct().ToList();
            return Task.FromResult<IEnumerable<WorkflowDefinition>>(matches);
        }

        public Task AddAsync(WorkflowDefinition value, CancellationToken cancellationToken)
        {
            WorkflowDefinitions.Add(value);
            return Task.CompletedTask;
        }
    }
}