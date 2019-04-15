using System;
using System.Collections.Generic;
using keepr.Repositories;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsRepository _kr;
    public KeepsController(KeepsRepository kr)
    {
      _kr = kr;
    }

    //getall
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      IEnumerable<Keep> results = _kr.GetAll();
      if (results == null)
      {
        return BadRequest();
      }
      return Ok(results);
    }

    //GetByID
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      Keep found = _kr.GetById(id);
      if (found == null) { return BadRequest(); }
      return Ok(found);
    }

    //create
    [HttpPost]
    public ActionResult<Keep> Create([FromBody] Keep keep)
    {
      Keep newKeep = _kr.CreateKeep(keep);
      if (newKeep == null) { return BadRequest("Keep was not created"); }
      return Ok(newKeep);
    }

    //Delete
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      bool successful = _kr.Delete(id);
      if (!successful) { return BadRequest(); }
      return Ok();
    }






  }
}