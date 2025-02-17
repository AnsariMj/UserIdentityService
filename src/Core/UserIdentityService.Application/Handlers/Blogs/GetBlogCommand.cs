using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Common.Interfaces;

namespace UserIdentityService.Application.Handlers.Blogs;


public class GetBlogCommandHandler : IRequestHandler<GetBlogCommand, GetBlogResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetBlogCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBlogResponse> Handle(GetBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = await _dbContext.Blogs.FindAsync(request.Id);

        if (blog == null)
        {
            throw new KeyNotFoundException("Blog not found");
        }

        return new GetBlogResponse
        {
            Id = blog.Id,
            UserId = blog.UserId,
            Title = blog.Title,
            Content = blog.Content,
            Image = blog.Image,
            AuthorName = blog.AuthorName,
            CreatedAt = blog.CreatedAt,
            UpdateAt = blog.UpdateAt,
            PublishedAt = blog.PublishedAt,
            IsPublished = blog.IsPublished
        };
    }
}
public class GetBlogCommand : IRequest<GetBlogResponse>
{
    public Guid Id { get; set; }
}

public class GetBlogResponse
{
    public Guid Id { get; set; }
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