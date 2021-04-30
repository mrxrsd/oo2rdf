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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace OO2RDF.Framework.Mapper
{
    public class Tripler
    {

        private Dictionary<Type, List<int>> _process; 

        private List<object> _objs; 

        private List<Triple> _triples;

        private Uri _baseUrl;

        private Dictionary<Type, OO2RDFMapping> _mappingTypes; 

        private object l = new object();

        public List<Triple> Triples
        {
            get { return _triples; }
        } 

        public Tripler(Uri baseUrl, Dictionary<Type, OO2RDFMapping> MappingTypes, List<object> objects )
        {
            _mappingTypes = MappingTypes;
            _baseUrl = baseUrl;
            _objs = objects;
            _triples = new List<Triple>();
            _process = new Dictionary<Type, List<int>>();

        }

        public void Triplify()
        {
            foreach (object obj in _objs)
            {
                if (!AlreadyProcess(obj))
                    ParseObject(obj);
            }
        }

        private int GenerateTripleID(object obj)
        {

            lock (l)
            {
                List<int> processHashs = null;

                if (!_process.ContainsKey(obj.GetType()))
                {
                    _process.Add(obj.GetType(),new List<int>());   
                }   
                
                processHashs = _process[obj.GetType()];

                if (!processHashs.Contains(obj.GetHashCode()))
                {
                    processHashs.Add(obj.GetHashCode());
                }

                return processHashs.IndexOf(obj.GetHashCode()) + 1;
            }            

        }

        private bool AlreadyProcess(object obj)
        {
            if (!_process.ContainsKey(obj.GetType()))
            {
                return false;
            }

            if (!_process[obj.GetType()].Contains(obj.GetHashCode()))
            {
                return false;
            }

            return true;
        }

        private void ParseObject(object obj)
        {

            OO2RDFMapping m = null;
            Type objType = obj.GetType();

            if (_mappingTypes.TryGetValue(objType, out m))
            {

                var attribClasse = m.Class;
                Uri baseUrlObj = new Uri(_baseUrl + attribClasse.SubUrl + GenerateTripleID(obj));


                _triples.Add(new Triple(
                                 baseUrlObj,
                                 new Uri(attribClasse.Name)
                                 )
                    );


                PropertyInfo[] properties = objType.GetProperties();

                foreach (PropertyInfo pi in properties)
                {
                    OO2RDFPropertyAttribute a = null;

                    if (m.Attributes.TryGetValue(pi,out a))
                    {
                        if (!a.Ignore)
                        {

                            MethodInfo methodInfo = pi.GetGetMethod(false);

                            object val = pi.GetValue(obj, null);

                            if (val != null)
                            {
                                if (val.GetType().IsPrimitive || isStringType(methodInfo.ReturnType))
                                {
                                    ParseMember(baseUrlObj, val, a.Name);
                                }     
                                else if (methodInfo.ReturnType.IsGenericType)
                                {
                                    ParseGenericType(baseUrlObj, val, a.Name);
                                }
                                else if (methodInfo.ReturnType.IsClass)
                                {
                                    ParseObject(baseUrlObj, val, a.Name);
                                }
                            }

                        }
                    }

                }
            }

        }

        private void ParseObject(Uri baseURL, object val, string memberName)
        {

            bool process = AlreadyProcess(val);


            Type objType = val.GetType();
            var attribClasse = _mappingTypes[objType].Class;
            Uri baseUrlObj = new Uri(_baseUrl + attribClasse.SubUrl + GenerateTripleID(val));


            _triples.Add(new Triple(baseURL,
                                    memberName,
                                    baseUrlObj)
                         );

            if (!process)
            {
                ParseObject(val);
            }
        }

        private void ParseGenericType(Uri baseURL, object val, string memberName)
        {
            if (val is IEnumerable)
            {
                ParseIEnumerable(baseURL,val,memberName);
            }
        }

        private void ParseIEnumerable(Uri baseURL, object val, string memberName)
        {
            var enumerator = (val as IEnumerable).GetEnumerator();

            if (enumerator.MoveNext())
            {
                do
                {
                    ParseObject(baseURL,enumerator.Current,memberName);

                } while (enumerator.MoveNext());
            }

        }

        private bool isStringType(Type t)
        {
            if (t == typeof(string))
                return true;
            return false;
        }

        private void ParseMember(Uri baseURL, object val, string memberName)
        {
    
            _triples.Add
                (
                new Triple(
                    baseURL,
                    memberName,
                    val
                    )
                );
        }
    }
}
