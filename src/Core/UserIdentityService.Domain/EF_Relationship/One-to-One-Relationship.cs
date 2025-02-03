using Microsoft.EntityFrameworkCore;

namespace UserIdentityService.Domain.EF_Relationship;

public class One_to_One_Relationship
{
}

/////////-------------------------O-N-E---T-O---O-N-E-------------------------//////////
public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    // Navigation property
    public UserProfile Profile { get; set; }
}
public class UserProfile
{
    public int UserProfileId { get; set; }
    public string FullName { get; set; }

    // Foreign Key and navigation property 
    public int UserId { get; set; }
    public User User { get; set; }
}

/*public class FluentAPI_One_to_One
{
    ModelBuilder.Entity<User>()
        .HasOne(u=>u.Profile)
        .Withone(p=>p.User)
        .HasForeignKey(p=>p.UserId)
}
*/

/////////-------------------------O-N-E---T-O---M-A-N-Y-------------------------//////////

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    // Navigation property
    public ICollection<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    // Foreign Key and navigation property 
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}


/*public class FluentAPI_One_to_Many
{
    ModelBuilder.Entity<Blog>()
        .HasMany(b=>b.Posts)
        .WithOne(p=>p.Blog)
        .HasForeignKey(p=>p.BlogId)
}*/


/////////-------------------------M-A-N-Y---T-O---M-A-N-Y-------------------------//////////


public class Student
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}


public class Course
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}

/*
public class FluentAPI_Many_to_Many
{
    ModelBuilder.Entity<Student>()
        .HasMany(s=>s.Course)
        .WithMany(c=>c.Students)
        .UsingEntity(j=>j.ToTable("StudentCourse"));
}*/




/////////-------------------------Self-Referencing Relationship-------------------------//////////
///
public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; }

    public int? ManagerId { get; set; }
    public Employee Manager { get; set; }
    public ICollection<Employee> GetEmployees { get; set; }
}