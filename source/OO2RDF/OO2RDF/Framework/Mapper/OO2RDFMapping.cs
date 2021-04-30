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

namespace OO2RDF.Framework.Mapper
{
    public class OO2RDFMapping
    {

        public OO2RDFMapping(Type type)
        {
            this.Type = type;
            this.Attributes = new Dictionary<PropertyInfo, OO2RDFPropertyAttribute>();
        }

        public Type Type { get; set; }

        public OO2RDFClassAttribute Class { get; set; }

        public Dictionary<PropertyInfo,OO2RDFPropertyAttribute> Attributes { get; set; } 
    }
}
