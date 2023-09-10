using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;

namespace Task.Infrastructure.ExamRepository
{
    public interface IExamRepository
    {
        Task<List<Exam>> GetExamsAsync();
        Task<Answer> GetAnswerAsync(int answerId);
        Task<Exam> GetExamsById(int Id);
        Task<Exam> AddExamAsync(Exam exam);
        Task<int> UpdateExamAsync(Exam exam);
        Task<List<Question>> GetQuestionsByExamId(int examId);
        Task<Question> AddQuestionToExam(Question question);
        Task<int> EditQuestionToExam(Question question);
        Task<int> RemoveQustion(int questionId);
        Task<int> RemoveExam(int examId);
        Task<List<QuestionsAnswer>> GetQuestionAnswers(int questionId);
        Task<QuestionsAnswer> AddQuestionsAnswer(QuestionsAnswer questionsAnswer);
        Task<Question> GetQuestionById(int QId);
        Task<int> AddAnswersAsync(List<Answer> answers);
        Task<QuestionsAnswer> AddQuestionAnswers(QuestionsAnswer questionsAnswer);
        Task<int> RemoveAnswerQuestion(QuestionsAnswer questionsAnswer);
        Task<int> RemoveAnswer(Answer answer);
        Task<ExamStudents> AddStudentToExam(ExamStudents examStudents);
    }
}
