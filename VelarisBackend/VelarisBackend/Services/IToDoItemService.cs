using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VelarisBackend.DTOs;
using VelarisBackend.Models;

namespace VelarisBackend.Services
{
    public interface IToDoItemService
    {
        TodoItem Create(string userId, ToDoItemAddReq req);
        TodoItem Edit(string userId, ToDoItemEditReq req);

        void Delete(string userId, int taskId);


        IEnumerable<TodoItem> GetAll(string userId);
        TodoItem GetOneTask(string userId, int taskId);
        void DeleteAll(string userId);
    }
}