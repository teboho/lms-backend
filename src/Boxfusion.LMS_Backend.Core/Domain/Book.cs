using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxfusion.LMS_Backend.Domain
{
    public class Book : Entity<long>
    {
        public string Name { get; set; }
        public string Description {  get; set; }
        public byte Type { get; set; }
        public long Year { get; set; }
        public long CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category CategoryModel { get; set; }
    }
}
