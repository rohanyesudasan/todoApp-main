using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todoApp.Models;

namespace todoApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
// private static List<TodoItem> todoItems = new List<TodoItem>();
  private static List<TodoItem> todoItems = new List<TodoItem>
    {
        new TodoItem { ID = 1, Title = "Sample ToDo 1", Description="bhbh" , IsDone = false },
        new TodoItem { ID = 2, Title = "Sample ToDo 2",Description="bhbh" , IsDone = false },
    };
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // Action to display the ToDo list on the home screen
    public ActionResult Index()
    {
        return View(todoItems);
    }

    // Action to mark a task as done
    public ActionResult MarkAsDone(int id)
    {
        var todoItem = todoItems.Find(item => item.ID == id);
        if (todoItem != null)
        {
            todoItem.IsDone = true;
        }
        return RedirectToAction("Index");
    }

    // Action to delete a task
    public ActionResult Delete(int id)
    {
        var todoItem = todoItems.Find(item => item.ID == id);
        if (todoItem != null)
        {
            todoItems.Remove(todoItem);
        }
        return RedirectToAction("Index");
    }

    // Action to create a new task (not requested but added for completeness)
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(TodoItem newItem)
    {
        if (ModelState.IsValid)
        {
            // Assign a unique ID to the new item
            int newId = todoItems.Count > 0 ? todoItems.Max(item => item.ID) + 1 : 1;
            newItem.ID = newId;
            todoItems.Add(newItem);
            return RedirectToAction("Index");
        }
        return View(newItem);
    }
}
