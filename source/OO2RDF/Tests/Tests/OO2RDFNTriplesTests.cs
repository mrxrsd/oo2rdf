using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AssemblySampleTest;
using NUnit.Framework;
using OO2RDF;
using OO2RDF.Framework.Mapper;
using OO2RDF.Framework.Serializer;

namespace Tests
{


    public class OO2RDFNTriplesTests
    {


        private OO2RDFOptions opcoes;

        [SetUp]
        public void Inicializar()
        {
            this.opcoes = new OO2RDFOptions(new Uri("http://www.base.com"), new NTriplesSerializer());
        }

        [Test]
        public void TesteSimplesNTriples()
        {
            string expectResult = "<http://www.base.com/person1> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .\r\n" 
                                + "<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/name> \"James\"^^<http://www.w3.org/2001/XMLSchema#string> .\r\n"
                                + "<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/age> \"15\"^^<http://www.w3.org/2001/XMLSchema#integer> .\r\n";

            var p1 = new Person();
            p1.Name = "James";
            p1.Age = 15;

            OO2RDFMapper o = new OO2RDFMapper(Assembly.Load("AssemblySampleTest"), new List<object> { p1 }, opcoes);

            o.GenerateTriples();

            Assert.AreEqual(expectResult, o.SerializedTriples);
        }

        [Test]
        public void TesteSimples2NTriples()
        {
            string expectResult = "<http://www.base.com/person1> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .\r\n"
                                + "<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/name> \"James\"^^<http://www.w3.org/2001/XMLSchema#string> .\r\n"
                                + "<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/age> \"15\"^^<http://www.w3.org/2001/XMLSchema#integer> .\r\n"
                                + "<http://www.base.com/person2> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .\r\n"
                                + "<http://www.base.com/person2> <http://xmlns.com/foaf/0.1/name> \"Ronald\"^^<http://www.w3.org/2001/XMLSchema#string> .\r\n"
                                + "<http://www.base.com/person2> <http://xmlns.com/foaf/0.1/age> \"18\"^^<http://www.w3.org/2001/XMLSchema#integer> .\r\n";

            var p1 = new Person();
            p1.Name = "James";
            p1.Age = 15;

            var p2 = new Person();
            p2.Name = "Ronald";
            p2.Age = 18;

            OO2RDFMapper o = new OO2RDFMapper(Assembly.Load("AssemblySampleTest"), new List<object> { p1, p2 }, opcoes);

            o.GenerateTriples();

            Assert.AreEqual(expectResult, o.SerializedTriples);
        }

        [Test]
        public void TesteListaNTriples()
        {
            string expectResult = "<http://www.base.com/person1> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .\r\n"
                                + "<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/name> \"James\"^^<http://www.w3.org/2001/XMLSchema#string> .\r\n"
                                + "<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/knows> <http://www.base.com/person2> .\r\n"
                                + "<http://www.base.com/person2> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .\r\n"
                                + "<http://www.base.com/person2> <http://xmlns.com/foaf/0.1/name> \"Ronald\"^^<http://www.w3.org/2001/XMLSchema#string> .\r\n";

            var p1 = new Person();
            p1.Name = "James";

            var p2 = new Person();
            p2.Name = "Ronald";

            p1.Knows = new List<Person>();
            p1.Knows.Add(p2);

            OO2RDFMapper o = new OO2RDFMapper(Assembly.Load("AssemblySampleTest"), new List<object> { p1, p2 }, opcoes);

            o.GenerateTriples();

            Assert.AreEqual(expectResult, o.SerializedTriples);
        }



    }
}
