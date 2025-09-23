namespace Application.Dtos.Department
{
    public class DepartmentDetailDto : DepartmentDto
    {
       public Guid Id { get; set; }
        public bool Highlighted { get; set; } = false;
    }
}
