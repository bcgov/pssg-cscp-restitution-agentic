using Gov.Cscp.VictimServices.Public;
using Gov.Cscp.VictimServices.Public.Models;

namespace Gov.Cscp.VictimServices.Public.Tests
{
    public class DocumentFileExtensionValidationTests
    {
        [Theory]
        [InlineData("report.pdf")]
        [InlineData("photo.PNG")]
        [InlineData("scan.jpeg")]
        [InlineData("scan.jpg")]
        [InlineData("letter.doc")]
        [InlineData("letter.docx")]
        [InlineData("slides.ppt")]
        public void IsAllowedFilename_AcceptsClientAllowlistExtensions(string filename)
        {
            Assert.True(DocumentFileExtensionValidation.IsAllowedFilename(filename));
        }

        [Theory]
        [InlineData("malware.exe")]
        [InlineData("script.js")]
        [InlineData("archive.zip")]
        [InlineData("noextension")]
        [InlineData("")]
        [InlineData(null)]
        public void IsAllowedFilename_RejectsDisallowedOrMissingExtensions(string? filename)
        {
            Assert.False(DocumentFileExtensionValidation.IsAllowedFilename(filename));
        }

        [Fact]
        public void TryValidateDocuments_NullCollection_Passes()
        {
            Assert.True(DocumentFileExtensionValidation.TryValidateDocuments(null, out var error));
            Assert.Null(error);
        }

        [Fact]
        public void TryValidateDocuments_AllowedFilenames_Pass()
        {
            var docs = new[]
            {
                new DocumentDto { Filename = "a.pdf", Body = "abc" },
                new DocumentDto { Filename = "b.docx", Body = "def" },
            };

            Assert.True(DocumentFileExtensionValidation.TryValidateDocuments(docs, out var error));
            Assert.Null(error);
        }

        [Fact]
        public void TryValidateDocuments_DisallowedFilename_FailsWithError()
        {
            var docs = new[]
            {
                new DocumentDto { Filename = "ok.pdf", Body = "abc" },
                new DocumentDto { Filename = "bad.exe", Body = "def" },
            };

            Assert.False(DocumentFileExtensionValidation.TryValidateDocuments(docs, out var error));
            Assert.Contains("bad.exe", error);
        }
    }
}
