using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using TaskManagment.Entities;
using TaskManagment.Models;

namespace TaskManagment.AppServices.Employees
{
    public class EmployeeAppService(TasksDbContext _dbContext, IMapper _mapper) : IEmployeeAppService
    {


        public async Task<PageResult<EmployeeDto>> GetAll(DataTableFilter filter)
        {
            //  Get  Employees as Querable

            var emps = _dbContext.Employees.AsQueryable();

            int totalRecords = await emps.CountAsync();
            if (!string.IsNullOrWhiteSpace(filter.Filter))
            {
                string filterTrimed = filter.Filter.Trim();
                emps = emps.Where(s => s.Name.Contains(filterTrimed) || s.Email.Contains(filterTrimed) || (s.PhoneNumber != null && s.PhoneNumber.Contains(filterTrimed)));
            }

            if (!string.IsNullOrWhiteSpace(filter.OrderBy))
            {
                filter.OrderBy = nameof(Employee.Name);
                filter.OrderDir = "asc";
            }

            int filteredCount = await emps.CountAsync();

            var result = await emps.OrderBy($"{filter.OrderBy} {filter.OrderDir}")
                .Skip(filter.Start)
                .Take(filter.Length)
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider).ToListAsync();


            return new PageResult<EmployeeDto> { Data = result, RecordsFiltered = filteredCount, RecordsTotal = totalRecords };

        }


        public async Task<bool> Create(CreateEmployeeDto input)
        {
            var  emp =_mapper.Map<Employee>(input);

            emp.IsActive = true;
            //  call  add to  insert to  database
            _dbContext.Employees.Add(emp);
          return   await _dbContext.SaveChangesAsync()>0;
        }


        public async Task<CreateEmployeeDto?> GetForEditById(int id)
        {
            var emp = await _dbContext.Employees.FindAsync(id);

            if (emp != null)
            {

                return _mapper.Map<CreateEmployeeDto?>(emp);
            }

            return null;
        }

        public async Task<bool> Update(CreateEmployeeDto input)
        {
           var  empFromDb =  await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(s=>s.Id==input.Id);
            if (empFromDb != null)
            {
                // Replace-->
               // _dbContext.Employees.Update(_mapper.Map<Employee>(input));
                //  update matched properties
                 _mapper.Map(input, empFromDb);
                _dbContext.Employees.Update(empFromDb);

                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
