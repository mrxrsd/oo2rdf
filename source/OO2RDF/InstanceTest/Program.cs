using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using OO2RDF;
using OO2RDF.Framework.Serializer;

namespace InstanceTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Person a = new Person();
            a.Name = "James";
            a.Age = 26;

            Person b = new Person();
            b.Name = "Jack";
            b.Age = 30;

            Person c = new Person();
            c.Name = "Amanda";
            c.Age = 19;


            a.Knows = new List<Person>();
            a.Knows.Add(b);
            a.Knows.Add(c);

            c.Knows = new List<Person>();
            c.Knows.Add(a);

            b.Knows = new List<Person>();
            b.Knows.Add(a);



            List<object> l = new List<object>();
            l.Add(a);
            l.Add(c);
           


            FileInfo f = new FileInfo(@"c:\test.txt");

            OO2RDFOptions opcoes = new OO2RDFOptions(new Uri("http://www.base.com"),  f, new NTriplesSerializer());

            OO2RDFMapper o = new OO2RDFMapper(Assembly.GetExecutingAssembly(), l, opcoes);

            o.GenerateTriples();
        }
    }
}
