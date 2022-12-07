using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.entwicklerheld
{
    public class FlightService
    {
        private IEnumerable<Flight> availableFlights;

        public static FlightService Of(IEnumerable<Flight> availableFlights)
        {
            return new FlightService(availableFlights);
        }

        private FlightService(IEnumerable<Flight> availableFlights)
        {
            this.availableFlights = availableFlights;
        }

        public IEnumerable<Flight> GetFullFlights()
        {
            // Implement this
            // You can also add more methods or functions
            // Try to write clean code.

            return availableFlights
                .ToList()
                .Where(x => x.AvailableSeats < 1);

            //throw new NotImplementedException();
        }

        public IEnumerable<Flight> GetFlightsForDestination(Airport destination)
        {
            // Implement this
            return availableFlights.
                Where(flight => flight.Destination == destination);
            //throw new NotImplementedException();
        }

        public IEnumerable<Flight> GetFlightsForOrigin(Airport origin)
        {
            // Implement this
            return availableFlights.
                Where(flight => flight.Origin == origin)
                .ToList();
            //throw new NotImplementedException();
        }

        public List<Flight> GetShortestFlightByRoute(Airport origin, Airport destination)
        {
            var data = availableFlights.ToList();
            var fF = new FastFlights(data);
            var pFlights = GetFlightsForOrigin(origin)
                        .Where(x => x.AvailableSeats > 0)
                        .Select(x =>
                        new FlightPlan(origin, destination, new List<Flight>() { x }, fF));

            fF.pFlights = pFlights.ToList();

            var result = fF.GetShortest();

            if (!result.Any())
                return new List<Flight>();
            return result.First().route;
        }
    }

    // after googling algorythm
    public class Dijkstra
    {
        // how to implement visited
        public Flight currentNode;
        public PriorityQueue<Flight, TimeSpan> priorityQueue = new();
        public Dictionary<Flight, Flight> prevNodes = new();
        public Dictionary<Flight, TimeSpan> totalCost = new();
        public HashSet<Flight> visitedNodes = new();
        public List<Flight> allFlight;
        public Airport start;
        public Airport goal;

        public Dijkstra(List<Flight> aV, Airport s, Airport g)
        {
            start = s;
            goal = g;
            allFlight = aV;
            foreach (var f in aV.Where(x => x.Origin == s)) 
                priorityQueue.Enqueue(f, f.Duration);
            currentNode = priorityQueue.Peek();
            foreach (var f in aV) totalCost.Add(f, TimeSpan.MaxValue);
        }

        public List<Flight> DoDijsktraMagic()
        {
            while (priorityQueue.Count != 0 && currentNode.Destination != goal)
            {
                currentNode = priorityQueue.Dequeue();
                UpdateQueues();
            }

            List<Flight> path = new();
            var cN = currentNode;
            if (cN.Destination != goal)
                return path;
            while(cN.Origin != start)
            {
                path.Add(cN);
                cN = prevNodes.Single(x => x.Key.Equals(cN)).Value;
            }
            path.Reverse();
            return path;
        } 

        public void UpdateQueues()
        {
            visitedNodes.Add(currentNode);
            var cWeight = totalCost.Single(x => x.Key.Equals(currentNode)).Value;
            foreach (var node in GetFlights())
            {
                var newWeight = cWeight + node.Duration;

                if (visitedNodes.Any(x => x.Equals(node)))
                    continue;

                if (totalCost.Any(x => x.Key.Equals(node))) {
                    if (newWeight < totalCost[node])
                    {
                        Console.WriteLine("node of totalcost1 works");
                        totalCost[node] = newWeight;
                        prevNodes[node] = currentNode;
                        priorityQueue.Enqueue(node, newWeight);
                    }
                }
                else
                {
                    priorityQueue.Enqueue(node, newWeight);
                    prevNodes.Add(node, currentNode);
                    totalCost[node] = newWeight;
                }
            }            
        }

        public List<Flight> GetFlights()
        {
            return allFlight
                .Where(x => x.Origin == currentNode.Destination)
                .ToList();
        }
    }

    //test without google algorythms 
    public class FlightPlan
    {
        public List<Flight> route { get; set; }
        public Airport aOrigin { get; }
        public Airport aDestination { get; }
        public bool isAtDestination { get; set; }
        public FastFlights AllFlights { get; set; }
        public FlightPlan(Airport o, Airport d, List<Flight> startFlight, FastFlights a)
        {
            aOrigin = o;
            aDestination = d;
            isAtDestination = false;
            route = startFlight;
            AllFlights = a;
        }
        public string TString()
        {
            return string.Join(", ", route.Select(x => x.Destination));
        }

        public TimeSpan GetCurrentTime()
        {
            return route.Select(x => x.Duration).Aggregate((a, b) => a + b);
        }
        public Airport CurrentAirport()
        {
            return route.Last().Destination;
        }

        public void OneIteration(List<Flight> cFlights)
        {
            var cTime = GetCurrentTime();
            if (cFlights.Count == 0 || isAtDestination) return;

            foreach (var f in cFlights)
            {
                if (f.AvailableSeats < 1)
                    continue;
                if (cTime + f.Duration > AllFlights.shortestTime)
                    continue;
                var newRoute = route.ToList();
                newRoute.Add(f);
                AllFlights.pFlights.Add(new FlightPlan(aOrigin, aDestination, newRoute, AllFlights));
            }

            AllFlights.pFlights.Remove(this);
        }
        public void CheckMe()
        {
            if (CurrentAirport() == aDestination)
            {
                isAtDestination = true;
                if (GetCurrentTime() <= AllFlights.shortestTime)
                    AllFlights.shortestTime = GetCurrentTime();
                else
                    AllFlights.pFlights.Remove(this);
                return;
            }
            if (GetCurrentTime() > AllFlights.shortestTime)
            {
                AllFlights.pFlights.Remove(this);
            }
        }
    }

    public class FastFlights
    {
        public TimeSpan shortestTime { get; set; }
        public List<FlightPlan> pFlights { get; set; }
        public List<Flight> avFlights { get; set; }
        public int iteration { get; set; }

        public FastFlights(List<Flight> aV)
        {
            shortestTime = TimeSpan.FromHours(24);
            iteration = 1;
            avFlights = aV;
        }

        public List<FlightPlan> GetShortest()
        {
            var cList = pFlights.ToList();

            while (pFlights.Count > 1)
            {
                foreach (var item in cList)
                {
                    item.OneIteration(possFlights(item.CurrentAirport()));
                }
                cList = pFlights.ToList();
                foreach (var item in cList)
                {
                    item.CheckMe();
                }
                cList = pFlights.ToList();
                // warum wird shortest time nicht gelogt mit 24 h ??????? 
                Console.WriteLine(shortestTime);
            }
            //pFlights.ForEach(x => Console.WriteLine(x.TString()));
            var output = pFlights.Where(x => x.isAtDestination).ToList();
            Console.WriteLine(pFlights[0].aOrigin.Name + " - " + pFlights[0].aDestination.Name);
            Console.WriteLine(pFlights.Count);
            Console.WriteLine(pFlights[0].isAtDestination);
            return output;
        }

        public List<Flight> possFlights(Airport o)
        {
            return avFlights.Where(x => x.Origin == o).ToList();
        }
    }
    public class Flight
    {

        public Flight(Airport origin, Airport destination, TimeSpan duration, int availableSeats)
        {
            Origin = origin;
            Destination = destination;
            Duration = duration;
            AvailableSeats = availableSeats;
        }

        public Airport Origin { get; set; }
        public Airport Destination { get; set; }
        public int AvailableSeats { get; set; }
        public TimeSpan Duration { get; set; }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Flight flight = (Flight)obj;
                return (Destination == flight.Destination) && (Origin == flight.Origin) && (AvailableSeats == flight.AvailableSeats);
            }
        }

        public override int GetHashCode()
        {
            return Origin.GetHashCode() ^ Destination.GetHashCode() ^ AvailableSeats.GetHashCode();
        }

        public override string ToString()
        {
            return $"\n\tFrom: {Origin.Name}\n\tTo: {Destination.Name}\n\tAvailableSeats: {AvailableSeats}\n";
        }
    }

    public class Airport
    {
        public string Name { get; set; }
    }
}
