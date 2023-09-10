using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Dtos;
using Task.Core.Entities;

namespace Task.Services.StudentService
{
    public interface IStudentService
    {
        Task<List<ApplicationUser>> GetAllStudents();
        Task<List<ExamStudents>> GetStudentExams(int userId);
        Task<ApplicationUser?> GetUserByName(string userName);
        Task<int> AssignStudentToExam(List<int> students, int examId);

        Task<ExamStudents> SubmitExamToStudent(List<ExamAnswersDto> examAnswersDtos, int examId);
    }
}
