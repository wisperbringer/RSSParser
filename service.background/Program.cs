using repository.operation;
using System;

namespace service.background
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceOperation = new SourceOperation();
            using (var db = new DBContext())
            {
                var sources = sourceOperation.GetSources();
                foreach (var source in sources)
                {
                    new ParserTask(source);
                }
            }

            Console.ReadLine();
        }
    }
}
