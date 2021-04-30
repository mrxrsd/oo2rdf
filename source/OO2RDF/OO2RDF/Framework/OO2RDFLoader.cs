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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OO2RDF;
using OO2RDF.Framework.Mapper;

namespace OO2RDF
{
    public class OO2RDFLoader
    {

        private Dictionary<Type,OO2RDFMapping> _mappingTypes;   

        public Dictionary<Type, OO2RDFMapping> MappingTypes
        {
            get { return _mappingTypes; }
        }

        public OO2RDFLoader(Assembly asm)
        {
            _mappingTypes = new Dictionary<Type, OO2RDFMapping>();

            foreach (Type type in asm.GetExportedTypes())
            {
                OO2RDFClassAttribute attribClasse = (OO2RDFClassAttribute)Attribute.GetCustomAttribute(type, typeof(OO2RDFClassAttribute));
                OO2RDFMapping mapping = new OO2RDFMapping(type);
                mapping.Class = attribClasse;

                if (attribClasse != null)
                {
                    PropertyInfo[] propClasse = type.GetProperties();
                    
                    foreach (var p in propClasse)
                    {
                        OO2RDFPropertyAttribute attribProp = p.GetCustomAttribute<OO2RDFPropertyAttribute>(false);
                        if (attribProp != null && !String.IsNullOrEmpty(attribProp.Name) && !attribProp.Ignore)
                        {
                            mapping.Attributes.Add(p, attribProp);
                        }
                    }

                    if (mapping.Attributes.Count > 0)
                    {
                        _mappingTypes.Add(type, mapping);
                    }
                }
            }
        }
    }
}
