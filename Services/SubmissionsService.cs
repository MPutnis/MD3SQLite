using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class SubmissionsService(DatabaseContext databaseContext)
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        // Get all submissions
        public Task<List<Submissions>> GetSubmissionsAsync()
        {
            return _databaseContext.GetSubmissionsAsync();
        }

        // Get a submission by ID
        public Task<Submissions> GetSubmissionAsync(int id)
        {
            return _databaseContext.GetSubmissionAsync(id);
        }

        // Save a submission
        public Task<int> SaveSubmissionAsync(Submissions submission)
        {
            return _databaseContext.SaveSubmissionAsync(submission);
        }
        // Delete a submission
        public Task DeleteSubmissionAsync(Submissions submission)
        {
            return _databaseContext.DeleteSubmissionAsync(submission);
        }
    }
}
