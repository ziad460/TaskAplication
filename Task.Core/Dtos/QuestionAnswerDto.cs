using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Enums;

namespace Task.Core.Dtos
{
    public class QuestionAnswerDto
    {
        public string QuestionText1 { get; set; }
        public string QuestionText2 { get; set; }
        public string QuestionText3 { get; set; }
        public string QuestionText4 { get; set; }

        public string IsCorrect { get; set; }

    }
}
