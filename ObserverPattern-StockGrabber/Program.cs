using System;
using System.Collections.Generic;

namespace ObserverPattern_StockGrabber
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create the Subject object
            // It will handle updating all observers 
            // as well as deleting and adding them
            StockGrabber stockGrabber = new StockGrabber();

            // Create an Observer that will be sent updates from Subject
            Console.WriteLine("Create an Observer that will be sent updates from Subject");
            StockObserver observer1 = new StockObserver(stockGrabber);

            Console.WriteLine("Update the stock prices");
            stockGrabber.SetIBMPrice(197.00);
            stockGrabber.SetAAPLPrice(677.60);
            stockGrabber.SetGOOGPrice(676.40);

            Console.WriteLine("Create a second Observer that will be sent updates from Subject");
            StockObserver observer2 = new StockObserver(stockGrabber);

            Console.WriteLine("Update the stock prices");
            stockGrabber.SetIBMPrice(197.00);
            stockGrabber.SetAAPLPrice(677.60);
            stockGrabber.SetGOOGPrice(676.40);

            // Delete one of the observers
            Console.WriteLine("Delete Observer2");
            stockGrabber.Unregister(observer2);

            Console.WriteLine("Update the stock prices");
            stockGrabber.SetIBMPrice(197.00);
            stockGrabber.SetAAPLPrice(677.60);
            stockGrabber.SetGOOGPrice(676.40);

            Console.ReadLine();
        }
    }


    // This interface handles adding, deleting and updating
    // all observers 
    public interface Subject
    {
        void Register(Observer o);
        void Unregister(Observer o);
        void NotifyObserver();
    }

    // The Observers update method is called when the Subject changes
    public interface Observer
    {
        void Update(double ibmPrice, double aaplPrice, double googPrice);
    }

    // Uses the Subject interface to update all Observers
    public class StockGrabber : Subject
    {
        private List<Observer> observers;
        private double ibmPrice;
        private double aaplPrice;
        private double googPrice;

        public StockGrabber()
        {
            // Creates an ArrayList to hold all observers
            observers = new List<Observer>();
        }

        public void Register(Observer newObserver)
        {
            // Adds a new observer to the ArrayList
            observers.Add(newObserver);
        }

        public void Unregister(Observer deleteObserver)
        {
            // Removes observer from the ArrayList
            observers.Remove(deleteObserver);
        }

        public void NotifyObserver()
        {
            // Cycle through all observers and notifies them of
            // price changes
            foreach (Observer observer in observers)
                observer.Update(ibmPrice, aaplPrice, googPrice);
        }

        // Change prices for all stocks and notifies observers of changes
        public void SetIBMPrice(double newIBMPrice)
        {
            this.ibmPrice = newIBMPrice;
            NotifyObserver();
        }

        public void SetAAPLPrice(double newAAPLPrice)
        {
            this.aaplPrice = newAAPLPrice;
            NotifyObserver();
        }

        public void SetGOOGPrice(double newGOOGPrice)
        {
            this.googPrice = newGOOGPrice;
            NotifyObserver();
        }

    }

    // Represents each Observer that is monitoring changes in the subject
    public class StockObserver : Observer
    {
        private double ibmPrice;
        private double aaplPrice;
        private double googPrice;

        // Static used as a counter
        private static int observerIDTracker = 0;

        // Used to track the observers
        private int observerID;

        // Will hold reference to the StockGrabber object
        private Subject stockGrabber;

        public StockObserver(Subject stockGrabber)
        {
            // Store the reference to the stockGrabber object so
            // I can make calls to its methods
            this.stockGrabber = stockGrabber;

            // Assign an observer ID and increment the static counter
            this.observerID = ++observerIDTracker;

            // Message notifies user of new observer
            Console.WriteLine($"New Observer {this.observerID}");

            // Add the observer to the Subjects ArrayList
            stockGrabber.Register(this);
        }

        // Called to update all observers
        public void Update(double ibmPrice, double aaplPrice, double googPrice)
        {
            this.ibmPrice = ibmPrice;
            this.aaplPrice = aaplPrice;
            this.googPrice = googPrice;

            PrintThePrices();
        }

        public void PrintThePrices()
        {
            Console.WriteLine($"{observerID} \nIBM: {ibmPrice} \nAAPL: {aaplPrice} \nGOOG: {googPrice}\n");
        }

    }

}
