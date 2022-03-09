using AutoMapper;
using TodoListAPI.Entities;
using TodoListAPI.Models;

namespace TodoListAPI
{
    // Class in which maps objects using automapper.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>();
            CreateMap<CreateTodoDto, Todo>()
                .ForMember(m => m.ExpireDay, c => c.MapFrom(s => s.ExpireDate.Day))
                .ForMember(m => m.ExpireMonth, c => c.MapFrom(s => s.ExpireDate.Month))
                .ForMember(m => m.ExpireYear, c => c.MapFrom(s => s.ExpireDate.Year));
        }
    }
}
