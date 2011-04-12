using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CodeSoda.IOC.Tests
{
	[TestFixture]
	public class ContainerTests
	{

		[Test]
		public void TestInitialiseContainer()
		{
			IContainer container = new Container();
		}

		#region Basic Instance Tests

		[Test]
		public void TestCanRegisterAndResolveInstance()
		{
			IContainer container = new Container()
				.Register<ISimpleClassNoDependancies>(new SimpleClassNoDependancies());

			ISimpleClassNoDependancies cs = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs);
		}

		[Test]
		public void TestCanRegisterAndResolve2InstancesThatAreTheSameInstance()
		{
			IContainer container = new Container()
				.Register<ISimpleClassNoDependancies>(new SimpleClassNoDependancies());

			ISimpleClassNoDependancies cs = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs);

			ISimpleClassNoDependancies cs2 = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs2);

			Assert.AreEqual(cs, cs2);
		}

		#endregion

		#region Basic Creator Tests

		[Test]
		public void TestContainerWithCreatorGetsPassedContainer()
		{
			IContainer container = new Container();
			container.Register<IContainer>(c => { Assert.AreSame(c, container); return null; });

			container.Resolve<IContainer>();
		}

		[Test]
		public void TestCanRegisterAndResolveUsingCreator()
		{
			IContainer container = new Container()
				.Register<ISimpleClassNoDependancies>(c => new SimpleClassNoDependancies() );

			ISimpleClassNoDependancies cs = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs);

		}

		[Test]
		public void TestCanRegisterAndResolveTwiceUsingCreatorDoesntCrossInstances()
		{
			IContainer container = new Container()
				.Register<ISimpleClassNoDependancies>(c => new SimpleClassNoDependancies() );

			ISimpleClassNoDependancies cs = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs);

			ISimpleClassNoDependancies cs2 = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs2);

			Assert.AreNotEqual(cs, cs2);
		}

		#endregion

		#region Basic Implementation Tests

		[Test]
		public void TestCanRegisterAndResolveImplementation()
		{
			IContainer container = new Container()
				.Register<ISimpleClassNoDependancies, SimpleClassNoDependancies>();

			ISimpleClassNoDependancies cs = container.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs);
		}

		[Test]
		public void TestCanRegisterAndResolveTwiceUsingImplementationDoesntCrossInstances()
		{
			IContainer IOC = new Container()
				.Register<ISimpleClassNoDependancies, SimpleClassNoDependancies>();

			ISimpleClassNoDependancies cs = IOC.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs);

			ISimpleClassNoDependancies cs2 = IOC.Resolve<ISimpleClassNoDependancies>();
			Assert.IsNotNull(cs2);

			Assert.AreNotEqual(cs, cs2);
		}

		#endregion

		[Test]
		public void TestRegisterAndResolveWithDependancies()
		{
			IContainer container = new Container()
				.Register<IFileSystemAdapter>(c => new FileSystemAdapter() )
				.Register<IBuildDirectoryStructureService, BuildDirectoryStructureService>()
				.Register<ISimpleClassNoDependancies>(new SimpleClassNoDependancies());
			
			IBuildDirectoryStructureService service = container.Resolve<IBuildDirectoryStructureService>();

			Assert.IsNotNull(service);
			Assert.IsNotNull(service.FileSystemAdapter);
		}

		[Test]
		public void TestCanResolveConcreteClassOnly() {
			IContainer container = new Container()
				.Register<IConcreteDependancyInterface, ConcreteDependancyClass>();

			ConcreteClass cc = container.Resolve<ConcreteClass>();
			Assert.IsNotNull(cc);
			Assert.IsNotNull(cc._dependancy);
		}
	}

}
