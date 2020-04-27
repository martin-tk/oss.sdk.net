using System;
using System.Collections.Generic;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.API;
namespace SDK.Examples
{
    public class LocalLanguageExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new LocalLanguageExample().Run();
        }

        public readonly string DOCUMENT_NAME = "First Document";

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("One1")
                                .WithLastName("Span1")
                                .WithLocalLanguage())
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = OssClient.CreatePackage(superDuperPackage);
            OssClient.SendPackage(packageId);
        }
    }
}

