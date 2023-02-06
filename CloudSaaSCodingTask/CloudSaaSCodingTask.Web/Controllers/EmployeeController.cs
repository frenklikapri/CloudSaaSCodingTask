using AutoMapper;
using CloudSaaSCodingTask.Core.Dtos;
using CloudSaaSCodingTask.Core.Entities;
using CloudSaaSCodingTask.Core.Helpers;
using CloudSaaSCodingTask.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq.Expressions;

namespace CloudSaaSCodingTask.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IGenericRepository<Employee> employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(string? name, string? email, string? department, int page = 1, int pageSize = 10)
        {
            var filter = EmployeeSearchHelper.BuildFilter(name, email, department);

            var result = await _employeeRepository
                .GetItemsAsync(new Core.Common.PaginationParameters
                {
                    Page = page,
                    PageSize = pageSize
                }, filter);

            return Ok(new
            {
                TotalCount = result.TotalCount,
                Items = result.Items.Select(i => _mapper.Map<ViewEmployeeDto>(i)).ToList()
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id);

            if (entity.NotFound)
                return NotFound();

            return Ok(entity.Entity);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            var entity = _mapper.Map<Employee>(addEmployeeDto);
            await _employeeRepository.AddAsync(entity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] EditEmployeeDto editEmployeeDto)
        {
            var entity = _mapper.Map<Employee>(editEmployeeDto);
            var result = await _employeeRepository.EditAsync(entity);

            if (result.NotFound)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _employeeRepository.DeleteAsync(id);

            if (result.NotFound)
                return NotFound();

            return Ok();
        }
    }
}
