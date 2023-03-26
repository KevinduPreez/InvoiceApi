using InvoiceApi.Models;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceService _invoiceService;

    public InvoiceController(InvoiceService invoiceService) =>
        _invoiceService = invoiceService;

    [HttpGet]
    public async Task<List<Invoice>> Get() =>
        await _invoiceService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Invoice>> Get(string id)
    {
        var invoice = await _invoiceService.GetAsync(id);

        if (invoice is null)
        {
            return NotFound();
        }

        return invoice;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Invoice newInvoice)
    {
        await _invoiceService.CreateAsync(newInvoice);

        return CreatedAtAction(nameof(Get), new { id = newInvoice.Id }, newInvoice);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Invoice updatedInvoice)
    {
        var invoice = await _invoiceService.GetAsync(id);

        if (invoice is null)
        {
            return NotFound();
        }

        updatedInvoice.Id = invoice.Id;

        await _invoiceService.UpdateAsync(id, updatedInvoice);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var invoice = await _invoiceService.GetAsync(id);

        if (invoice is null)
        {
            return NotFound();
        }

        await _invoiceService.RemoveAsync(id);

        return NoContent();
    }
}