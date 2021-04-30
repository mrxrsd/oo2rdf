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
using System.Text;
using System.Threading.Tasks;
using OO2RDF.Framework.Mapper;

namespace OO2RDF.Framework.Serializer
{
    public  class NTriplesSerializer : BaseSerializer
    {
 
        public override void Serialize(List<Triple> triples )
        {
            triples = triples.OrderBy(x => x.S.ToString()).ToList();

            foreach (var triple in triples)
            {
                string s, p, o;

                s = string.Format("<{0}>", triple.S);

                p = string.Format("<{0}>", triple.P ?? "http://www.w3.org/1999/02/22-rdf-syntax-ns#type");

                o = string.Format("{0}", HandleO(triple.O));

                _sb.AppendLine(string.Format("{0} {1} {2} .",s,p,o));
            }
        }


        private string HandleO(object o)
        {
            if (o is Uri)
            {
                return string.Format("<{0}>", o.ToString());
            }

            return string.Format("\"{0}\"{1}", o.ToString(),HandleDatatype(o));
               
        }

        private string HandleDatatype(object o)
        {

            if (o is string)
            {
                return "^^<http://www.w3.org/2001/XMLSchema#string>";
            }

            if (o is float || o is float?)
            {
                return "^^<http://www.w3.org/2001/XMLSchema#float>";
            }

            if (o is int || o is int?)
            {
                return "^^<http://www.w3.org/2001/XMLSchema#integer>";
            }

            if (o is double)
            {
                return "^^<http://www.w3.org/2001/XMLSchema#double>";
            }

            if (o is decimal)
            {
                return "^^<http://www.w3.org/2001/XMLSchema#decimal>";
            }

            if (o is DateTime)
            {
                return "^^<http://www.w3.org/2001/XMLSchema#dateTime>";
            }


            return string.Empty;
        }
    }
}
