using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class AssignmentService
    {
        private readonly DatabaseContext _databaseContext;
        public AssignmentService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        // Get all assignments
        public Task<List<Assignment>> GetAssignmentsAsync()
        {
            return _databaseContext.GetAssignmentsAsync();
        }
        // Get an assignment by ID
        public Task<Assignment> GetAssignmentAsync(int id)
        {
            return _databaseContext.GetAssignmentAsync(id);
        }
        // Save an assignment
        public Task<int> SaveAssignmentAsync(Assignment assignment)
        {
            return _databaseContext.SaveAssignmentAsync(assignment);
        }
        // Delete an assignment
        public Task<int> DeleteAssignmentAsync(Assignment assignment)
        {
            return _databaseContext.DeleteAssignmentAsync(assignment);
        }
    }
}
