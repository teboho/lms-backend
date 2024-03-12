using Abp.Application.Services;
using Abp.Domain.Repositories;
using Boxfusion.LMS_Backend.Domain;
using Boxfusion.LMS_Backend.Services.Dtos;
using Boxfusion.LMS_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxfusion.LMS_Backend.Services
{
    public class PatronAppService : AsyncCrudAppService<Patron, PatronDto, Guid>, IPatronAppService
    {
        public PatronAppService(IRepository<Patron, Guid> repository) : base(repository)
        {
        }
    }
}
