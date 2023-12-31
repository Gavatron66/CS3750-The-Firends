﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment1v3.Data;
using Assignment1v3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assignment1v3.Pages.Assignments
{
    [Authorize(Policy = "MustBeInstructor")]
    public class DeleteModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public DeleteModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        [BindProperty]
      public Assignment Assignment { get; set; } = default!;
        [BindProperty]
        public string courseId { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Assignment == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignment.FirstOrDefaultAsync(m => m.ID == id);

            if (assignment == null)
            {
                return NotFound();
            }
            else 
            {
                courseId = assignment.course.ToString();
                Assignment = assignment;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Assignment == null)
            {
                return NotFound();
            }
            var assignment = await _context.Assignment.FindAsync(id);
            courseId = assignment.course.ToString();

            if (assignment != null)
            {                
                Assignment = assignment;
                _context.Assignment.Remove(Assignment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./InstructorCourseView", new { CourseId= courseId });
        }
    }
}
