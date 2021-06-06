using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopsyTurvyCakes.Models;

namespace Topsy_Turvy_Cakes.Pages.Admin
{
    [Authorize]
    public class AddEditRecipeModel : PageModel
    {
        private readonly IRecipesService _recipesService;

        [FromRoute]
        public long? Id { get; set; }

        public bool IsNewRecipe => Id == null;

        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        public AddEditRecipeModel(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }
        public async Task OnGetAsync()
        {
            Recipe = await _recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recipe = await _recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
            recipe.Name = Recipe.Name;
            recipe.Description = Recipe.Description;
            recipe.Ingredients = Recipe.Ingredients;
            recipe.Directions = Recipe.Directions;
            if (Image is not null)
            {
                using (var stream = new MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    recipe.Image = stream.ToArray();
                    recipe.ImageContentType = Image.ContentType;
                }
            }
            await _recipesService.SaveAsync(recipe);
            return RedirectToPage("/Recipe", new { id = recipe.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await _recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}
