﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
//using GR30321.API.Data;
using GR30321.Domain.Entities;
using GR30321.TrusN.UI.Services.BookService;

namespace GR30321.TrusN.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;

        public DetailsModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public Book Book { get; set; } = default!;
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
                if (id == null)
                {
                    return NotFound();
                }
                var response = await _bookService.GetBookByIdAsync(id.Value);
                if (response == null)
                {
                    return NotFound();
                }
                if (response.Success)
                {
                    if (response.Data == null)
                    {
                        return NotFound();
                    }
                    Book = response.Data;
                }
                else
                {
                    ErrorMessage = response.ErrorMessage ?? "Unknown error.";
                    return Page();
                }
                return Page();
            
        }
    }
}
