using AutoMapper;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotChocolateAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateAPI.Services
{
    public interface IBlogSerivce
    {
         int CreatePost(PostDto dto);
         List<PostView> GetAllPosts();
         Post GetPostById(int id);
         void Delete(int id);
        void UpdatePost(int id, UpdateBlogDto dto);
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
        public int CreatePost(PostDto dto)
        {
            var post = new Post();
            if (dto.PostParts != null)
            {
                post.PostParts = new List<PostParts>();
                foreach (var item in dto.PostParts)
                {
                    post.PostParts.Add(item);
                }
            }
            post.MainPictureAdress = dto.MainPictureAdress;
            post.Title = dto.Title;
            post.Date = DateTime.Now;
            post.Description = dto.Description;
            post.Author = _userContextService.User.Identity.Name;
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post.Id;
        }
        public List<PostView> GetAllPosts()
        {
            var posts = _context.Posts.ToList();

            if (posts == null)
                throw new EmptyListException("Pusta lista postów");
            
            return _mapper.Map<List<PostView>>(posts);

        }
        public Post GetPostById(int id)
        {
            var post = _context.Posts.Include(x=>x.PostParts).FirstOrDefault(x => x.Id == id);
            if (post == null)
                throw new EmptyListException($"Nie znaleziono bloga o id:{id}");

            return post;
        }
        public void Delete(int id)
        {
            var post = _context.Posts.Include(x=>x.PostParts).FirstOrDefault(x => x.Id == id);
            if (post == null)
                throw new EmptyListException($"Nie znaleziono bloga o id:{id}");

            _context.PostParts.RemoveRange(post.PostParts);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public void UpdatePost(int id, UpdateBlogDto dto)
        {
            var post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null)
                throw new Exception("Post nie istnieje");
            post.Description = dto.Description;
            post.Title = dto.Title;
            post.MainPictureAdress = dto.MainPictureAdress;
            _context.SaveChanges();

        }
    }
}
