using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Entities
{
    public class Exam : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; }

        public int TotalMarks { get; set; }

        public int? TeacherId { get; set; }             

        [ForeignKey("TeacherId")]
        public virtual ApplicationUser? Teacher { get; set; }

        public virtual ICollection<Question>? Questions { get; set; }

        public virtual ICollection<ExamStudents>? ExamStudents { get; set; }


    }
}
