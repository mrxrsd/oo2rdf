﻿/*******************************************************************************
 * You may amend and distribute as you like, but don't remove this header!
 *
 * OO2RDF - framework for data triplification
 * See http://www.codeplex.com/oo2rdf for details.
 *
 * Copyright (C) 2013  Thiago Trigueiros
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.

 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU Lesser General Public License for more details.
 *
 * The GNU Lesser General Public License can be viewed at http://www.opensource.org/licenses/lgpl-license.php
 * If you unfamiliar with this license or have questions about it, here is an http://www.gnu.org/licenses/gpl-faq.html
 *
 * All code and executables are provided "as is" with no warranty either express or implied. 
 * The author accepts no liability for any damage or loss of business that this product may cause.
 *
 * Code change notes:
 * 
 * Author							Change						Date
 *******************************************************************************
 * Thiago Trigueiros		        Added		              18-JUN-2013 
 *******************************************************************************/



using OO2RDF.Framework;
using OO2RDF.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OO2RDF
{

    public class OO2RDFMapper
    {

        private OO2RDFOptions _options;

        private OO2RDFLoader _loader;

        private Tripler _trippler;
        
        private List<Object> _objs;

        public string SerializedTriples
        {
            get { return _options.Serializer.SerializedTriples; }
        }

        public List<Triple> Triples
        {
            get { return _trippler.Triples; }
        }

        public OO2RDFMapper(Assembly assembly, List<Object> objetos, OO2RDFOptions opcoes)
        {
            if (assembly == null || objetos == null || opcoes == null)
            {
                throw new OO2RDFException("All parameters are required.");
            }

            if (objetos.Count == 0)
            {
                throw new OO2RDFException("List of objects to triplify cannot be empty.");
            }

            _loader = new OO2RDFLoader(assembly);
            _options = opcoes;
            _objs = objetos;            

            if (_loader.MappingTypes.Count == 0)
            {
                throw new OO2RDFException("There is no decorated class.");
            }

            if (!_objs.Exists(o => _loader.MappingTypes.ContainsKey(o.GetType())))
            {
                throw new OO2RDFException("There is no known object in objects list.");
            }
        }


        public void GenerateTriples()
        {

            _trippler = new Tripler(_options.BaseUrl, _loader.MappingTypes, _objs);

            _trippler.Triplify();

            _options.Serializer.Serialize(_trippler.Triples);

            if (_options.OutputFile != null)
            {
                if (_options.OutputFile.Exists)
                {
                    _options.OutputFile.Delete();
                }

                TextWriter tw = new StreamWriter(_options.OutputFile.FullName);
                tw.Write(SerializedTriples);
                tw.Close();
            }
            
        }

    }
}
