using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using LesApp2;
using System.Diagnostics;

namespace LesApp2.Tests
{
    [TestClass]
    public class SearchTests
    {
        private readonly List<string> stub = new List<string>()
        {
            "до магазину, їхати до додому, їхати до",
            "їхати до магазину, іти по дорозі, лежати на столі, кинути під лаву",
            "ніч перед боєм, прийти за годину, початок об одинадцятій",
            "не виконати через хворобу, плакати від горя, сміятися з радості",
            "жити заради добра, подарувати на пам’ять, іти по хліб",
            "завітати при нагоді, бігти незважаючи на перешкоди",
            "говорити через силу, сказати по правді",
            "проїхати зо два кілометри, придбати близько десятка книг",
            "борошно без домішок, книга для вчителів, засіб від опіків",
            "спостерігати за зірками, забути про печаль",
            "тестове речення",
        };

        [TestMethod]
        public void SearchAdnChangeStringData_ReturnTrue()
        {
            foreach (var sentence in stub)
            {
                if (Search.IsPreposition(sentence))
                {
                    string temp = sentence;
                    Debug.WriteLine(temp);
                    Search.ReplacePreposition(ref temp);
                    Debug.WriteLine(temp);

                    Assert.AreNotEqual(sentence, temp);
                }
            }
        }
    }
}
