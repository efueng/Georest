using AutoMapper;
using Georest.Api.ViewModels;
using Georest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Api.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Instructor, InstructorViewModel>();
            CreateMap<InstructorViewModel, Instructor>();
            //CreateMap<InstructorViewModel, InstructorInputViewModel>();
            CreateMap<InstructorInputViewModel, Instructor>();

            CreateMap<InstructorLab, InstructorLabViewModel>();
            CreateMap<InstructorLabViewModel, InstructorLab>();
            CreateMap<InstructorLabInputViewModel, InstructorLab>();

            CreateMap<InstructorResponse, InstructorResponseViewModel>();
            CreateMap<InstructorResponseInputViewModel, InstructorResponse>();

            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();
            CreateMap<StudentInputViewModel, Student>();

            CreateMap<StudentLab, StudentLabViewModel>();
            CreateMap<StudentLabInputViewModel, StudentLab>();

            CreateMap<StudentResponse, StudentResponseViewModel>();
            CreateMap<StudentResponseInputViewModel, StudentResponse>();

            CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<ExerciseInputViewModel, Exercise>();

            CreateMap<Section, SectionViewModel>();
            CreateMap<SectionViewModel, Section>();
        }
    }
}
