using TodoListAPI.Models;

namespace TodoListAPI.Services
{
    public interface ITodoService
    {
        bool Create(CreateTodoDto dto);
        bool Delete(int id);
        System.Collections.Generic.IEnumerable<TodoDto> GetAll();
        System.Collections.Generic.IEnumerable<TodoDto> GetByDate(int number);
        TodoDto GetById(int id);
        bool SetAsDoneUndone(int id);
        bool SetPercent(int id, SetPercentDto dto);
        bool Update(int id, UpdateTodoDto dto);
    }
}