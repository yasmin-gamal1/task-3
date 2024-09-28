using System;
using System.Collections.Generic;


public class Movie
{
    private string title;
    private string genre;
    private bool isAvailable;

    public string Title { get { return title; } }
    public string Genre { get { return genre; } }
    public bool IsAvailable { get { return isAvailable; } }

    public Movie(string title, string genre)
    {
        this.title = title;
        this.genre = genre;
        this.isAvailable = true;
    }

    public bool CheckAvailability()
    {
        return isAvailable;
    }

    public bool RentMovie()
    {
        if (isAvailable)
        {
            isAvailable = false;
            return true;
        }
        return false;
    }

    public void ReturnMovie()
    {
        isAvailable = true;
    }
}


public class Customer
{
    private string name;
    private List<Movie> rentedMovies;
    private Membership membership;

    public string Name { get { return name; } }
    public List<Movie> RentedMovies { get { return rentedMovies; } }
    public Membership Membership { get { return membership; } }

    public Customer(string name, Membership membership)
    {
        this.name = name;
        this.membership = membership;
        rentedMovies = new List<Movie>();
    }

    public bool RentMovie(Movie movie)
    {
        if (rentedMovies.Count < membership.MaxRentals && movie.CheckAvailability())
        {
            movie.RentMovie();
            rentedMovies.Add(movie);
            Console.WriteLine($"{name} rented {movie.Title}");
            return true;
        }
        else if (rentedMovies.Count >= membership.MaxRentals)
        {
            Console.WriteLine($"{name} has reached the rental limit of {membership.MaxRentals} movies.");
        }
        else
        {
            Console.WriteLine($"{movie.Title} is not available for rent.");
        }
        return false;
    }

    public void ReturnMovie(Movie movie)
    {
        if (rentedMovies.Contains(movie))
        {
            movie.ReturnMovie();
            rentedMovies.Remove(movie);
            Console.WriteLine($"{name} returned {movie.Title}");
        }
        else
        {
            Console.WriteLine($"{name} has not rented {movie.Title}");
        }
    }
}


public class Membership
{
    private string type;
    private int maxRentals;

    public string Type { get { return type; } }
    public int MaxRentals { get { return maxRentals; } }

    public Membership(string type, int maxRentals)
    {
        this.type = type;
        this.maxRentals = maxRentals;
    }

    public string GetMembershipInfo()
    {
        return $"Membership Type: {type}, Max Rentals Allowed: {maxRentals}";
    }

    public bool CanRentMoreMovies(int currentRentals)
    {
        return currentRentals < maxRentals;
    }
}


public class Payment
{
    private float amount;
    private DateTime date;

    public float Amount { get { return amount; } }
    public DateTime Date { get { return date; } }

    public Payment(float amount)
    {
        this.amount = amount;
        this.date = DateTime.Now;
    }

    public void ProcessPayment()
    {
        Console.WriteLine($"Payment of {amount} processed on {date}");
    }
}


public class Rental
{
    private Movie movie;
    private Customer customer;
    private DateTime rentalDate;
    private Payment payment;

    public Rental(Movie movie, Customer customer, float rentalCost)
    {
        this.movie = movie;
        this.customer = customer;
        this.rentalDate = DateTime.Now;
        this.payment = new Payment(rentalCost);
    }

    public void CreateRental()
    {
        if (customer.RentMovie(movie))
        {
            payment.ProcessPayment();
            Console.WriteLine($"Rental created: {customer.Name} rented {movie.Title} on {rentalDate}");
        }
        else
        {
            Console.WriteLine("Rental transaction failed.");
        }
    }

    public void CompleteRental()
    {
        customer.ReturnMovie(movie);
        Console.WriteLine($"Rental completed: {customer.Name} returned {movie.Title}");
    }
}


class Program
{
    static void Main(string[] args)
    {
       
        Membership regularMembership = new Membership("Regular", 2);
        Membership premiumMembership = new Membership("Premium", 5);

        
        Movie movie1 = new Movie("Inception", "Sci-Fi");
        Movie movie2 = new Movie("The Godfather", "Crime");
        Movie movie3 = new Movie("The Dark Knight", "Action");

       
        Customer customer1 = new Customer("John Doe", regularMembership);
        Customer customer2 = new Customer("Jane Smith", premiumMembership);

        
        Rental rental1 = new Rental(movie1, customer1, 5.99f);
        Rental rental2 = new Rental(movie2, customer2, 7.99f);

        
        rental1.CreateRental();
        rental2.CreateRental();

        
        Rental rental3 = new Rental(movie3, customer1, 4.99f);
        rental3.CreateRental();

        
        rental1.CompleteRental();

     
        rental3.CreateRental();

        Console.ReadLine();
    }
}
