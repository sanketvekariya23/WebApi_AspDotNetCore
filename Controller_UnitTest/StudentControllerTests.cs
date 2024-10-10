using AutoMapper;
using Login_Register.Controllers;
using Login_Register.DTOs;
using Login_Register.Model;
using Login_Register.Process;
using Login_Registor.Data;
using Login_Registor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;
using System.Security.Claims;
using Xunit;

namespace Login_Register_unittest.Controller
{
    public class StudentControllerTests
    {
        private readonly StudentController _controller;
        internal readonly Mock<IMapper> _mapper;
        internal readonly Mock<ILogger<StudentProcess>> _logger;

        public StudentControllerTests()
        { 
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<StudentProcess>>();
            var user = new User
            {
                Id = 1,
                Username = "Test",
                Password = "Test",
                Name = "Test",
                Email = "Test@gmail.com",
                ContactNo = "9876543210",
                IsActive = true,
                IsAdmin = true,
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoie1wiaWRcIjoxLFwidXNlcm5hbWVcIjpcIlRlc3RcIixcInBhc3N3b3JkXCI6XCJcIixcIm5hbWVcIjpcIlRlc3RcIixcImVtYWlsXCI6XCJUZXN0QGdtYWlsLmNvbVwiLFwiY29udGFjdE5vXCI6XCIxMjM0NTY3ODkwXCIsXCJpc0FjdGl2ZVwiOnRydWUsXCJpc0FkbWluXCI6ZmFsc2UsXCJhY2Nlc3NUb2tlblwiOm51bGx9Iiwicm9sZSI6IlVzZXIiLCJuYmYiOjE3MjgzNjc3NDQsImV4cCI6MTcyODQxMjE5OSwiaWF0IjoxNzI4MzY3NzQ0fQ.nljNYC5HtbAXWKelBi49SVjTvWcbOcsAO336dZYB3k8"
            };
            _controller = new StudentController(user, _logger.Object,_mapper.Object);
        }

        [Fact]
        public async Task GetReturn_SuccessResponse()
        {
            var students = new List<StudentDTO> { new StudentDTO { EnrollementNo = 1, Name = "John Doe", ContactNo = "1234567890", IsActive = true, IsAdmin = true } };
            _mapper.Setup(m => m.Map<List<StudentDTO>>(It.IsAny<List<Student>>())).Returns(students);

            var result = await _controller.Get();

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var apiResponse = Assert.IsAssignableFrom<ApiResponse>(actionResult.Value);
            Assert.Equal((byte)StatusFlag.Success, apiResponse.Status);
            Assert.NotNull(apiResponse.data);
        }
      
        [Fact]
        public async Task Post_Create_ReturnsSuccessResponse()
        {
            var studentDto = new StudentDTO{EnrollementNo = 1,Name = "John Doe",ContactNo = "1234567890",IsActive = true,IsAdmin = true};
             var apiResponse = new ApiResponse { Status = (byte)StatusFlag.Success };
            _mapper.Setup(m => m.Map<Student>(It.IsAny<StudentDTO>())).Returns(new Student());
            var processMock = new Mock<StudentProcess>(_logger.Object, _mapper.Object);
            processMock.Setup(p => p.Create(studentDto)).ReturnsAsync(apiResponse);
            var user = new User { Id = 1, Username = "Test", IsActive = true, IsAdmin = true };
            var controller = new StudentController(user, _logger.Object, _mapper.Object);
            controller.process = processMock.Object;
            var result = await controller.Post(studentDto);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsAssignableFrom<ApiResponse>(actionResult.Value);
            Assert.Equal((byte)StatusFlag.Success, returnedResponse.Status);
        }

        [Fact]
        public async Task Post_Update_ReturnsSuccessResponse()
        {
            var studentDto = new StudentDTO{EnrollementNo = 1, Name = "John Doe Updated",ContactNo = "9876543210",IsActive = true,IsAdmin = false};

            var apiResponse = new ApiResponse { Status = (byte)StatusFlag.Success };
            _mapper.Setup(m => m.Map<Student>(It.IsAny<StudentDTO>())).Returns(new Student());
            var processMock = new Mock<StudentProcess>(_logger.Object, _mapper.Object);
            processMock.Setup(p => p.Update(studentDto)).ReturnsAsync(apiResponse);
            var user = new User { Id = 1, Username = "Test", IsActive = true, IsAdmin = true };
            var controller = new StudentController(user, _logger.Object, _mapper.Object);
            controller.process = processMock.Object;
            var result = await controller.Put(studentDto);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsAssignableFrom<ApiResponse>(actionResult.Value);
            Assert.Equal((byte)StatusFlag.Success, returnedResponse.Status);
        }

        [Fact]
        public async Task Delete_ReturnsSuccessResponse()
        {
            var enrollmentNo = 1;
            var apiResponse = new ApiResponse { Status = (byte)StatusFlag.Success };

            var processMock = new Mock<StudentProcess>(_logger.Object, _mapper.Object);
            processMock.Setup(p => p.Delete(enrollmentNo)).ReturnsAsync(apiResponse);

            var result = await _controller.Put(enrollmentNo);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnedResponse = Assert.IsAssignableFrom<ApiResponse>(actionResult.Value);
            Assert.Equal((byte)StatusFlag.Success, returnedResponse.Status);
        }
    }
}
