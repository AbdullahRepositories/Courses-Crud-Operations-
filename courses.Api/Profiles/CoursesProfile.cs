﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courses.Api.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Course, Models.CourseDto>();
            CreateMap<Models.CourseForCreationDto, Entities.Course>();
            CreateMap<Models.CourseForUpdateDto, Entities.Course>();
        }
    }
}
