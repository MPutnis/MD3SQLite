using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<List<Submission>> GetSubmissionsAsync()
        {
            try
            {
                return await _databaseContext.GetSubmissionsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching submissions: {ex.Message}");
                throw;
            }
        }

        // Get a submission by ID
        public async Task<Submission> GetSubmissionAsync(int id)
        {
            try
            {
                return await _databaseContext.GetSubmissionAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching submission with {id}: {ex.Message}");
                throw;
            }
        }

        // Save a submission
        public async Task<int> SaveSubmissionAsync(Submission submission)
        {
            try
            {
                return await _databaseContext.SaveSubmissionAsync(submission);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving submission: {ex.Message}");
                throw;
            }
        }

        // Delete a submission
        public async Task DeleteSubmissionAsync(Submission submission)
        {
            try
            {
                await _databaseContext.DeleteSubmissionAsync(submission);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting submission: {ex.Message}");
                throw;
            }
        }
    }
}
