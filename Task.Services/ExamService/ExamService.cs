using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Dtos;
using Task.Core.Entities;
using Task.Core.Interfaces;
using Task.Infrastructure.ExamRepository;

namespace Task.Services.ExamService
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository _examRepository;

        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public async Task<List<Exam>> GetAllExams()
        {
            return await _examRepository.GetExamsAsync();
        }

        public async Task<Exam> GetExamById(int Id)
        {
            return await _examRepository.GetExamsById(Id);
        }


        public async Task<Exam> AddExam(Exam exam)
        {
            return await _examRepository.AddExamAsync(exam);
        }

        public async Task<int> UpdateExam(Exam exam)
        {
            return await _examRepository.UpdateExamAsync(exam);
        }

        public async Task<List<Question>> GetExamQuestions(int examId)
        {
            return await _examRepository.GetQuestionsByExamId(examId);
        }

        public async Task<Question?> AddQuestionToExam(Question question , int examId)
        {
            if(await IsPermittedToAdd(question.Mark , examId))
                
                return await _examRepository.AddQuestionToExam(question);
            return null;
        }

        public async Task<int> EditQuestionToExam(Question question)
        {
            return await _examRepository.EditQuestionToExam(question);
        }

        public async Task<int> RemoveQuestion(int QId)
        {
            return await _examRepository.RemoveQustion(QId);
        }

        public async Task<bool> IsPermittedToAdd(int questionMark , int examId)
        {
            var exam = await GetExamById(examId); 
            var questions = await GetExamQuestions(examId);

            if (questions == null)
                return true;

            int TotalMarks = questions.Sum(x => x.Mark) + questionMark;

            if ( TotalMarks <= exam.TotalMarks)
                return true;

            return false;
        }

        public async Task<int> RemoveAnswers(Answer answer)
        {
            var questionAnswer =  await _examRepository.GetQuestionAnswers((int)answer.QuestionId);
            foreach (var item in questionAnswer)
            {
                await _examRepository.RemoveAnswerQuestion(item);
            }

            return await _examRepository.RemoveAnswer(answer);
        }

        public async Task<List<Answer>?> AddOrEditAnswerToQuestion(int questionId , QuestionAnswerDto questionAnswerDto)
        {
            List<Answer> answers = new List<Answer>();
            var question = await _examRepository.GetQuestionById(questionId);
            
            if (question.QuestionsAnswers == null || question.QuestionsAnswers.Count == 0)
            {
                switch (question.QuestionType)
                {
                    case Core.Enums.QuestionType.MultipleChoice:
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText2, IsCorrect = questionAnswerDto.IsCorrect == "Q2" ? true : false, QuestionId = questionId });
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText3, IsCorrect = questionAnswerDto.IsCorrect == "Q3" ? true : false, QuestionId = questionId });
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText4, IsCorrect = questionAnswerDto.IsCorrect == "Q4" ? true : false, QuestionId = questionId });
                        break;
                    case Core.Enums.QuestionType.TrueFalse:
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText2, IsCorrect = questionAnswerDto.IsCorrect == "Q2" ? true : false, QuestionId = questionId });
                        break;
                    case Core.Enums.QuestionType.FillInBlank:
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                        break;
                    case Core.Enums.QuestionType.Essay:
                        answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                        break;
                }

                int result = await _examRepository.AddAnswersAsync(answers);

                if (result > 0)
                {
                    foreach (var item in answers)
                    {
                        await _examRepository.AddQuestionAnswers(new QuestionsAnswer { AnswerId = item.Id, QuestionId = questionId });
                    }
                    return answers;
                }

            }
            else
            {
                var answer = await _examRepository.GetAnswerAsync(question.QuestionsAnswers.First().AnswerId);
                int res = await RemoveAnswers(answer);
                if (res > 0)
                {
                    switch (question.QuestionType)
                    {
                        case Core.Enums.QuestionType.MultipleChoice:
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText2, IsCorrect = questionAnswerDto.IsCorrect == "Q2" ? true : false, QuestionId = questionId });
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText3, IsCorrect = questionAnswerDto.IsCorrect == "Q3" ? true : false, QuestionId = questionId });
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText4, IsCorrect = questionAnswerDto.IsCorrect == "Q4" ? true : false, QuestionId = questionId });
                            break;
                        case Core.Enums.QuestionType.TrueFalse:
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText2, IsCorrect = questionAnswerDto.IsCorrect == "Q2" ? true : false, QuestionId = questionId });
                            break;
                        case Core.Enums.QuestionType.FillInBlank:
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                            break;
                        case Core.Enums.QuestionType.Essay:
                            answers.Add(new Answer { AnswerText = questionAnswerDto.QuestionText1, IsCorrect = questionAnswerDto.IsCorrect == "Q1" ? true : false, QuestionId = questionId });
                            break;
                    }

                    int result = await _examRepository.AddAnswersAsync(answers);

                    if (result > 0)
                    {
                        foreach (var item in answers)
                        {
                            await _examRepository.AddQuestionAnswers(new QuestionsAnswer { AnswerId = item.Id, QuestionId = questionId });
                        }
                        return answers;
                    }
                }
            }

            return null;
        }
    }
}
