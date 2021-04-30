using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OO2RDF.Framework.Mapper;

namespace AssemblySampleTest
{
    [OO2RDFClass(Name = "http://xmlns.com/foaf/0.1/Person", SubUrl = "person")]
    public class Person
    {

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/name")]
        public string Name { get; set; }

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/age")]
        public int? Age { get; set; }

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/knows")]
        public List<Person> Knows { get; set; }

        public string Nickname { get; set; }

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/mbox", Ignore = true)]
        public string Email { get; set; }
    }
}
