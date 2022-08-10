using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
namespace CRUDelicious.Controllers;

public class DishController : Controller
{
    private CRUDeliciousContext _context;
    // here we can "inject" our context service into the constructor
    public DishController(CRUDeliciousContext context)
    {
        _context = context;
    }

    [ HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.ToList();
        return View( "All", AllDishes );
    }

    [ HttpGet( "/dish/new" ) ]
    public IActionResult New()
    {
        return View( "New" );
    }

    [ HttpPost( "/dish/create" ) ]
    public IActionResult Create( Dish newDish )
    {
        if( ModelState.IsValid == false )
        {
            return New();
        }
        _context.Dishes.Add( newDish );
        _context.SaveChanges();
        return Index();
    }

    [ HttpGet( "/dish/{dishId}" ) ]
    public IActionResult ViewDish( int dishId )
    {
        Dish? dish = _context.Dishes.FirstOrDefault( dish => dish.DishId == dishId );
        if( dish == null )
        {
            return Index();
        }
        return View( "ViewDish", dish );
    }

    [ HttpPost( "/dish/{deletedDishId}/delete" ) ]
    public IActionResult Delete( int deletedDishId )
    {
        Dish? dishToBeDeleted = _context.Dishes.FirstOrDefault( dish => dish.DishId == deletedDishId );
        if( dishToBeDeleted != null )
        {
            _context.Dishes.Remove( dishToBeDeleted );
            _context.SaveChanges();
        }
        return Index();
    }

    [ HttpPost( "/dish/{dishToBeEdited}/edit" ) ]
    public IActionResult EditDish( int dishToBeEdited )
    {
        Dish? dish = _context.Dishes.FirstOrDefault( dish => dish.DishId == dishToBeEdited );
        if( dish == null )
        {
            return Index();
        }
        return View( "Edit", dish );
    }

    [ HttpPost( "/dish/{updatedDishId}/update" ) ]
    public IActionResult UpdateDish( int updatedDishId, Dish updatedDish )
    {
        if( ModelState.IsValid == false )
        {
            return EditDish( updatedDishId );
        }
        Dish? dbDish = _context.Dishes.FirstOrDefault( dish => dish.DishId == updatedDishId );
        if( dbDish == null )
        {
            return Index();
        }
        dbDish.Name = updatedDish.Name;
        dbDish.Chef = updatedDish.Chef;
        dbDish.Tastiness = updatedDish.Tastiness;
        dbDish.Calories = updatedDish.Calories;
        dbDish.Description = updatedDish.Description;
        dbDish.UpdatedAt = DateTime.Now;

        _context.Dishes.Update( dbDish );
        _context.SaveChanges();

        return RedirectToAction( "ViewPost", new { dishId = dbDish.DishId } );
    }

}