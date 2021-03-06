using Core.Domain.Entities.CatalogueItemAggregate;
using Core.Domain.Entities.CatalogueItemAggregate.Command.Delete;
using Core.Domain.Entities.CatalogueItemAggregate.Query.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Shop_Demo.Pages.Item
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;

        public DeleteModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public CatalogueItemDTO Item { get; set; }

        public long Id { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = (long)id;

            Item = await _mediator.Send(new GetItemByIdQuery((long)id));

            if (Item == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _mediator.Send(new DeleteItemCommand(Id));

            return RedirectToPage("./Index", result);
        }
    }
}
