using System;
using System.Collections.Generic;

namespace SchoolDBFirst.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int Credits { get; set; }
}
