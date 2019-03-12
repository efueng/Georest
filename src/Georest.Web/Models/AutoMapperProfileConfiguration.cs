using Georest.Web.ApiViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Georest.Web.Models
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            //CreateMap<Instructor, InstructorViewModel>();
            //CreateMap<InstructorViewModel, Instructor>();
            CreateMap<InstructorViewModel, InstructorInputViewModel>();
            CreateMap<InstructorInputViewModel, InstructorViewModel>();

            //CreateMap<InstructorLab, InstructorLabViewModel>();
            CreateMap<InstructorLabViewModel, InstructorLabInputViewModel>();
            CreateMap<InstructorLabInputViewModel, InstructorLabViewModel>();

            //CreateMap<InstructorResponse, InstructorResponseViewModel>();
            CreateMap<InstructorResponseViewModel, InstructorResponseInputViewModel>();
            CreateMap<InstructorResponseInputViewModel, InstructorResponseViewModel>();

            //CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, StudentInputViewModel>();
            CreateMap<StudentInputViewModel, StudentViewModel>();

            //CreateMap<StudentLab, StudentLabViewModel>();
            CreateMap<StudentLabViewModel, StudentLabInputViewModel>();
            CreateMap<StudentLabInputViewModel, StudentLabViewModel>();

            CreateMap<StudentResponseViewModel, StudentResponseInputViewModel>();
            CreateMap<StudentResponseInputViewModel, StudentResponseViewModel>();

            //CreateMap<Exercise, ExerciseViewModel>();
            CreateMap<ExerciseViewModel, ExerciseInputViewModel>();
            CreateMap<ExerciseInputViewModel, ExerciseViewModel>();

            CreateMap<SectionViewModel, SectionInputViewModel>();
            CreateMap<SectionInputViewModel, SectionViewModel>();
        }
    }
}
