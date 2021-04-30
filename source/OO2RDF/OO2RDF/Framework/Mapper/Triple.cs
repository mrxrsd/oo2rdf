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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO2RDF.Framework.Mapper
{
    [DebuggerDisplay("{_s} {_p} {_o}")]
    public class Triple
    {
        
        private Uri _s;

        private string _p;

        private object _o;

        public Uri S
        {
            get { return _s; }
        }

        public string P
        {
            get { return _p; }
        }

        public object O 
        {
            get
            {
                return _o;
            }
        }

        public Triple(Uri s, string p, object o)
        {
            _s = s;
            _p = p;
            _o = o;
        }

        public Triple(Uri s, object o)
            : this(s, null, o)
        {
   
        }


    }
}
