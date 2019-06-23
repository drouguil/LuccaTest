using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;

namespace LuccaDevisesTest.Mock
{
    internal sealed class MockStreamReaderService
    {

        /// <summary>
        /// 
        /// </summary>

        private const string MOCK_FILE_NAME = @"C:\temp\test.txt";

        /// <summary>
        /// 
        /// </summary>

        private MockFileSystem _mockFileSystem;

        /// <summary>
        /// 
        /// </summary>

        private static readonly Lazy<MockStreamReaderService> lazy = new Lazy<MockStreamReaderService>(() => new MockStreamReaderService());

        /// <summary>
        /// 
        /// </summary>

        public static MockStreamReaderService Instance { get { return lazy.Value; } }


        /// <summary>
        /// 
        /// </summary>

        private MockStreamReaderService() {
            _mockFileSystem = new MockFileSystem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>

        public static StreamReader MockContentFile(string content)
        {
            MockFileSystem mockFileSystem = Instance._mockFileSystem;

            MockFileData mockInputFile = new MockFileData(content);

            mockFileSystem.AddFile(MOCK_FILE_NAME, mockInputFile);

            StreamReader streamReader = mockFileSystem.File.OpenText(MOCK_FILE_NAME);

            return streamReader;
        }
    }
}
