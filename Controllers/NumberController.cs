using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Parcial1_P4_Chayanne.Models;
using Parcial1_P4_Chayanne.Services;
using SQLitePCL;

namespace Parcial1_P4_Chayanne.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class NumberController : ControllerBase
{
    [HttpGet("{Number}")]
    public IActionResult Index([FromRoute] int Number)
    {
        return Ok(Number + Number);
    }

    /*
    [HttpGet("{Number}")]
    public IActionResult Index([FromRoute] int Number)
    {
        NumberRecordSet number = new NumberRecordSet(Number, Number + Number);
        return Ok(number);
    }
    */

    private readonly NumberService _numberService;

    public NumberController(NumberService confle)
    {
        _numberService = confle;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var table = await _numberService.GetListAsync();
        return Ok(table);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        var row = await _numberService.GetByIdAsync(Id);
        if (row == null)
        {
            return NotFound(new {mensaje = "Registro ne encontrado."});
        }
        return Ok(row);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] NumberRecordSet number)
    {
        if (number == null) { return NotFound(); }

        bool save = await _numberService.SaveAsync(number);
        
        if (!save) { return StatusCode(500); }

        return Ok(new
        {
            Numero = number.Numero,
            Resultado = number.Resultado
        });
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> Update(int Id, [FromBody] NumberRecordSet number)
    {
        var row = await _numberService.GetByIdAsync(Id);

        if (row == null) { return NotFound(); }

        bool update = await _numberService.UpdateAsync(number);

        if (!update) { return StatusCode(500); }

        return Ok(new
        {
            Numero_Anterior = row.Numero,
            Resultado_Anterior = row.Resultado,
            Numero_Actual = number.Numero,
            Resultado_Actual = number.Resultado
        });
    }
}
