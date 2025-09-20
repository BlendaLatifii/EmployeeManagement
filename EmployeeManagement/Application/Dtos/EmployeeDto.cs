﻿namespace Application.Dtos
{
    public class EmployeeDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
