using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Dtos
{
    public class ExamAnswersDto
    {
        public int QuestionId { get; set; }
        public string QuestionAnswer { get; set; }
        public string UserName { get; set; }
    }
}
