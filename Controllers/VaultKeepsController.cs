






using System.Collections;
using System.Collections.Generic;
using keepr.Repositories;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsRepository _vkr;
    public VaultKeepsController(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }


    //GETBYID
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Keep>> Get(int id)
    {
      IEnumerable<Keep> found = _vkr.GetByVaultId(id);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }



    //CREATE
    [HttpPost]
    public ActionResult<Vaultkeep> Create([FromBody] Vaultkeep vaultkeep)
    {
      Vaultkeep newVaultkeep = _vkr.CreateVaultkeep(vaultkeep);
      if (newVaultkeep == null) { return BadRequest("vaultkeep not created"); }
      return Ok(newVaultkeep);
    }


    //DELETE
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _vkr.Delete(id);
      if (!successful) { return BadRequest(); }
      return Ok();
    }

  }
}