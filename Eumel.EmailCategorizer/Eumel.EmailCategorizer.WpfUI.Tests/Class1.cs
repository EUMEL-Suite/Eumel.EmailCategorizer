using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Eumel.EmailCategorizer.WpfUI.Manager;
using Eumel.EmailCategorizer.WpfUI.Tests;
using NUnit.Framework;

namespace CGI.eGovCDS.Architectural.Tests.TestCreator
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    internal class Generate_Test_Classes
    {
        public const string TestClassTemplate = @"using $testclassnamespace$;
using FluentAssertions;
using NUnit.Framework;

namespace $rootnamespace$
{
    [TestFixture]
    public class $ClassToTest$Test
    {
        [Test]
        public void Be_Creatable()
        {
            var ctx = new ContextFor<$ClassToTest$>();
            var sut = ctx.BuildSut();

            sut.Should().NotBeNull();
        }
    }
}";

        [Test]
        [Explicit("this created code")]
        [TestCase(typeof(EumelConfigManager), typeof(EumelConfigManagerTests))]
        [Description("Create all missing test classes")]
        public void Create_Missing_Tests(Type sourceType, Type destinationType)
        {
            var srcAsm = Assembly.GetAssembly(sourceType);
            var dstAsm = Assembly.GetAssembly(destinationType);
            var testProjectFile = new FileInfo(dstAsm.Location).Directory.Parent.Parent.Parent.GetFiles("*.csproj", SearchOption.AllDirectories).First(x => x.Name.StartsWith(dstAsm.GetName().Name)).FullName;

            var rootNamespace = sourceType.Namespace;

            var cls = srcAsm.GetTypes().Where(y =>
                y.IsClass && !y.IsAbstract && !y.IsNested && !y.IsGenericType &&
                y.GetCustomAttribute<IgnoreAttribute>() == null).ToArray();

            var tst = dstAsm.GetTypes().Where(y =>
                y.IsClass && !y.IsAbstract && !y.IsNested && !y.IsGenericType).ToArray();

            foreach (var type in cls)
            {
                var name = type.Name;
                var testName = name + "Test";
                var testClass = tst.SingleOrDefault(x => x.Name == testName);
                if (testClass != null) continue; // nothing to create

                AddFileToUnitTestProject(testProjectFile, type, rootNamespace);
            }
        }

        private void AddFileToUnitTestProject(string testProjectFile, Type type, string rootNamespace)
        {
            var subNameSpace = type.Namespace.Replace(rootNamespace, "");
            if (subNameSpace.Length > 0) subNameSpace = subNameSpace.Substring(1);

            var baseDir = new FileInfo(testProjectFile).Directory.FullName;
            var fileDir = Path.Combine(baseDir, subNameSpace.Replace(".", "\\"));
            Directory.CreateDirectory(fileDir);
            var fullFilename = Path.Combine(fileDir, type.Name + "Test.cs");
            var filename = fullFilename.Replace(baseDir, "").Substring(1);

            var subNamespacePart = subNameSpace.Length == 0 ? "" : "." + subNameSpace;
            File.WriteAllText(fullFilename, TestClassTemplate
                .Replace("$ClassToTest$", type.Name)
                .Replace("$testclassnamespace$", type.Namespace)
                .Replace("$rootnamespace$", rootNamespace + ".Tests" + subNamespacePart));

            var unitTestProjectFile = XDocument.Load(testProjectFile);
            var itemGroup =
                unitTestProjectFile.Nodes()
                    .OfType<XElement>()
                    .DescendantNodes()
                    .OfType<XElement>().Where(xy => xy.Name.LocalName == "ItemGroup")
                    .FirstOrDefault(z => z.Descendants().First().Name.LocalName == "Compile");
            var xelem = AddProjectContent(filename, unitTestProjectFile);
            itemGroup.Add(xelem);
            unitTestProjectFile.Save(testProjectFile);
        }

        private static XElement AddProjectContent(string pathToAdd, XDocument doc)
        {
            XNamespace rootNamespace = doc.Root.Name.NamespaceName;
            var xelem = new XElement(rootNamespace + "Compile");
            xelem.Add(new XAttribute("Include", pathToAdd));
            return xelem;
        }
    }
}