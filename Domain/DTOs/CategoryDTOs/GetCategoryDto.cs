using Domain.DTOs;

namespace Domain;
public class GetCategoryDto:BaseCategoryDto
{
    public List<GetToDoDto> ToDos { get; set; } = new List<GetToDoDto>();
}
