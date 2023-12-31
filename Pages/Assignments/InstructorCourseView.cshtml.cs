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
using Microsoft.AspNetCore.Authentication;


namespace Assignment1v3.Pages.Assignments
{
    [Authorize(Policy = "MustBeInstructor")]
    public class InstructorCourseViewModel : PageModel
    {
        private readonly Assignment1v3.Data.Assignment1v3Context _context;

        public InstructorCourseViewModel(Assignment1v3.Data.Assignment1v3Context context)
        {
            _context = context;
        }

        public IList<Assignment> Assignments { get; set; } = new List<Assignment>();
        public Course SelectedCourse { get; set; }
        public List<Assignment> Assignment { get; private set; }

        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            // Check if assignments exist
            if (_context.Assignment != null)
            {
                Assignment = await _context.Assignment.ToListAsync();
            }

            SelectedCourse = await _context.Course
                .Where(c => c.Id == courseId)
                .FirstOrDefaultAsync();

            if (SelectedCourse == null)
            {
                // Course not found, handle accordingly (e.g., return a not found page).
                return NotFound();
            }

            Assignment = await _context.Assignment
                .Where(a => a.course == SelectedCourse.Id) // Assuming 'course' is now an int in the Assignment model
                .ToListAsync();

            return Page();
        }
    }
}
