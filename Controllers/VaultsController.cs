using System;
using System.Collections.Generic;
using keepr.Repositories;
using Keepr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsRepository _vr;
    public VaultsController(VaultsRepository vr)
    {
      _vr = vr;
    }

    //GETALL
    [HttpGet]
    [Authorize]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      string userId = HttpContext.User.Identity.Name;
      IEnumerable<Vault> results = _vr.GetAll(userId);
      if (results == null)
      {
        return BadRequest();
      }
      return Ok(results);
    }

    //GetByID
    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      Vault found = _vr.GetById(id);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }

    //GETKeepsbyVaultId
    [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeeps(int id)
    {

      return Ok(_vr.GetKeeps(id));
    }

    //CREATE
    [HttpPost]
    [Authorize]
    public ActionResult<Vault> Create([FromBody] Vault vault)
    {
      vault.UserId = HttpContext.User.Identity.Name;
      Vault newVault = _vr.CreateVault(vault);
      if (newVault == null) { return BadRequest("Vault not created"); }
      return Ok(newVault);
    }

    //Delete
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _vr.Delete(id, HttpContext.User.Identity.Name);
      if (!successful) { return BadRequest(); }
      return Ok();
    }

  }
}