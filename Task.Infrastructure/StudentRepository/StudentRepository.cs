using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Entities;
using Task.Core.Interfaces;

namespace Task.Infrastructure.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentRepository(IUnityOfWork unityOfWork , UserManager<ApplicationUser> userManager) 
        {
            _unityOfWork = unityOfWork;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUser>> GetAllStudents()
        {
            IList<ApplicationUser> users =  await _userManager.GetUsersInRoleAsync("Student");
            return users.ToList();
        }

        public async Task<ApplicationUser?> GetUserByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<ApplicationUser?> GetUserById(int Id)
        {
            return await _userManager.FindByIdAsync(Id.ToString());
        }

        public async Task<List<ExamStudents>> GetUserExams(int userId)
        {
            return await _unityOfWork.GetRepository<ExamStudents>().FindAllAsync(x => x.StudentId == userId , new[] { "Student" , "Exam" } );
        }

        public async Task<int> AddStudentsToExam(List<ExamStudents> examStudents)
        {
            _unityOfWork.GetRepository<ExamStudents>().AddList(examStudents);
            return await _unityOfWork.CompleteAsync();
        }
    }
}
