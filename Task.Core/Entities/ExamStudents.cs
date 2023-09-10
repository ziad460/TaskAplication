using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Entities
{
    public class ExamStudents : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
        public int ObtainedMarks { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam? Exam { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser? Student { get; set; }
    }
}
