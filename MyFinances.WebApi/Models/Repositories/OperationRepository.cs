using MyFinances.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.WebApi.Models.Repositories
{
    public class OperationRepository
    {
        private readonly MyFinancesContext _context;

        public OperationRepository(MyFinancesContext context)
        {
            _context = context;
        }
        public IEnumerable<Operation> Get()
        {
            return _context.Operations.ToList();
        }
        public IEnumerable<Operation> Get(int numberOfRecords, int pageNumber = 1)
        {
            var listOfOperations = _context.Operations.Skip(numberOfRecords * (pageNumber - 1)).ToList();
            return listOfOperations.Take(numberOfRecords);
        }

        public Operation Get(int id)
        {
            return _context.Operations.FirstOrDefault(x => x.Id == id);
        }
        public void Add(Operation operation)
        {
            operation.Date = DateTime.Now;
            _context.Operations.Add(operation);
        }

        public void Update(Operation operation)
        {
            var operationToUpdate = _context.Operations.First(x => x.Id == operation.Id);

            operationToUpdate.CategoryId = operation.CategoryId;
            operationToUpdate.Description = operation.Description;
            operationToUpdate.Name = operation.Name;
            operationToUpdate.Value = operation.Value;

            _context.Operations.Add(operation);
        }
        public void Delete(int id)
        {
            var operationToDelete = _context.Operations.First(x => x.Id == id);
            _context.Operations.Remove(operationToDelete);
        }

    }
}
