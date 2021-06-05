using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopsyTurvyCakes.Models;

namespace Topsy_Turvy_Cakes.Pages.Admin
{
    public class AddEditRecipeModel : PageModel
    {
        [FromRoute]
        public long? Id { get; set; }

        public bool IsNewRecipe => Id == null;

        public Recipe Recipe { get; set; }

        public void OnGet()
        {
        }
    }
}
