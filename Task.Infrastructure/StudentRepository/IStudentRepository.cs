using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.Infrastructure.StudentRepository
{
    public interface IStudentRepository
    {
        Task<List<ApplicationUser>> GetAllStudents();
        Task<ApplicationUser?> GetUserByName(string userName);
        Task<List<ExamStudents>> GetUserExams(int userId);
        Task<ApplicationUser?> GetUserById(int Id);
        Task<int> AddStudentsToExam(List<ExamStudents> examStudents);
    }
}
