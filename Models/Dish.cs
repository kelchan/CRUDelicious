// Disabled because we know the framework will assign non-null values when it
// constructs this class for us.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDelicious.Models;

// [NotMapped] taghelper will define a class/property to not be stored in your db
public class Dish
{
    [ Key ]
    public int DishId { get; set; }

    [ Required( ErrorMessage = "is required" ) ]
    [ Display( Name = "Name of Dish" ) ]
    public string Name { get; set; }

    [ Required( ErrorMessage = "is required" ) ]
    [ Display( Name = "Chef's Name" ) ]
    public string Chef { get; set; }

    [ Required( ErrorMessage = "is required" ) ]
    [ Display( Name = "Tastiness" ) ]
    public int Tastiness { get; set; }

    [ Required( ErrorMessage = "must be more than 0 calories" ) ]
    // [ MinLength( 0, ErrorMessage = "must be more than 0 calories" ) ]
    [ Display( Name = "# of Calories" ) ]
    public int Calories { get; set; }

    [ Required( ErrorMessage = "is required" ) ]
    [ Display( Name = "Description" ) ]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}