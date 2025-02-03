using MediatR;
using UserIdentityService.Application.Common;
using UserIdentityService.Application.Common.Interfaces;

namespace UserIdentityService.Application.Handlers.Blogs;

public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand, Response>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateBlogCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {

        var blog = await _dbContext.Blogs.FindAsync(request.Id);
        if (blog is null)
        {
            return new Response
            {
                Status = "Error",
                Message = "Blog not found.",
                IsSuccess = false
            };
        }

        blog.Title = request.Title;
        blog.Content = request.Content;
        blog.Image = request.Image;
        blog.AuthorName = request.AuthorName;
        blog.UpdateAt = request.UpdateAt;
        blog.PublishedAt = request.PublishedAt;
        blog.IsPublished = request.IsPublished;

        _dbContext.Blogs.Update(blog);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response
        {
            Status = "Success",
            Message = "Blog updated successfully.",
            IsSuccess = true
        };
    }
}
public class UpdateBlogCommand : IRequest<Response>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Image { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }
}
