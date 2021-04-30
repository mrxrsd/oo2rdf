using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OO2RDF;
using OO2RDF.Framework;
using OO2RDF.Framework.Serializer;

namespace Tests
{
    public class OO2RDFLoaderTests
    {
        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "All parameters are required.")]
        public void UriParametroObrigatorio()
        {

            OO2RDFOptions opcoes = new OO2RDFOptions(null, new FileInfo(@"c:\test.txt"), new NTriplesSerializer());

        }

        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "All parameters are required.")]
        public void ArquivoSaidaParametroObrigatorio()
        {
         
            OO2RDFOptions opcoes = new OO2RDFOptions(new Uri("http://www.base.com"), null, new NTriplesSerializer());

        }

        [Test, ExpectedException(typeof(OO2RDFException), ExpectedMessage = "All parameters are required.")]
        public void SerializadorParametroObrigatorio()
        {

            OO2RDFOptions opcoes = new OO2RDFOptions(new Uri("http://www.base.com"), new FileInfo(@"c:\test.txt"), null);

        }

    }
}
