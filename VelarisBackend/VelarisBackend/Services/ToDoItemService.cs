using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
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

        public TodoItem FindItem(string userId, int todoId)
        {
            if (string.IsNullOrEmpty(userId)) {
                throw new Exception("User not authenticated");
            }

            var todoItem = _context.TodoItems.FirstOrDefault(t => t.Id == todoId && t.UserId == userId);

            if (todoItem == null) {
                throw new Exception("Todo item not found");
            }

            return todoItem;
        }
        public TodoItem Create(string userId, ToDoItemAddReq request)
        {
            var todo = new TodoItem
            {
                Title = request.Title,
                IsCompleted = false,
                UserId = userId
            };

            _context.TodoItems.Add(todo);
            _context.SaveChanges();

            return todo;
        }

        public IEnumerable<TodoItem> GetAll(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated");
            }

            return _context.TodoItems.Where(t => t.UserId == userId).ToList();
        }

        public TodoItem GetOneTask(string userId, int taskId)
        {
            return FindItem(userId, taskId);
        }

        public TodoItem Edit(string userId, ToDoItemEditReq request)
        {
            var todoItem = FindItem(userId, request.Id);

            todoItem.Title = request.Title;
            todoItem.IsCompleted = request.IsCompleted;

            _context.SaveChanges();

            return todoItem;
        }

        public void Delete(string userId, int taskId)
        {
            var todoItem = FindItem(userId, taskId);

            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
        }

        public void DeleteAll(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated");
            }

            var userTodos = _context.TodoItems.Where(t => t.UserId == userId).ToList();

            _context.TodoItems.RemoveRange(userTodos);
            _context.SaveChanges();
        }
    }
}