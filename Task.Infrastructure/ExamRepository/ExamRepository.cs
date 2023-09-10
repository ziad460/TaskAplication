using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;
using Task.Core.Interfaces;

namespace Task.Infrastructure.ExamRepository
{
    public class ExamRepository : IExamRepository
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IBaseRepository<Exam> _baseRepository;


        public ExamRepository(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
            _baseRepository = _unityOfWork.GetRepository<Exam>();
        }

        public async Task<List<Exam>> GetExamsAsync()
        {
            return await _baseRepository.FindAllAsync(x => !x.IsDeleted , new []{ "Teacher" , "Questions" , "ExamStudents" });
        }

        public async Task<Exam> GetExamsById(int Id)
        {
            return await _baseRepository.FindAsync(x => !x.IsDeleted && x.Id == Id, new[] { "Teacher", "Questions", "ExamStudents" });
        }

        public async Task<Question> GetQuestionById(int QId)
        {
            return await _unityOfWork.GetRepository<Question>().FindAsync(x => !x.IsDeleted && x.Id == QId , new[] { "QuestionsAnswers" , "Exam" });
        }


        public async Task<Exam> AddExamAsync(Exam exam)
        {
            _baseRepository.AddOne(exam);
            await _unityOfWork.CompleteAsync();  
            return exam;
        }

        public async Task<QuestionsAnswer> AddQuestionAnswers(QuestionsAnswer questionsAnswer)
        {
            _unityOfWork.GetRepository<QuestionsAnswer>().AddOne(questionsAnswer);
            await _unityOfWork.CompleteAsync();
            return questionsAnswer;
        }

        public async Task<int> AddAnswersAsync(List<Answer> answers)
        {
            _unityOfWork.GetRepository<Answer>().AddList(answers);
            return await _unityOfWork.CompleteAsync();
        }


        public async Task<int> UpdateExamAsync(Exam exam)
        {
            _baseRepository.UpdateOne(exam);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<List<Question>> GetQuestionsByExamId(int examId)
        {
            var Exam = await _baseRepository.QueryableFind(x => !x.IsDeleted && x.Id == examId)
                .Include(x => x.Questions).ThenInclude(x => x.QuestionsAnswers).ThenInclude(x => x.Answer).FirstOrDefaultAsync();

            return Exam.Questions.ToList();
        }

        public async Task<Question> AddQuestionToExam(Question question)
        {
            _unityOfWork.GetRepository<Question>().AddOne(question);    
            await _unityOfWork.CompleteAsync();
            return question;
        }
        public async Task<int> EditQuestionToExam(Question question)
        {
            _unityOfWork.GetRepository<Question>().UpdateOne(question);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<int> RemoveExam(int examId)
        {
            var Exam = await _baseRepository.FindAsync(x => x.Id == examId);
            Exam.IsDeleted = true;
            _baseRepository.UpdateOne(Exam); 
            return await _unityOfWork.CompleteAsync(); 
        }

        public async Task<int> RemoveQustion(int questionId)
        {
            var question = await _unityOfWork.GetRepository<Question>().FindAsync(x => x.Id == questionId);
            question.IsDeleted = true;
            _unityOfWork.GetRepository<Question>().UpdateOne(question);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<List<QuestionsAnswer>> GetQuestionAnswers(int questionId)
        {
            return await _unityOfWork.GetRepository<QuestionsAnswer>().FindAllAsync(x => x.QuestionId == questionId , new[] { "Answer" , "Question" } );
            
        }

        public async Task<QuestionsAnswer> AddQuestionsAnswer(QuestionsAnswer questionsAnswer)
        {
            _unityOfWork.GetRepository<QuestionsAnswer>().AddOne(questionsAnswer);
            await _unityOfWork.CompleteAsync();
            return questionsAnswer;
        }

        public async Task<ExamStudents> AddStudentToExam(ExamStudents examStudents)
        {
            _unityOfWork.GetRepository<ExamStudents>().AddOne(examStudents);
            await _unityOfWork.CompleteAsync();
            return examStudents;
        }

        public async Task<int> RemoveAnswerQuestion(QuestionsAnswer questionsAnswer)
        {
            _unityOfWork.GetRepository<QuestionsAnswer>().Remove(questionsAnswer);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<int> RemoveAnswer(Answer answer)
        {
            _unityOfWork.GetRepository<Answer>().Remove(answer);
            return await _unityOfWork.CompleteAsync();
        }

        public async Task<Answer> GetAnswerAsync(int answerId)
        {
            return await _unityOfWork.GetRepository<Answer>().FindAsync(x => x.Id == answerId);
        }



    }
}
