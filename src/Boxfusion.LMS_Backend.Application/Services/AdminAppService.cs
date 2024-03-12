using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;
using Boxfusion.LMS_Backend.Domain;
using Boxfusion.LMS_Backend.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxfusion.LMS_Backend.Services
{
    public class AdminAppService : AsyncCrudAppService<Admin, AdminDto, Guid>
    {
        public IRepository<Admin, Guid> _repository;
        public AdminAppService(IRepository<Admin, Guid> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
