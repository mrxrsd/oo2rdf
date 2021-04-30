# **OO2RDF**

OO2RDF is a framework for data triplification. 

**Pros:**
* It´s a framework, you only need to decorate your business classes.
* Simple to use.

**Cons:**
* All objects must be in memory.
* Only one standard implemented, for now, just N-Triples.
* Early stage of development (W.I.P)

## **How to use:**

First, you need to decorate all your classes, then just instantiate OO2RDF and call GenerateTriples method.  There is a sample project in OO2RDF solution.

### **Example:**

Person.cs
```csharp
    [OO2RDFClass(Name = "http://xmlns.com/foaf/0.1/Person", SubUrl = "person")](OO2RDFClass(Name-=-_http___xmlns.com_foaf_0.1_Person_,-SubUrl-=-_person_))
    public class Person
    {      

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/name")](OO2RDFProperty(Name-=-_http___xmlns.com_foaf_0.1_name_))
        public string Name { get; set; }

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/age")](OO2RDFProperty(Name-=-_http___xmlns.com_foaf_0.1_age_))
        public int Age { get; set; }

        [OO2RDFProperty(Name = "http://xmlns.com/foaf/0.1/knows")](OO2RDFProperty(Name-=-_http___xmlns.com_foaf_0.1_knows_))
        public List<Person> Knows { get; set; }
    }
```

Program.cs
```csharp
            Person a = new Person();
            a.Name = "James";
            a.Age = 26;

            Person b = new Person();
            b.Name = "Jack";
            b.Age = 30;

            Person c = new Person();
            c.Name = "Amanda";
            c.Age = 19;


            a.Knows = new List<Person>();
            a.Knows.Add(b);
            a.Knows.Add(c);

            c.Knows = new List<Person>();
            c.Knows.Add(a);

            b.Knows = new List<Person>();
            b.Knows.Add(a);



            List<object> l = new List<object>();
            l.Add(a);
            l.Add(b);
            l.Add(c);
           


            FileInfo f = new FileInfo(@"c:\test.txt");

            OO2RDFOptions opcoes = new OO2RDFOptions(new Uri("http://www.base.com"),  f, new NTriplesSerializer());

            OO2RDFMapper o = new OO2RDFMapper(Assembly.GetExecutingAssembly(), l, opcoes);

            o.GenerateTriples();
```



Output:

<http://www.base.com/person1> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .

<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/name> "James"^^<http://www.w3.org/2001/XMLSchema#string> .

<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/age> "26"^^<http://www.w3.org/2001/XMLSchema#integer> .

<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/knows> <http://www.base.com/person2> .

<http://www.base.com/person1> <http://xmlns.com/foaf/0.1/knows> <http://www.base.com/person3> .

<http://www.base.com/person2> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .

<http://www.base.com/person2> <http://xmlns.com/foaf/0.1/name> "Jack"^^<http://www.w3.org/2001/XMLSchema#string> .

<http://www.base.com/person2> <http://xmlns.com/foaf/0.1/age> "30"^^<http://www.w3.org/2001/XMLSchema#integer> .

<http://www.base.com/person2> <http://xmlns.com/foaf/0.1/knows> <http://www.base.com/person1> .

<http://www.base.com/person3> <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://xmlns.com/foaf/0.1/Person> .

<http://www.base.com/person3> <http://xmlns.com/foaf/0.1/name> "Amanda"^^<http://www.w3.org/2001/XMLSchema#string> .

<http://www.base.com/person3> <http://xmlns.com/foaf/0.1/age> "19"^^<http://www.w3.org/2001/XMLSchema#integer> .

<http://www.base.com/person3> <http://xmlns.com/foaf/0.1/knows> <http://www.base.com/person1> .


