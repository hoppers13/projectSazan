using ProjectSazan.Domain;
using System;

namespace ProjectSazan.Web.Persistence.FileSystem
{
	public static class PersistencePathCreator
    {
		internal static string GetDataStoragePath()
		{
			//TODO: read data storage location from app configuration
			return "\\dataStorage\\";
		}

		internal static string GetCollectorPersistencePath(UserIdentity collector)
		{
			return $"{GetDataStoragePath()}{collector.Id}\\";
		}

		internal static string GetCollectionSummaryPersistencePath(UserIdentity collector)
		{
			return $"{GetCollectorPersistencePath(collector)}collections.json";
		}

		internal static string GetCollectionPersistencePath(UserIdentity collector, Guid collectionId)
		{
			return $"{GetCollectorPersistencePath(collector)}\\{collectionId}\\{collectionId}.json";
		}


		//TODO: methods below should build paths incrementally as the methods above

		internal static PersistencePath CreateCollectableScanPath(UserIdentity collector, Guid collectionId, string filename)
		{
			return new PersistencePath
			{
				DirectoryPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\scans",
				FilestreamPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\scans\\{filename}",
				PathToPersist = $"/{collector.Id}/{collectionId}/scans/{filename}"
			};
		}

		internal static PersistencePath CreateCollectionDocumentPath(UserIdentity collector, Guid collectionId, string documentName)
		{
			return new PersistencePath
			{
				DirectoryPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\",
				FilestreamPath = $"\\dataStorage\\{collector.Id}\\{collectionId}\\{documentName}"
			};
		}

	}
}