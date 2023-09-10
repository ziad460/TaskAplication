using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Entities
{
    public class Answer : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string? AnswerText { get; set; }
        public bool? IsCorrect { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question? Question { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public virtual ICollection<QuestionsAnswer>? QuestionsAnswers { get; set; }
    }
}
