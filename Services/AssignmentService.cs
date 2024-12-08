using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD3SQLite.Models;

namespace MD3SQLite.Services
{
    public class AssignmentService(DatabaseContext databaseContext)
    {
        // Assign database context
        private readonly DatabaseContext _databaseContext = databaseContext;
        
        // Get all assignments
        public async Task<List<Assignment>> GetAssignmentsAsync()
        {
            try
            {
                return await _databaseContext.GetAssignmentsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching assignments: {ex.Message}");
                throw;
            }
        }

        // Get an assignment by ID
        public async Task<Assignment> GetAssignmentAsync(int id)
        {
            try
            {
                return await _databaseContext.GetAssignmentAsync(id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching assignment with {id}: {ex.Message}");
                throw;
            }
            
        }

        // Save an assignment
        public async Task<int> SaveAssignmentAsync(Assignment assignment)
        {
            try
            {
                return await _databaseContext.SaveAssignmentAsync(assignment);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving assignment: {ex.Message}");
                throw;
            }
            
        }

        // Delete an assignment
        public async Task<int> DeleteAssignmentAsync(Assignment assignment)
        {
            try
            {
                var submissions = await _databaseContext.GetSubmissionsByAssignmentIdAsync(assignment.Id);
                if (submissions.Count > 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Cannot delete assignment with submissions", "OK");
                    throw new InvalidOperationException("Cannot delete assignment with submissions");
                }

                return await _databaseContext.DeleteAssignmentAsync(assignment);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting assignment: {ex.Message}");
                throw;
            }
            
        }

        // Get assignments by course ID
        public  async Task<List<Assignment>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            try
            {
                return await _databaseContext.GetAssignmentsByCourseIdAsync(courseId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching assignments for course {courseId}: {ex.Message}");
                throw;
            }
            
        }
    }
}
