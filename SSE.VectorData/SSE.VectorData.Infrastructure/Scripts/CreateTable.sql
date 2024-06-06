/*
CREATE TABLE {tableName}(
	[Word] [nvarchar](400) NULL,
	[Vector] [nvarchar](4000) NULL
);
CREATE CLUSTERED INDEX {tableName}_Index ON {tableName}([Word]);


ALTER DATABASE {dbName}
  MODIFY FILE
  (NAME=[VectorDataTurkish],FILEGROWTH=20MB);

*/