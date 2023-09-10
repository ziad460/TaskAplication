using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Dtos;
using Task.Core.Entities;
using Task.Infrastructure.ExamRepository;
using Task.Infrastructure.StudentRepository;
using Task.Infrastructure.UserRepository;
using Task.Services.UserService;

namespace Task.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExamRepository _examRepository;

        public StudentService(IStudentRepository studentRepository ,IUserRepository userRepository, IExamRepository examRepository)
        {
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _examRepository = examRepository;
        }

        public async Task<List<ApplicationUser>> GetAllStudents()
        {
            return await _studentRepository.GetAllStudents();
        }

        public async Task<List<ExamStudents>> GetStudentExams(int userId)
        {
            return await _studentRepository.GetUserExams(userId);
        }

        public async Task<ApplicationUser?> GetUserByName(string userName)
        {
            return await _studentRepository.GetUserByName(userName);
        }

        public async Task<int> AssignStudentToExam(List<int> students , int examId)
        {
            List<ExamStudents> examStudents = new List<ExamStudents>();
            foreach (var item in students)
            {
                examStudents.Add(new ExamStudents { ExamId = examId , CreatedDate = DateTime.Now , StudentId = item });
            }
            return await _studentRepository.AddStudentsToExam(examStudents);
        }

        public async Task<ExamStudents> SubmitExamToStudent(List<ExamAnswersDto> examAnswersDtos , int examId)
        {
            int mark = 0;
            var user = await _studentRepository.GetUserByName(examAnswersDtos[0].UserName);
            foreach (var item in examAnswersDtos)
            {
                List<QuestionsAnswer> answers = await _examRepository.GetQuestionAnswers(item.QuestionId);
                var answe =  answers.Where(x => x.Answer.IsCorrect == true).FirstOrDefault();
                if (answe.Answer.AnswerText == item.QuestionAnswer)
                {
                    mark += answe.Question.Mark;
                }
            }
            ExamStudents examStudent = new ExamStudents { CreatedDate = DateTime.Now, ExamId = examId, ObtainedMarks = mark , StudentId = user.Id };
            var student = await _examRepository.AddStudentToExam(examStudent);
            return student;
        }
    }
}
