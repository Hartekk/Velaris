using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VelarisBackend.DTOs;
using VelarisBackend.Infrastructure;
using VelarisBackend.Models;

namespace VelarisBackend.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly DatabaseContext _context;

        public ToDoItemService(DatabaseContext context)
        {
            _context = context;
        }
        public TodoItem Create(string userId, ToDoItemAddReq request)
        {
            var todo = new TodoItem
            {
                Title = req.Title,
                IsCompleted = false,
                UserId = userId
            };

            _context.TodoItems.Add(todo);
            _context.SaveChanges();

            return todo;
        }
    }
}