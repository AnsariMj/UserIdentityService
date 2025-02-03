using UserIdentityService.Domain.Models;

namespace UserIdentityService.Domain.Entities;

public class Blog
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }
    public string UserId { get; set; }
    public virtual ApplicatioinUser User { get; set; }
}
