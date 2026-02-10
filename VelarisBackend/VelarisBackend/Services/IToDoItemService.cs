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
    }
}