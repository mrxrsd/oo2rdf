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


    public class OO2RDFTripplerTests
    {


        private OO2RDFOptions opcoes;

        [SetUp]
        public void Inicializar()
        {
            this.opcoes = new OO2RDFOptions(new Uri("http://www.base.com"), new NTriplesSerializer());
        }

        [Test]
        public void QuantidadeTriplasGeradasDevemSer3()
        {
            var p = new Person();
            p.Name = "James";
            p.Age = 15;
            p.Nickname = "Jimmy";
            p.Email = "aaa@aaa.com";

            OO2RDFMapper o = new OO2RDFMapper(Assembly.Load("AssemblySampleTest"), new List<object> { p } , opcoes);

            o.GenerateTriples();

            Assert.AreEqual(3, o.Triples.Count);
        }


        [Test]
        public void DeveTriplificarElementoInternoDoVetor()
        {
            var p1 = new Person();
            p1.Name = "James";
            p1.Age = 15;

            var p2 = new Person();
            p2.Name = "Jack";
            p2.Age = 20;

            p1.Knows = new List<Person> {p2};

            OO2RDFMapper o = new OO2RDFMapper(Assembly.Load("AssemblySampleTest"), new List<object> { p1 }, opcoes);

            o.GenerateTriples();

            Assert.AreEqual(7, o.Triples.Count);
        }


        [Test]
        public void NaoDeveTriplificarRepetidamenteElementoInternoDoVetor()
        {
            var p1 = new Person();
            p1.Name = "James";
            p1.Age = 15;

            var p2 = new Person();
            p2.Name = "Jack";
            p2.Age = 20;

            p1.Knows = new List<Person> { p2 };

            OO2RDFMapper o = new OO2RDFMapper(Assembly.Load("AssemblySampleTest"), new List<object> { p1, p2 }, opcoes);

            o.GenerateTriples();

            Assert.AreEqual(7, o.Triples.Count);
        }



    }
}
