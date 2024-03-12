using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxfusion.LMS_Backend.Domain
{
    public class Loan : Entity<long>
    {
        public Guid PatronId { get; set; }
        [ForeignKey(nameof(PatronId))]
        public Patron PatronModel { get; set; }
        public long BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book BookModel { get; set; }
        public DateTime DateCreated { get; set; }

        public Loan()
        {
            DateCreated = DateTime.Now;
        }
    }
}
