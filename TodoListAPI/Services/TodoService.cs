using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoListAPI.Entities;
using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    // Class with main buisness logic
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _dbContext;
        private readonly IMapper _mapper;

        public TodoService(TodoDbContext dbContext, IMapper mapper)     // Dependency incjection of AutoMapper
        {                                                               //  and database context
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // This method search specific entity by ID from
        // database to and returns it to api controller.
        public TodoDto GetById(int id)
        {
            var todo = _dbContext.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return null;
            var result = _mapper.Map<TodoDto>(todo);
            return result;
        }

        // This method search all entities from
        // database and returns it to api controller.
        public IEnumerable<TodoDto> GetAll()
        {
            var todoList = _dbContext.Todos.ToList();
            var todoListDto = _mapper.Map<List<TodoDto>>(todoList);
            return todoListDto;
        }

        // This method creates new entity, maps it form DTO to Todo entity
        // and add to databese.
        public bool Create(CreateTodoDto dto)
        {
            if (dto.ExpireDate < DateTime.Now) return false;

            var todo = _mapper.Map<Todo>(dto);
            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();
            return true;

        }

        // This method delete entity by id from databese.
        public bool Delete(int id)
        {
            var todo = _dbContext.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return false;

            _dbContext.Todos.Remove(todo);
            _dbContext.SaveChanges();

            return true;
        }

        // This method updates data which it received from controller
        // and saves it in database.
        public bool Update(int id, UpdateTodoDto dto)
        {
            var todo = _dbContext.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return false;
            if (!(dto.Title is null))
                todo.Title = dto.Title;
            if (!(dto.Description is null))
                todo.Description = dto.Description;
            if (!(dto.StartDate == DateTime.MinValue || dto.ExpireDate < DateTime.Now))
                todo.StartDate = dto.StartDate;
            if (!(dto.ExpireDate == DateTime.MinValue || dto.ExpireDate < DateTime.Now))
            {
                todo.ExpireDate = dto.ExpireDate;
                todo.ExpireDay = dto.ExpireDate.Day;
                todo.ExpireMonth = dto.ExpireDate.Month;
                todo.ExpireYear = dto.ExpireDate.Year;
            }
            _dbContext.SaveChanges();

            return true;
        }

        // This method change done/undone in the "Done" column in specific entity by ID.
        public bool SetAsDoneUndone(int id)
        {
            var todo = _dbContext.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return false;

            if (todo.Done == false)
            {
                todo.Done = true;
                todo.PercentComplete = 100;
            }
            else
            {
                todo.Done = false;
                todo.PercentComplete = 0;
            }
            _dbContext.SaveChanges();
            return true;
        }


        // This metohod gest value of percent and update it in databese.
        public bool SetPercent(int id, SetPercentDto dto)
        {
            var todo = _dbContext.Todos.FirstOrDefault(x => x.Id == id);
            if (todo is null) return false;

            todo.PercentComplete = dto.PercentComplete;
            _dbContext.SaveChanges();
            return true;
        }

        // This method gest a data from databese, and returns selected by date records
        // to controller.
        public IEnumerable<TodoDto> GetByDate(int number)
        {
            var todoAll = GetAll();
            var todoList = new Object();

            if ((Dates)number == Dates.today)   // By today
            {
                todoList = todoAll.Where(x => x.ExpireDay == DateTime.Now.Day &&
                    x.ExpireMonth == DateTime.Now.Month &&
                    x.ExpireYear == DateTime.Now.Year).ToList();
            }
            if ((Dates)number == Dates.nextday)     // By next dat
            {
                todoList = todoAll.Where(x => x.ExpireDay == DateTime.Now.AddDays(1).Day &&
                    x.ExpireMonth == DateTime.Now.Month &&
                    x.ExpireYear == DateTime.Now.Year).ToList();
            }
            if ((Dates)number == Dates.currentmonth) // By current month
            {
                todoList = todoAll.Where(x => x.ExpireMonth == DateTime.Now.Month &&
                    x.ExpireYear == DateTime.Now.Year).ToList();
            }
            if ((Dates)number == Dates.currentyear) // By current year
            {
                todoList = todoAll.Where(x => x.ExpireYear == DateTime.Now.Year).ToList();
            }
            if ((Dates)number == Dates.currentweek)     // By current week. 
            {
                var numDayOfWeek = (int)(DayWeek)DateTime.Now.DayOfWeek;
                var monday = DateTime.Now.Day - (numDayOfWeek - 1); // Counts number of day of monday
                var sunday = DateTime.Now.Day + (7 - numDayOfWeek); // Counts number of day of sunday

                todoList = todoAll.Where(
                    x => x.ExpireDay >= monday &&
                    x.ExpireDay <= sunday &&
                    x.ExpireMonth == DateTime.Now.Month &&  // Serching between monday and sunday of curent week.
                    x.ExpireYear == DateTime.Now.Year)
                    .ToList();
            }

            var todoListDto = _mapper.Map<List<TodoDto>>(todoList);  // returns resault
            return todoListDto;
        }
    }
}
