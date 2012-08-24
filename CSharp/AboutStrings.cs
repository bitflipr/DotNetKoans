using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace DotNetKoans.CSharp
{
    public class AboutStrings : Koan
    {
        //Note: This is one of the longest katas and, perhaps, one
        //of the most important. String behavior in .NET is not
        //always what you expect it to be, especially when it comes
        //to concatenation and newlines, and is one of the biggest
        //causes of memory leaks in .NET applications

        [Koan(1)]
        public void DoubleQuotedStringsAreStrings()
        {
            var str = "Hello, World";
            Assert.Equal(typeof(System.String), str.GetType());
        }

        [Koan(2)]
        public void SingleQuotedStringsAreNotStrings()
        {
            var str = 'H';
            Assert.Equal(typeof(Char), str.GetType());
        }

        [Koan(3)]
        public void CreateAStringWhichContainsDoubleQuotes()
        {
            var str = "Hello, \"World\"";
            Assert.Equal(14, str.Length);
        }

        [Koan(4)]
        public void AnotherWayToCreateAStringWhichContainsDoubleQuotes()
        {
            //The @ symbol creates a 'verbatim string literal'. 
            //Here's one thing you can do with it:
            var str = @"Hello, ""World""";
            Assert.Equal(14, str.Length);
        }

        [Koan(5)]
        public void VerbatimStringsCanHandleFlexibleQuoting()
        {
            var strA = @"Verbatim Strings can handle both ' and "" characters (when escaped)";
            var strB = "Verbatim Strings can handle both ' and \" characters (when escaped)";
            Assert.Equal(true, strA.Equals(strB));
        }

        [Koan(6)]
        public void VerbatimStringsCanHandleMultipleLinesToo()
        {
            //Tip: What you create for the literal string will have to 
            //escape the newline characters. For Windows, that would be
            // \r\n. If you are on non-Windows, that would just be \n.
            //We'll show a different way next.
            var verbatimString = @"I
am a
broken line";
            Assert.Equal(20, verbatimString.Length);
            var literalString = "I\r\nam a\r\nbroken line";
            Assert.Equal(literalString, verbatimString);
        }

        [Koan(7)]
        public void ACrossPlatformWayToHandleLineEndings()
        {
            //Since line endings are different on different platforms
            //(\r\n for Windows, \n for Linux) you shouldn't just type in
            //the hardcoded escape sequence. A much better way
            //(We'll handle concatenation and better ways of that in a bit)
            var literalString = "I" + System.Environment.NewLine + "am a" + System.Environment.NewLine + "broken line";
            var vebatimString = @"I
am a
broken line";
            Assert.Equal(literalString, vebatimString);
        }

        [Koan(8)]
        public void PlusWillConcatenateTwoStrings()
        {
            var str = "Hello, " + "World";
            Assert.Equal("Hello, World", str);
        }

        [Koan(9)]
        public void PlusConcatenationWillNotModifyOriginalStrings()
        {
            var strA = "Hello, ";
            var strB = "World";
            var fullString = strA + strB;
            Assert.Equal("Hello, ", strA);
            Assert.Equal("World", strB);
        }

        [Koan(10)]
        public void PlusEqualsWillModifyTheTargetString()
        {
            var strA = "Hello, ";
            var strB = "World";
            strA += strB;
            Assert.Equal("Hello, World", strA);
            Assert.Equal("World", strB);
        }

        [Koan(11)]
        public void StringsAreReallyImmutable()
        {
            //So here's the thing. Concatenating strings is cool
            //and all. But if you think you are modifying the original
            //string, you'd be wrong. 

            var strA = "Hello, ";
            var originalString = strA;
            var strB = "World";
            strA += strB;
            Assert.Equal("Hello, ", originalString);

            //What just happened? Well, the string concatenation actually
            //takes strA and strB and creates a *new* string in memory
            //that has the new value. It does *not* modify the original
            //string. This is a very important point - if you do this kind
            //of string concatenation in a tight loop, you'll use a lot of memory
            //because the original string will hang around in memory until the
            //garbage collector picks it up. Let's look at a better way
            //when dealing with lots of concatenation
        }

        [Koan(12)]
        public void ABetterWayToConcatenateLotsOfStrings()
        {
            //As shows in the above Koan, concatenating lots of strings
            //is a Bad Idea(tm). If you need to do that, then do this instead
            var strBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < 100; i++) { strBuilder.Append("a"); }
            var str = strBuilder.ToString();
            Assert.Equal(100, str.Length);

            //The tradeoff is that you have to create a StringBuilder object, 
            //which is a higher overhead than a string. So the rule of thumb
            //is that if you need to concatenate less than 5 strings, += is fine.
            //If you need more than that, use a StringBuilder. 
        }

        [Koan(13)]
        public void LiteralStringsInterpretsEscapeCharacters()
        {
            var str = "\n";
            Assert.Equal(1, str.Length);
        }

        [Koan(14)]
        public void VerbatimStringsDoNotInterpretEscapeCharacters()
        {
            var str = @"\n";
            Assert.Equal(2, str.Length);
        }

        [Koan(15)]
        public void VerbatimStringsStillDoNotInterpretEscapeCharacters()
        {
            var str = @"\\\";
            Assert.Equal(3, str.Length);
        }

        [Koan(16)]
        public void YouDoNotNeedConcatenationToInsertVariablesInAString()
        {
            var world = "World";
            var str = String.Format("Hello, {0}", world);
            Assert.Equal("Hello, World", str);
        }

        [Koan(17)]
        public void AnyExpressionCanBeUsedInFormatString()
        {
            var str = String.Format("The square root of 9 is {0}", Math.Sqrt(9));
            Assert.Equal("The square root of 9 is 3", str);
        }

        [Koan(18)]
        public void YouCanGetASubstringFromAString()
        {
            var str = "Bacon, lettuce and tomato";
            Assert.Equal("tomato", str.Substring(19));
            Assert.Equal("let", str.Substring(7, 3));
        }

        [Koan(19)]
        public void YouCanGetASingleCharacterFromAString()
        {
            var str = "Bacon, lettuce and tomato";
            Assert.Equal('B', str[0]);
        }

        [Koan(20)]
        public void SingleCharactersAreRepresentedByIntegers()
        {
            Assert.Equal(97, 'a');
            Assert.Equal(98, 'b');
            Assert.Equal(true, 'b' == ('a' + 1));
        }

        [Koan(21)]
        public void StringsCanBeSplit()
        {
            var str = "Sausage Egg Cheese";
            string[] words = str.Split();
            Assert.Equal(new[] { "Sausage", "Egg", "Cheese" }, words);
        }

        [Koan(22)]
        public void StringsCanBeSplitUsingCharacters()
        {
            var str = "the:rain:in:spain";
            string[] words = str.Split(':');
            Assert.Equal(new[] { "the", "rain", "in", "spain" }, words);
        }

        [Koan(23)]
        public void StringsCanBeSplitUsingRegularExpressions()
        {
            var str = "the:rain:in:spain";
            var regex = new System.Text.RegularExpressions.Regex(":");
            string[] words = regex.Split(str);
            Assert.Equal(new[] { "the", "rain", "in", "spain" }, words);

            //A full treatment of regular expressions is beyond the scope
            //of this tutorial. The book "Mastering Regular Expressions"
            //is highly recommended to be on your bookshelf
        }
    }
}
