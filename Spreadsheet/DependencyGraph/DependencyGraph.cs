﻿// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

///<summary>
///     Author: Samuel Hancock
///     
///     Jan 24, 2020
///     
///     This library is build a manage the cell references of a spreadsheet. It will map the dependents and dependees 
///     of any given cell.
///     
///     I pledge that I did this work myself.
///</summary>


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SpreadsheetUtilities
{
    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two 
    /// ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 
    ///equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an 
    ///element to a
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is
    ///called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is
    ///called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        // A hashtable to show which cells have dependents
        private Dictionary<string, HashSet<string>> dependentsOfKey;
        // Hashtable for showing a cell's dependees 
        private Dictionary<string, HashSet<string>> dependeesOfKey;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            dependentsOfKey = new Dictionary<string, HashSet<string>>();
            dependeesOfKey = new Dictionary<string, HashSet<string>>();
        }
        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get
            {
                int size = 0;
                //here ordered pair is calculated only on a key and its dependents
                //Every dependent has an implied dependee 
                foreach (KeyValuePair<string, HashSet<string>> dependents in dependentsOfKey)
                {

                    size += dependents.Value.Count;
                }

                return size;
            }
        }
        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you
        /// would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get
            {
                if (dependeesOfKey.ContainsKey(s))
                {
                    return dependeesOfKey[s].Count;
                }
                return 0;//if a cell has not had dependees added, it won't exist in the dictionary

            }
        }
        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        /// <param name="s">The name of the cell being checked for having dependent relationships</param>
        public bool HasDependents(string s)
        {
            //Avoids checking a null hashset object

            if (dependentsOfKey.ContainsKey(s) && dependentsOfKey[s] != null)
            {

                HashSet<string> setOfDependents = dependentsOfKey[s];
                if (setOfDependents.Count > 0)
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        /// <param name="s">Name of cell being checked for dependee relationships</param>
        public bool HasDependees(string s)
        {
            //should show that a cell does not have dependents even if this object has never seen the cell before
            if (dependeesOfKey.ContainsKey(s))
            {
                return dependeesOfKey[s].Count > 0;
            }
            return false;
        }
        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        /// <param name="s">Name of cell that has dependents to be checked and read.</param>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependentsOfKey.ContainsKey(s))
                return dependentsOfKey[s];
            return new HashSet<string>();
        }
        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (dependeesOfKey.ContainsKey(s))
                return dependeesOfKey[s];
            return new HashSet<string>();
        }
        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param> 
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            if (!dependentsOfKey.ContainsKey(s))
            {
                dependentsOfKey.Add(s, new HashSet<string>());
            }
            if (!dependeesOfKey.ContainsKey(t))
            {
                dependeesOfKey.Add(t, new HashSet<string>());
            }
            dependentsOfKey[s].Add(t);
            dependeesOfKey[t].Add(s);

        }
        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            if (dependentsOfKey.ContainsKey(s))
            {
                if (dependentsOfKey[s].Contains(t))
                {
                    dependentsOfKey[s].Remove(t);
                    dependeesOfKey[t].Remove(s);
                }

            }
        }
        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            if (!dependentsOfKey.ContainsKey(s))
            {
                //Here I make sure that the key has a value instead of throwing null pointer exceptions
                dependentsOfKey.Add(s, new HashSet<string>());
            }
            else
            {
                List<string> dependents = new List<string>(dependentsOfKey[s]);
                foreach (string dependent in dependents)
                {
                    RemoveDependency(s, dependent);
                }
                dependentsOfKey[s].Clear();
            }
            //This code will always be able to run, even if the associated hashset is empty
            foreach (string dependent in newDependents)
            {
                AddDependency(s, dependent);
            }
        }
        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            if (!dependeesOfKey.ContainsKey(s))
            {
                dependeesOfKey.Add(s, new HashSet<string>());
            }
            else
            {
                List<string> dependees = new List<string>(dependeesOfKey[s]);
                foreach (string dependee in dependees)
                {
                    //dependee, dependent for remove dependency
                    RemoveDependency(dependee, s);
                }
                dependeesOfKey[s].Clear();
            }
            foreach (string dependee in newDependees)
            {
                AddDependency(dependee, s);
            }
        }
    }
}