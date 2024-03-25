using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Runtime.Session;
using Boxfusion.LMS_Backend.Authorization.Users;
using Boxfusion.LMS_Backend.Domain;
using Boxfusion.LMS_Backend.Sessions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Boxfusion.LMS_Backend.Services.AskGoogle
{
    public class AskGoogleAppService : LMS_BackendAppServiceBase, IAskGoogleAppService
    {
        static HttpClient client;
        IRepository<Book, long> _booksRepository;
        IRepository<Category, long> _catRepo;
        IRepository<Author, Guid> _authorRepo;
        private readonly Random _random;
        public AskGoogleAppService(IRepository<Book, long> repository, IRepository<Category, long> catRepo, IRepository<Author, Guid> authorRepo)
        {
            _booksRepository = repository;
            _catRepo = catRepo;
            _authorRepo = authorRepo;

            client = new HttpClient();
            _random = new Random();

            client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<List<Book>> GetCount(int count = 50)
        {
            var books = await _booksRepository.GetAllListAsync();

            return books;
        }

        public async Task<List<Book>> GetRandomBooksAsync(int count = 50)
        {
            var books = new List<Book>();

            for (int i = 0; i < count; i++)
            {
                var categoryId = _random.Next(0, 10); // Adjust based on the number of categories

                var book = await GetRandomBookAsync(categoryId);
                if (book != null)
                {
                    books.Add(book);
                }
            }

            return books;
        }
        private async Task<Book> GetRandomBookAsync(int categoryId)
        {
            var startIndex = _random.Next(0, 1000); // Adjust based on the number of available books

            var response = await client.GetStringAsync($"volumes?q=*&startIndex={startIndex}&maxResults=1");

            var jsonResponse = JObject.Parse(response);
            var items = jsonResponse["items"];

            if (items != null && items.HasValues)
            {
                var book = items[0].ToObject<Book>();
                book.CategoryId = categoryId; // Assign category ID
                return book;
            }

            return null;
        }

        public async Task<string> TestPotter()
        {
            HttpResponseMessage response = await client.GetAsync(
                "volumes/s1gVAAAAYAAJ");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Result> TestVolumes(string query)
        {
            var startIndex = _random.Next(0, 1000); // Adjust based on the number of available books
            HttpResponseMessage response = await client.GetAsync(
                $"volumes?q={query}&startIndex={startIndex}&maxResults=10");
            var volumes = await response.Content.ReadFromJsonAsync<Result>();
            var books = new List<Book>();

            if (volumes.Items != null)
            {
                if (volumes.Items.Count > 0)
                {
                    foreach (Volume v in volumes.Items)
                    {
                        // we need to search if such a category does not already exist in the categories db... same with Author
                        try
                        {
                            var firstAuthor = v.VolumeInfo.Authors.FirstOrDefault();
                            string first = "";
                            string last = "";
                            if (!firstAuthor.IsNullOrEmpty())
                            {
                                string[] split = firstAuthor.Split(' ');
                                first = split[0];
                                last = split[1];
                            }

                            Author author = new Author
                            {
                                FirstName = first,
                                LastName = last,
                            };

                            Category category = new Category
                            {
                                Name = v.VolumeInfo.Categories == null ? "Category" : (v.VolumeInfo.Categories[0] != null ? v.VolumeInfo.Categories[0] : "Category"),
                                Description = v.VolumeInfo.Description != null ? v.VolumeInfo.Description : (v.VolumeInfo.Subtitle != null ? v.VolumeInfo.Description : ""),
                                location = "Room4,Column7"
                            };

                            Random random = new Random();
                            // random between 0 , 1 or 2
                            byte b = (byte)(random.Next() * 2);
                            string strYear = v.VolumeInfo.PublishedDate.Split("-")[0];
                            int intYear = int.Parse(strYear);
                            Book book = new Book
                            {
                                Name = v.VolumeInfo.Title,
                                Description = v.VolumeInfo.Description == null ? v.VolumeInfo.Subtitle : v.VolumeInfo.Description,
                                ImageURL = v.VolumeInfo.ImageLinks == null ? "" : (v.VolumeInfo.ImageLinks.Thumbnail != null ? v.VolumeInfo.ImageLinks.Thumbnail : ""),
                                ISBN = v.VolumeInfo.IndustryIdentifiers == null ? "" : (v.VolumeInfo.IndustryIdentifiers[1].Identifier != null ? v.VolumeInfo.IndustryIdentifiers[1].Identifier : ""),
                                Type = b,
                                Year = intYear,
                                AuthorId = Guid.NewGuid(),
                                CategoryId = 0
                            };
                            books.Add(book);
                        }
                        catch (Exception ex) { 
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }

            return volumes;
        }

        static async Task RunAsync()
        {

        }

        public class BulkReturn
        {
            public List<Book> Book { get; set; }
            public Tuple<Book, Category, Author> tuple { get; set; }
        }

        public class Result
        {
            public string Kind { get; set; }
            public int TotalItems { get; set; }
            public List<Volume> Items { get; set; }
        }
        public class Volume
        {
            public string Kind { get; set; }
            public string Id { get; set; }
            public string Etag { get; set; }
            public string SelfLink { get; set; }
            public VolumeInfo VolumeInfo { get; set; }
            public SaleInfo SaleInfo { get; set; }
            public AccessInfo AccessInfo { get; set; }
            public SearchInfo SearchInfo { get; set; }
        }

        public class VolumeInfo
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public List<string> Authors { get; set; }
            public string Publisher { get; set; }
            public string PublishedDate { get; set; }
            public string Description { get; set; }
            public List<IndustryIdentifier> IndustryIdentifiers { get; set; }
            public ReadingModes ReadingModes { get; set; }
            public int PageCount { get; set; }
            public string PrintType { get; set; }
            public List<string> Categories { get; set; }
            public string MaturityRating { get; set; }
            public bool AllowAnonLogging { get; set; }
            public string ContentVersion { get; set; }
            public PanelizationSummary PanelizationSummary { get; set; }
            public ImageLinks ImageLinks { get; set; }
            public string Language { get; set; }
            public string PreviewLink { get; set; }
            public string InfoLink { get; set; }
            public string CanonicalVolumeLink { get; set; }
        }

        public class IndustryIdentifier
        {
            public string Type { get; set; }
            public string Identifier { get; set; }
        }

        public class ReadingModes
        {
            public bool Text { get; set; }
            public bool Image { get; set; }
        }

        public class PanelizationSummary
        {
            public bool ContainsEpubBubbles { get; set; }
            public bool ContainsImageBubbles { get; set; }
        }

        public class ImageLinks
        {
            public string SmallThumbnail { get; set; }
            public string Thumbnail { get; set; }
        }

        public class SaleInfo
        {
            public string Country { get; set; }
            public string Saleability { get; set; }
            public bool IsEbook { get; set; }
            public ListPrice ListPrice { get; set; }
            public RetailPrice RetailPrice { get; set; }
            public string BuyLink { get; set; }
            public List<Offer> Offers { get; set; }
        }

        public class ListPrice
        {
            public double Amount { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class RetailPrice
        {
            public double Amount { get; set; }
            public string CurrencyCode { get; set; }
        }

        public class Offer
        {
            public int FinskyOfferType { get; set; }
            public ListPrice ListPrice { get; set; }
            public RetailPrice RetailPrice { get; set; }
        }

        public class AccessInfo
        {
            public string Country { get; set; }
            public string Viewability { get; set; }
            public bool Embeddable { get; set; }
            public bool PublicDomain { get; set; }
            public string TextToSpeechPermission { get; set; }
            public Epub Epub { get; set; }
            public Pdf Pdf { get; set; }
            public string WebReaderLink { get; set; }
            public string AccessViewStatus { get; set; }
            public bool QuoteSharingAllowed { get; set; }
        }

        public class Epub
        {
            public bool IsAvailable { get; set; }
            public string AcsTokenLink { get; set; }
        }

        public class Pdf
        {
            public bool IsAvailable { get; set; }
            public string AcsTokenLink { get; set; }
        }

        public class SearchInfo
        {
            public string TextSnippet { get; set; }
        }

    }
}
