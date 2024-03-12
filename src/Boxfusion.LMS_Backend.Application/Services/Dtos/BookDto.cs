﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Boxfusion.LMS_Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxfusion.LMS_Backend.Services.Dtos
{
    [AutoMap(typeof(Book))]
    public class BookDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public long CategoryId { get; set; }
    }
}
