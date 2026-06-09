using System;
using System.Collections.Generic;

namespace SchoolDBFirst.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Age { get; set; }
}
