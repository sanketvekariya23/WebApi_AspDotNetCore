using AutoMapper;
using Login_Register.DTOs;
using Login_Register.Model;

namespace Login_Register.Mapping
{
    public class MappingStudent : Profile
    {
        public MappingStudent(){ CreateMap<Student, StudentDTO>(); CreateMap<StudentDTO, Student>(); }
    }
}
