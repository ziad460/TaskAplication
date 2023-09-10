using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Enums;

namespace Task.Core.Entities
{
    public class Question : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ExamId { get; set; }

        public QuestionType QuestionType { get; set; }

        [Required]
        public string Text { get; set; }
        [Required]
        public int Mark { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }
        public virtual ICollection<QuestionsAnswer> QuestionsAnswers { get; set; }
    }
}
