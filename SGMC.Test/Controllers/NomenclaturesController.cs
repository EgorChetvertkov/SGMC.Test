using MediatR;

using Microsoft.AspNetCore.Mvc;

using SGMC.Test.Application.Links.Create;
using SGMC.Test.Application.Links.Delete;
using SGMC.Test.Application.Nomenclatures.Create;
using SGMC.Test.Application.Nomenclatures.Delete;
using SGMC.Test.Application.Nomenclatures.GetList;
using SGMC.Test.Application.Nomenclatures.GetOne;
using SGMC.Test.Application.Nomenclatures.Update;
using SGMC.Test.Common;
using SGMC.Test.Contracts;

namespace SGMC.Test.Controllers;
[Route("api/nomenklatures")]
[ApiController]
public class NomenclaturesController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await sender.Send(new GetNomenclaturesListRequest());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await sender.Send(new GetNomenclatureRequest(id));
        return result.Match(
            success => Ok(success),
            failed => failed.GetProblemDetails(this));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrUpdateNomenclature nomenclature)
    {
        var result = await sender.Send(new CreateNomenclatureRequest(
            nomenclature.Name,
            nomenclature.Price,
            nomenclature.Properties));
        return result.Match(
            success => Ok(success),
            failed => failed.GetProblemDetails(this));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, CreateOrUpdateNomenclature nomenclature)
    {
        var result = await sender.Send(new UpdateNomenclatureRequest(
            id,
            nomenclature.Name,
            nomenclature.Price,
            nomenclature.Properties));
        return result.Match(
            success => Ok(success),
            failed => failed.GetProblemDetails(this));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await sender.Send(new DeleteNomenclatureRequest(id));
        return result.Match(
            success => Ok(success),
            failed => failed.GetProblemDetails(this));
    }

    [HttpPost("link")]
    public async Task<IActionResult> CreateLink()
    {
        var result = await sender.Send(new CreateLinkRequest(0, 0, 0));
        return result.Match(
            success => Ok(success),
            failed => failed.GetProblemDetails(this));
    }

    [HttpDelete("link")]
    public async Task<IActionResult> DeleteLink()
    {
        var result = await sender.Send(new DeleteLinkRequest(0, 0));
        return result.Match(
            success => Ok(success),
            failed => failed.GetProblemDetails(this));
    }
}
