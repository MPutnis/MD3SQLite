using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class SubmissionService(DatabaseContext databaseContext)
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        // Get all submissions
        public Task<List<Submission>> GetSubmissionsAsync()
        {
            return _databaseContext.GetSubmissionsAsync();
        }

        // Get a submission by ID
        public Task<Submission> GetSubmissionAsync(int id)
        {
            return _databaseContext.GetSubmissionAsync(id);
        }

        // Save a submission
        public Task<int> SaveSubmissionAsync(Submission submission)
        {
            return _databaseContext.SaveSubmissionAsync(submission);
        }
        // Delete a submission
        public Task DeleteSubmissionAsync(Submission submission)
        {
            return _databaseContext.DeleteSubmissionAsync(submission);
        }
    }
}
