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
    public class InventoryAppService : AsyncCrudAppService<Inventory, InventoryDto, long>, IInventoryAppService
    {
        public InventoryAppService(IRepository<Inventory, long> repository) : base(repository)
        {
        }
    }
}
