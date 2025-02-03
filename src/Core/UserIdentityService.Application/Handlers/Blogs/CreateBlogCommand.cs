using MediatR;
using UserIdentityService.Domain.Entities;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Common.Interfaces;

namespace UserIdentityService.Application.Handlers.Blogs;


public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Response>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateBlogCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var BlogData = new Blog
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            Image = request.Image,
            AuthorName = request.AuthorName,
            CreatedAt = request.CreatedAt,
            UpdateAt = request.UpdateAt,
            PublishedAt = request.PublishedAt,
            IsPublished = request.IsPublished,
        };
        _dbContext.Blogs.Add(BlogData);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new Response
        {
            Status = "Success",
            Message = "Blog Created Successfully",
            IsSuccess = true
        };
    }
}
public class CreateBlogCommand : IRequest<Response>
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }
}
