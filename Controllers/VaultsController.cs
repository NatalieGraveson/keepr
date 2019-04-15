using System;
using System.Collections.Generic;
using keepr.Repositories;
using Keepr.Models;
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
    public ActionResult<IEnumerable<Vault>> Get()
    {
      IEnumerable<Vault> results = _vr.GetAll();
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
    public ActionResult<Vault> Create([FromBody] Vault vault)
    {
      Vault newVault = _vr.CreateVault(vault);
      if (newVault == null) { return BadRequest("Vault not created"); }
      return Ok(newVault);
    }

    //Delete
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _vr.Delete(id);
      if (!successful) { return BadRequest(); }
      return Ok();
    }

  }
}