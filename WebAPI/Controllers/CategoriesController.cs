using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("getall")]
    public IActionResult GetList()
    {
        var result = _categoryService.GetList();
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Message);
    }

    [HttpGet("getbyId")]
    public IActionResult GetById(int categoryId)
    {
        var result = _categoryService.GetById(categoryId);
        if (result.Success)
        {
            return Ok(result.Message);
        }

        return BadRequest(result.Message);
    }

    [HttpPost("add")]
    public IActionResult Add(Category category)
    {

        var result = _categoryService.Add(category);
        return result.Success ? 
            Ok(result.Message):
            BadRequest(result.Message);
    }

    [HttpPut("update")]
    public IActionResult Update(Category category)
    {
        var result = _categoryService.Update(category);
        return result.Success ? 
            Ok(result.Message) : 
            BadRequest(result.Message);
    }

    [HttpDelete("delete")]
    public IActionResult Delete(Category category)
    {
        var result = _categoryService.Delete(category);
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }
}