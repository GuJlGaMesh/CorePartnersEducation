using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Linq
{
    public enum MarketObjectType
    {
        Building,
        Flat
    }

    public class MarketObject
    {
        public long Id { get; set; }
        public MarketObjectType Type { get; set; }
        public string CadastralNumber { get; set; }
        public decimal Price { get; set; }

        public MarketObject(MarketObjectType type)
        {
            Type = type;
            Id = new Random().Next();
            Console.WriteLine("Id:" + Id);
        }
    }

    public class Building : MarketObject
    {
        public List<Flat> Flats { get; set; }

        public Building() : base(MarketObjectType.Building)
        {
            Flats = new List<Flat>();
        }

        public void AddFlats(List<Flat> flats)
        {
            foreach (var flat in flats)
            {
                flat.BuildingId = Id;
                Flats.Add(flat);
            }
        }
    }

    public class Flat : MarketObject
    {
        public long BuildingId { get; set; }
        public Flat() : base(MarketObjectType.Flat){}

        public override string ToString()
        {
            return "CN:" + CadastralNumber + "; BuildingId:" + BuildingId + "; Price:" + Price;
        }
    }

    public class MarketEqualityComparer : IEqualityComparer<MarketObject>
    {
        public bool Equals(MarketObject x, MarketObject y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Type == y.Type && x.CadastralNumber == y.CadastralNumber;
        }

        public int GetHashCode(MarketObject obj)
        {
            return HashCode.Combine((int)obj.Type, obj.CadastralNumber);
        }
    }

    public class MarketCollection : Collection<MarketObject>
    {
        protected override void InsertItem(int index, MarketObject item)
        {
            Console.WriteLine("insert item");
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Console.WriteLine("delete item");
            base.RemoveItem(index);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*
            #region 2

            var collection = new MarketCollection();
            var comparer = new MarketEqualityComparer();
            var obj1 = new MarketObject(MarketObjectType.Building);
            var obj2 = new MarketObject(MarketObjectType.Building);
            var obj3 = new MarketObject(MarketObjectType.Building);
            var stack = new Stack<MarketObject>();
            var queue = new Queue<MarketObject>();
            var dictionary = new Dictionary<MarketObject, string>(comparer);
            var llist = new LinkedList<MarketObject>();
            var hashset = new HashSet<MarketObject>(comparer);
            
            collection.Insert(0, obj1);
            collection.Insert(1, obj2);
            collection.Insert(2, obj3);

            stack.Push(obj1);
            stack.Push(obj2);
            stack.Push(obj3);
            
            queue.Enqueue(obj1);
            queue.Enqueue(obj2);
            queue.Enqueue(obj3);

            dictionary[obj1] = "obj1";
            dictionary[obj2] = "obj2";
            dictionary[obj3] = "obj3";

            llist.AddLast(obj1);
            llist.AddLast(obj2);
            llist.AddLast(obj3);

            hashset.Add(obj1);
            hashset.Add(obj2);
            hashset.Add(obj3);

            Console.WriteLine("Search by Id:");
            var id = Console.ReadLine();
            long.TryParse(id,out var lId);
            if (lId > 0)
            {
                Console.WriteLine(stack.FirstOrDefault(x => x.Id == lId));
                Console.WriteLine(queue.FirstOrDefault(x => x.Id == lId));
                Console.WriteLine(dictionary.FirstOrDefault(x => x.Key.Id == lId));
                Console.WriteLine(hashset.FirstOrDefault(x => x.Id == lId));
                Console.WriteLine(llist.FirstOrDefault(x => x.Id == lId));
            }
            

            #endregion
            */

            #region 2
            var flat1 = new Flat { CadastralNumber = "f_1", Price = 0 };
            var flat2 = new Flat { CadastralNumber = "f_2", Price = 2 };
            var flat3 = new Flat { CadastralNumber = "f_3", Price = 3 };
            var flat4 = new Flat { CadastralNumber = "f_1", Price = 4 };
            var flat5 = new Flat { CadastralNumber = "f_5", Price = 5 };
            var flat6 = new Flat { CadastralNumber = "f_6", Price = 6 };

            var flats1 = new List<Flat>() {flat1,flat2,flat3};
            var flats2 = new List<Flat>() {flat4,flat5,flat6};
        

            var building1 = new Building() { CadastralNumber = "b_1", Flats = flats1 };
            var building2 = new Building() { CadastralNumber = "b_2", Flats = flats2 };

            var buildings = new List<Building> { building1, building2 };

            //1
            var cnList = buildings.Select(x => x.CadastralNumber);
            //2
            var allFlats = buildings.SelectMany(x => x.Flats);
            //3
            var cnAllFlats = allFlats.Select(x => x.CadastralNumber).ToList();
            cnAllFlats.ForEach(Console.WriteLine);
            //4
            Console.WriteLine("unique flats cn:");
            cnAllFlats.Distinct().ToList().ForEach(Console.WriteLine);
            //5
            Console.WriteLine("Min price:" + buildings.Min(x => x.Price));
            Console.WriteLine("Max price:" + buildings.Max(x => x.Price));
            Console.WriteLine("Avg price:" + buildings.Average(x => x.Price));
            //6
            if (allFlats.Any(p => p.Price == 0))
            {
                Console.WriteLine("There are flat with price equal 0");
            }
            //7
            if (allFlats.All(p => p.Price != 0))
            {
                Console.WriteLine("There are no flat with price equal 0");
            }
            //8
            allFlats.OrderBy(x => x.CadastralNumber).ThenBy(y => y.Price).ToList().ForEach(Console.WriteLine);
            //9
            var cnflats1 = building1.Flats.Select(x => x.CadastralNumber);
            var cnflats2 = building2.Flats.Select(x => x.CadastralNumber);

            Console.WriteLine("unique cn in flats1");
            cnflats1.Except(cnflats2).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("intersect cn in flats1 and flats2");
            cnflats1.Intersect(cnflats2).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("unique cn ");
            cnflats1.Union(cnflats2).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("concated cn");
            cnflats1.Concat(cnflats2).ToList().ForEach(Console.WriteLine);

            //10
            var marketObjects = buildings.Cast<MarketObject>();
            //11
            var groppedflats = allFlats.GroupBy((key) => key.CadastralNumber);

            #endregion

        }
    }
}
