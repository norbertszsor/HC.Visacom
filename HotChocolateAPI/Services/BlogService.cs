using AutoMapper;
using HotChocolateAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotChocolateAPI.Services
{
    public interface IBlogSerivce
    {
        public void CreatePost(Post dto);
    }
    public class BlogService : IBlogSerivce
    {
        private readonly HotChocolateDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public BlogService(HotChocolateDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public void CreatePost(Post dto)
        {
            var post = new Post();
            post.PostParts = new List<PostParts>();
            foreach (var item in dto.PostParts)
            {
                post.PostParts.Add(item);
            }
            post.MainPictureAdress = dto.MainPictureAdress;
            post.Title = dto.Title;
            post.Date = DateTime.Now;
            post.Description = dto.Description;
            post.Author = _userContextService.User.Identity.Name;
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

    }
}
