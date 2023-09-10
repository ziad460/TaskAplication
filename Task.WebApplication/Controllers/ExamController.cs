using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Task.Core.Dtos;
using Task.Core.Entities;
using Task.Services.ExamService;
using Task.Services.StudentService;
using Task.Services.UserService;

namespace Task.WebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;

        public ExamController(IExamService examService , IStudentService studentService , IUserService userService) 
        {
            _examService = examService;
            _studentService = studentService;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.students = new SelectList(await _studentService.GetAllStudents() , "Id" , "UserName");
            return View(await _examService.GetAllExams());
        }

        public async Task<IActionResult> AddExam()
        {
            ViewBag.Teachers = new SelectList(await _userService.GetUsersInRole("Teacher"), "Id", "UserName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddExam(Exam exam)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Teachers = new SelectList(await _userService.GetUsersInRole("Teacher"), "Id", "UserName");
                return View();
            }
            
            Exam Exam =  await _examService.AddExam(exam);

            if (Exam != null)
            {
                return RedirectToAction("Questions" , new { examId = Exam.Id});
            }

            ViewBag.Teachers = new SelectList(await _userService.GetUsersInRole("Teacher"), "Id", "UserName");
            return View();
        }

        public async Task<IActionResult> Questions(int examId)
        {
            ViewData["ExamId"] = examId;
            var questions = await _examService.GetExamQuestions(examId);
            return View(questions);
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestion(Question question , QuestionAnswerDto questionAnswerDto)
        {
            if (await _examService.IsPermittedToAdd(question.Mark, question.ExamId))
            {
                Question questionAdded = await _examService.AddQuestionToExam(question, question.ExamId);
                if (questionAdded != null)
                {
                    await _examService.AddOrEditAnswerToQuestion(questionAdded.Id, questionAnswerDto);
                }
            }

            return RedirectToAction("Questions" , new { examId = question.ExamId });
        }

        [HttpPost]
        public async Task<IActionResult> EditQuestion(Question question)
        {
            await _examService.EditQuestionToExam(question);
            return RedirectToAction("Questions", new { examId = question.ExamId });
        }


        public async Task<IActionResult> RemoveQuestion(int questionId , int ExamId)
        {
            await _examService.RemoveQuestion(questionId);
            return RedirectToAction("Questions", new { examId = ExamId });
        }

        public async Task<IActionResult> AssignStudents(List<int> students, int examId)
        {
            int res = await _studentService.AssignStudentToExam(students, examId);
            return RedirectToAction("Index");
        }

    }
}
