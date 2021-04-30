using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OO2RDF;
using OO2RDF.Framework;
using OO2RDF.Framework.Serializer;

namespace Tests
{
    public class OO2RDFMapperTests
    {

        private OO2RDFOptions opcoes;

        [SetUp]
        public void Inicializar()
        {
            this.opcoes = new OO2RDFOptions(new Uri("http://www.base.com"), new FileInfo(@"c:\test.txt"), new NTriplesSerializer());
        }

        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "All parameters are required.")]
        public void AssemblyParametroObrigatorio()
        {

            OO2RDFMapper o = new OO2RDFMapper(null, new List<object>(), opcoes);
        }


        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "All parameters are required.")]
        public void ListaObjetosParametroObrigatorio()
        {

            OO2RDFMapper o = new OO2RDFMapper(null, null, opcoes);
        }

        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "All parameters are required.")]
        public void OpcoesParametroObrigatorio()
        {

            OO2RDFMapper o = new OO2RDFMapper(null, new List<object>(), opcoes);
        }


        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "List of objects to triplify cannot be empty.")]
        public void ListaObjetosTriplificarNaoPodeSerVazia()
        {

            OO2RDFMapper o = new OO2RDFMapper(Assembly.GetExecutingAssembly(), new List<object>(), opcoes);
        }


        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "There is no decorated class.")]
        public void NaoPodeTriplificarProjetoSemMarcacao()
        {
   
            OO2RDFMapper o = new OO2RDFMapper(Assembly.GetExecutingAssembly(), new List<object>{ new object() }, opcoes);
  
        }

        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "There is no known object in objects list.")]
        public void NaoPodeTriplificarObjetosNaoConhecidos()
        {

            Assembly asm = Assembly.Load("AssemblySampleTest");

            OO2RDFMapper o = new OO2RDFMapper(asm, new List<object> { new object() }, opcoes);

        }

    }
}
