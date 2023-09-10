using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Core.Dtos;
using Task.Services.ExamService;
using Task.Services.StudentService;

namespace Task.WebApplication.Controllers
{
    [Authorize (Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;

        public StudentController(IStudentService studentService , IExamService examService)
        {
            _studentService = studentService;
            _examService = examService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _studentService.GetUserByName(User.Identity.Name);
            var studentExams = await _studentService.GetStudentExams(user.Id);

            return View(studentExams);
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _studentService.GetUserByName(User.Identity.Name);
            var studentExams = await _studentService.GetStudentExams(user.Id);

            return View(studentExams);
        }
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> StartStudentExam(int id)
        {
            var examQuestions = await _examService.GetExamQuestions(id); 
            if(examQuestions != null || examQuestions.Count > 0)
                return View(examQuestions);  
            return RedirectToAction("Index");   
        }
        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> StartStudentExam(IFormCollection examAnswersDtos , int examId)
        {
            List<ExamAnswersDto> examAnswersDto = new List<ExamAnswersDto>();

            for (int i = 0; i < examAnswersDtos["QuestionId"].Count; i++)
            {
                ExamAnswersDto exDto = new ExamAnswersDto();
                exDto.QuestionId = Convert.ToInt32(examAnswersDtos["QuestionId"][i]);
                exDto.QuestionAnswer = examAnswersDtos["QuestionAnswer"][i];
                exDto.UserName = examAnswersDtos["UserName"][i];
                examAnswersDto.Add(exDto);
            }
            await _studentService.SubmitExamToStudent(examAnswersDto , examId);

            return RedirectToAction("Index");
        }
    }
}
