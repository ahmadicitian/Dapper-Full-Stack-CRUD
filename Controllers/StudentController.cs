using DapperFullStack.Models;
using DapperFullStack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DapperFullStack.Controllers
{
    public class StudentController : Controller
    {
        private readonly DapperRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;
        public StudentController(DapperRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index(int id)
        {
            if(id == 0)
            {
                return View();
            }
            else
            {
                Student d = _repository.GetStudent(id);
                return View(d);
            
            }
        }
      
        public IActionResult List()
        {
            
            return View(_repository.GetStudents().ToList());
        }
        public IActionResult Save(Student student)
        {
            if(student.Id == 0)
            {
                student.Image = SaveImageByproduct(student.file);
                _repository.AddUser(student);
                return RedirectToAction("List");
            }

            else {
                student.Image = SaveImageByproduct(student.file);
                _repository.AddUser(student);
                return RedirectToAction("List");
            }

           
        }
       
        public async Task<IActionResult> Delete(int id)
        {
           await _repository.DeleteStudent(id);
            return RedirectToAction("List");
        }

       
        public string SaveImageByproduct(IFormFile File)
        {
            if (File != null)
            {


                string wwroot = _hostEnvironment.WebRootPath;
                string Ext = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
                string fileNamee = Path.Combine("https://localhost:7099" + "/StudentImages/", Ext);
                string imagepath = Path.Combine(wwroot, "StudentImages", Ext);
                using (var filestream = new FileStream(imagepath, FileMode.Create))
                {
                    File.CopyTo(filestream);
                }
                return fileNamee;
            }
            return string.Empty;
        }
    }
}
