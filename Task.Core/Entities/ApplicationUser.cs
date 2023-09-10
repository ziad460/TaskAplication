using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Core.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get ; set ; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsNew { get; set; }

        public virtual ICollection<Exam>? TeatchersExams { get; set; }

        public virtual ICollection<Answer>? Answers { get; set; }

        public virtual ICollection<ExamStudents>? ExamStudents { get; set; }
    }
}
