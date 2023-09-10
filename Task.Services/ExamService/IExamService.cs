using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Dtos;
using Task.Core.Entities;

namespace Task.Services.ExamService
{
    public interface IExamService
    {
        Task<List<Exam>> GetAllExams();
        Task<Exam> GetExamById(int Id);
        Task<Exam> AddExam(Exam exam);
        Task<int> UpdateExam(Exam exam);
        Task<List<Question>> GetExamQuestions(int examId);
        Task<Question> AddQuestionToExam(Question question , int examId);
        Task<int> EditQuestionToExam(Question question);
        Task<int> RemoveQuestion(int QId);
        Task<bool> IsPermittedToAdd(int questionMark, int examId);
        Task<List<Answer>?> AddOrEditAnswerToQuestion(int questionId, QuestionAnswerDto questionAnswerDto);
    }
}
