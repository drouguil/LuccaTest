using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;

namespace LuccaDevisesTest.Mock
{
    /// <summary>
    /// Singleton service for mock file reading
    /// </summary>

    internal sealed class MockStreamReaderService
    {
        #region Private attributes

        /// <summary>
        /// Fake file
        /// </summary>

        private const string _MOCK_FILE_NAME = @"C:\temp\test.txt";

        /// <summary>
        /// Mock file system
        /// </summary>

        private MockFileSystem _mockFileSystem;

        /// <summary>
        /// Singleton instance
        /// </summary>

        private static readonly Lazy<MockStreamReaderService> lazy = new Lazy<MockStreamReaderService>(() => new MockStreamReaderService());

        #endregion

        #region Properties

        /// <summary>
        /// Singleton instance
        /// </summary>

        public static MockStreamReaderService Instance { get { return lazy.Value; } }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>

        private MockStreamReaderService() {
            _mockFileSystem = new MockFileSystem();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Mock content in the fake file
        /// </summary>
        /// <param name="content">Fake content file</param>
        /// <returns>Stream reader of the fake file</returns>

        public static StreamReader MockContentFile(string content)
        {
            MockFileSystem mockFileSystem = Instance._mockFileSystem;

            MockFileData mockInputFile = new MockFileData(content);

            mockFileSystem.AddFile(_MOCK_FILE_NAME, mockInputFile);

            StreamReader streamReader = mockFileSystem.File.OpenText(_MOCK_FILE_NAME);

            return streamReader;
        }

        #endregion
    }
}
