using FluentAssertions;
using Moq;
using Nabs.FileStorage;

namespace Nabs.Tests.FileStorageTests;

public sealed class FakeFileStorageServiceTests
{
	private readonly Mock<IFileStorageService> _fileStorageServiceMock;
	public FakeFileStorageServiceTests()
	{
		_fileStorageServiceMock = new Mock<IFileStorageService>();
	}

	[Fact]
	public async Task PersistFileAsString_Success()
	{
		var content = "ABC";
		_fileStorageServiceMock
			.Setup(x => x.PersistFileAsync(It.IsAny<string>()))
			.ReturnsAsync(true);

		var fileStorageService = _fileStorageServiceMock.Object;
		var result = await fileStorageService.PersistFileAsync(content);
		result.Should().Be(true);
	}

	[Fact]
	public async Task PersistFileAsBytes_Success()
	{
		var content = new byte[] { 10, 11, 12 };
		_fileStorageServiceMock
			.Setup(x => x.PersistFileAsync(It.IsAny<byte[]>()))
			.ReturnsAsync(true);

		var fileStorageService = _fileStorageServiceMock.Object;
		var result = await fileStorageService.PersistFileAsync(content);
		result.Should().Be(true);
	}

	[Fact]
	public async Task GetFile_AsString_Success()
	{
		var id = "fileId";
		var expected = new FileResponse
		{
			Content = "ABC"
		};
		_fileStorageServiceMock
			.Setup(x => x.GetFileAsync(It.IsAny<string>()))
			.ReturnsAsync(expected);

		var fileStorageService = _fileStorageServiceMock.Object;
		var result = await fileStorageService.GetFileAsync(id);
		result.Should().NotBeNull();
		result.Content.Should().NotBeNull();
		result.Content.Value.Should().BeOfType<string>();
	}

	[Fact]
	public async Task GetFile_AsBytes_Success()
	{
		var id = "fileId";
		var expected = new FileResponse()
		{
			Content = new byte[] { 10, 11, 12 }
		};
		_fileStorageServiceMock
			.Setup(x => x.GetFileAsync(It.IsAny<string>()))
			.ReturnsAsync(expected);

		var fileStorageService = _fileStorageServiceMock.Object;
		var result = await fileStorageService.GetFileAsync(id);
		result.Should().NotBeNull();
		result.Content.Should().NotBeNull();
		result.Content.Value.Should().NotBeNull();
		result.Content.Value.Should().BeOfType<byte[]>();
	}

	[Fact]
	public async Task GetFile_ReturnNull_Success()
	{
		var id = "fileId";
		var expected = new FileResponse();
		_fileStorageServiceMock
			.Setup(x => x.GetFileAsync(It.IsAny<string>()))
			.ReturnsAsync(expected);

		var fileStorageService = _fileStorageServiceMock.Object;
		var result = await fileStorageService.GetFileAsync(id);
		result.Should().NotBeNull();
		result.Content.Should().NotBeNull();
		result.Content.Value.Should().BeNull();
	}
}