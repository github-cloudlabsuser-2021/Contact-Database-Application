using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;
 
namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static int free_id = 1;
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            return View(userlist);
        }
 
        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                // User not found, return a HttpNotFoundResult
                return HttpNotFound();
            }

            // User found, return it
            return View(user);
        }
 
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
 
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            user.Id = free_id;
            free_id++;
            userlist.Add(user);

            // redirect to the Details action to display the newly created user
            return RedirectToAction("Details", new { id = user.Id });
        }
 
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                // User not found, return a HttpNotFoundResult
                return HttpNotFound();
            }

            // User found, return it
            return View(user);
        }
 
        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.
            var existingUser = userlist.FindIndex(u => u.Id == id);
            if (existingUser == -1)
            {
                // User not found, return a HttpNotFoundResult
                return HttpNotFound();
            }
            userlist.RemoveAt(existingUser);
            user.Id = id;
            userlist.Add(user);

            return RedirectToAction("Details", new { id = user.Id });
        }
 
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // This method is responsible for displaying the view to delete an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Delete view.
            User user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                // User not found, return a HttpNotFoundResult
                return HttpNotFound();
            }

            // User found, return it
            return View(user);
        }
 
        // DELETE: User/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Implement the Delete method (POST) here
            // This method is responsible for handling the HTTP POST request to delete an existing user with the specified ID.
            // It removes the user from the userlist based on the provided ID.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            var existingUser = userlist.FindIndex(u => u.Id == id);
            if (existingUser == -1)
            {
                // User not found, return a HttpNotFoundResult
                return HttpNotFound();
            }

            userlist.RemoveAt(existingUser);

            return RedirectToAction("Index");
        }
    }
}
