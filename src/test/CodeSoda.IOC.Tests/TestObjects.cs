
namespace CodeSoda.IOC.Tests
{
	public interface IConcreteDependancyInterface
	{
		string Name { get; }
	}

	public class ConcreteDependancyClass : IConcreteDependancyInterface
	{
		public string Name
		{
			get
			{
				return "Name";
			}
		}
	}

	public class ConcreteClass
	{
		public IConcreteDependancyInterface _dependancy;
		public ConcreteClass(IConcreteDependancyInterface dependancy)
		{
			this._dependancy = dependancy;
		}

	}

	public interface IFileSystemAdapter { }
	public class FileSystemAdapter : IFileSystemAdapter { }

	public interface IBuildDirectoryStructureService
	{
		IFileSystemAdapter FileSystemAdapter { get; }
	}

	public class BuildDirectoryStructureService : IBuildDirectoryStructureService
	{
		public IFileSystemAdapter FileSystemAdapter
		{
			get { return fileSystemAdapter; }
		}

		IFileSystemAdapter fileSystemAdapter;
		public BuildDirectoryStructureService(IFileSystemAdapter fileSystemAdapter)
		{
			this.fileSystemAdapter = fileSystemAdapter;
		}
	}

	public interface ISimpleClassNoDependancies
	{
		string Name { get; set; }
		int Age { get; set; }
	}

	public class SimpleClassNoDependancies : ISimpleClassNoDependancies
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}
}
