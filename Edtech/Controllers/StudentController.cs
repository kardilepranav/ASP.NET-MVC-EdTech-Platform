using Edtech.Data;
using Edtech.DTOs;
using Edtech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;

namespace Edtech.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class StudentController : Controller
    {
        private AppDbContext _myApDbContext;
        private IWebHostEnvironment _webHostEnvironment;

        public StudentController(AppDbContext myApDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _myApDbContext = myApDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            var students = _myApDbContext.Students2.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentImage image, StudentImage video1, StudentImage video2, StudentImage video3, StudentImage video4, StudentImage video5)
        {
            string filename = Upload(image);
            string videofile1 = UploadVideo1(video1);
            string videofile2 = UploadVideo2(video2);
            string videofile3 = UploadVideo3(video3);
            string videofile4 = UploadVideo4(video4);
            string videofile5 = UploadVideo5(video5);

            var student = new Student2()
            {
                CourseTitle = image.CourseTitle,
                InstructorName = image.InstructorName,
                Description = image.Description,
                Duration = image.Duration,
                Price = image.Price,
                ProfileImage = filename,
                ProfileVideo1 = videofile1,
                ProfileVideo2 = videofile2,
                ProfileVideo3 = videofile3,
                ProfileVideo4 = videofile4,
                ProfileVideo5 = videofile5,
            };

            _myApDbContext.Add(student);
            _myApDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _myApDbContext.Students2.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _myApDbContext.Students2.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Delete the associated profile image file
            if (!string.IsNullOrEmpty(student.ProfileImage))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", student.ProfileImage);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _myApDbContext.Students2.Remove(student);
            await _myApDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _myApDbContext.Students2 == null)
            {
                return NotFound();
            }

            var student2 = await _myApDbContext.Students2.FindAsync(id);
            if (student2 == null)
            {
                return NotFound();
            }
            return View(student2);
        }
        // GET: Student2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _myApDbContext.Students2 == null)
            {
                return NotFound();
            }

            var student2 = await _myApDbContext.Students2
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student2 == null)
            {
                return NotFound();
            }

            return View(student2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseTitle,InstructorName,Description,Duration,Price,ProfileImage")] Student2 student2, string originalProfileImage)
        {
            if (id != student2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // If the ProfileImage is not modified, use the original path
                    if (string.IsNullOrEmpty(student2.ProfileImage))
                    {
                        student2.ProfileImage = originalProfileImage;
                    }

                    _myApDbContext.Update(student2);
                    await _myApDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Student2Exists(student2.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student2);
        }


        private bool Student2Exists(int id)
        {
            return (_myApDbContext.Students2?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private string Upload(StudentImage image)
        {
            string filename = null;
            if (image.ProfileImage != null)
            {
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                filename = Guid.NewGuid().ToString() + "-" + image.ProfileImage.FileName;
                string filepath = Path.Combine(uploaddir, filename);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    image.ProfileImage.CopyTo(filestream);
                }
            }
            else
            {
                filename = "Empty_Content";
            }
            return filename;
        }

        private string UploadVideo1(StudentImage video)
        {
            string videofile1 = null;
            if (video.ProfileVideo1 != null)
            {
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "video");
                //  videofile1 = Guid.NewGuid().ToString() + "-" + video.ProfileVideo1.FileName;
                videofile1 = video.ProfileVideo1.FileName;
                string filepath = Path.Combine(uploaddir, videofile1);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    video.ProfileVideo1.CopyTo(filestream);
                }
            }
            else
            {
                videofile1 = "Empty_Content";
            }
            return videofile1;
        }
        private string UploadVideo2(StudentImage video)
        {
            string videofile2 = null;
            if (video.ProfileVideo2 != null)
            {
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "video");
                //videofile2 = Guid.NewGuid().ToString() + "-" + video.ProfileVideo2.FileName;
                videofile2 = video.ProfileVideo2.FileName;
                string filepath = Path.Combine(uploaddir, videofile2);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    video.ProfileVideo2.CopyTo(filestream);
                }
            }
            else
            {
                videofile2 = "Empty_Content";
            }
            return videofile2;
        }
        private string UploadVideo3(StudentImage video)
        {
            string videofile3 = null;
            if (video.ProfileVideo3 != null)
            {
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "video");
                //videofile3 = Guid.NewGuid().ToString() + "-" + video.ProfileVideo3.FileName;
                videofile3 = video.ProfileVideo3.FileName;
                string filepath = Path.Combine(uploaddir, videofile3);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    video.ProfileVideo3.CopyTo(filestream);
                }
            }
            else
            {
                videofile3 = "Empty_Content";
            }
            return videofile3;
        }

        private string UploadVideo4(StudentImage video)
        {
            string videofile4 = null;
            if (video.ProfileVideo4 != null)
            {
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "video");
                // videofile4 = Guid.NewGuid().ToString() + "-" + video.ProfileVideo4.FileName;
                videofile4 = video.ProfileVideo4.FileName;
                string filepath = Path.Combine(uploaddir, videofile4);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    video.ProfileVideo4.CopyTo(filestream);
                }
            }
            else
            {
                videofile4 = "Empty_Content";
            }
            return videofile4;
        }
        private string UploadVideo5(StudentImage video)
        {
            string videofile5 = null;
            if (video.ProfileVideo5 != null)
            {
                string uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "video");
                // videofile5 = Guid.NewGuid().ToString() + "-" + video.ProfileVideo5.FileName;
                videofile5 = video.ProfileVideo5.FileName;
                string filepath = Path.Combine(uploaddir, videofile5);

                using (var filestream = new FileStream(filepath, FileMode.Create))
                {
                    video.ProfileVideo5.CopyTo(filestream);
                }
            }
            else
            {
                videofile5 = "Empty_Content";
            }

            return videofile5;
        }


    }
}
