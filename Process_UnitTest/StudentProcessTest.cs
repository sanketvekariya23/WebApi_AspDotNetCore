using AutoMapper;
using Login_Register.DTOs;
using Login_Register.Model;
using Login_Register.Process;
using Login_Registor.Data;
using Login_Registor.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Login_Register_UnitTest.Process
{
    public class StudentProcessTest
    {
        private readonly Mock<ILogger<StudentProcess>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly StudentProcess _process;
        private readonly DefaultContext _context;

        public StudentProcessTest()
        {
            _logger = new Mock<ILogger<StudentProcess>>();
            _mapper = new Mock<IMapper>();
            _context = new DefaultContext();
            _context.Database.EnsureCreated();
            _process = new StudentProcess(_logger.Object, _mapper.Object);
        }

        [Fact]  
        public async Task Get_ReturnStudentList()
        {
            var studentList = new List<Student> { new Student { EnrollementNo = 1, Name = "John Doe", ContactNo = "1234567890", IsActive = true, IsAdmin = false } };
            _mapper.Setup(m => m.Map<List<StudentDTO>>(It.IsAny<List<Student>>())).Returns(new List<StudentDTO> { new StudentDTO { EnrollementNo = 1, Name = "John Doe", ContactNo = "1234567890", IsActive = true, IsAdmin = false } });

            var result = await _process.Get();

            Assert.Equal((byte)StatusFlag.Success, result.Status);
            Assert.NotNull(result.data);
        }

        [Fact]
        
        public async Task Create_StudentList()
        {
            var studentDto = new StudentDTO { Name = "John Doe", ContactNo = "1234567890", IsActive = true, IsAdmin = true };
            _mapper.Setup(m => m.Map<Student>(It.IsAny<StudentDTO>())).Returns((StudentDTO dto) => new Student {Name = dto.Name, ContactNo = dto.ContactNo, IsActive = dto.IsActive, IsAdmin = dto.IsAdmin });
            //_context.Student.RemoveRange(_context.Student); //for in memory database 
            var result = await _process.Create(studentDto);
            Assert.Equal((byte)StatusFlag.Success, result.Status);
            Assert.NotNull(result.Status);
            Assert.Equal("John Doe", studentDto.Name); 
        }
        [Fact]
        public async Task Updates_ExistingStudent()
        {
            var existingStudent = new Student {Name = "test update", ContactNo = "1234567890", IsActive = true, IsAdmin = true };
            _context.Student.Add(existingStudent); _context.SaveChanges();
            var studentDto = new StudentDTO { EnrollementNo = 2, Name = "test123", ContactNo = "814005890", IsActive = true, IsAdmin = true };
            _mapper.Setup(m => m.Map<Student>(It.IsAny<StudentDTO>())).Returns(new Student {EnrollementNo=studentDto.EnrollementNo, Name = studentDto.Name, ContactNo = studentDto.ContactNo, IsActive = studentDto.IsActive, IsAdmin = studentDto.IsAdmin });
            var result = await _process.Update(studentDto);
            Assert.Equal((byte)StatusFlag.Success, result.Status);
            var updatedStudent = await _context.Student.FindAsync(2);
            Assert.NotNull(updatedStudent);
            Assert.Equal("814005890", updatedStudent.ContactNo);
            Assert.Equal("test123", updatedStudent.Name);
        }

        [Fact]
        public async Task Delete_RemovesExistingStudent()
        {
            var enrollmentNo = 1;
            var apiResponse = new ApiResponse { Status = (byte)StatusFlag.Success };
            var student = new Student { EnrollementNo = 1 };
            _mapper.Setup(m => m.Map<Student>(It.IsAny<StudentDTO>())).Returns(student);
            var result = await _process.Delete(enrollmentNo);
            Assert.Equal((byte)StatusFlag.Success, result.Status);
        }
    }
}
