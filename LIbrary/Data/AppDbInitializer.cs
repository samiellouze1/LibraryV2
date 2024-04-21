using LIbrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using static System.Net.WebRequestMethods;

namespace LIbrary.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                #region Genre

                if (!context.Genre.Any())
                {
                    context.Genre.AddRange(new List<Genre>()
                    {
                        new Genre(){Id="1",name="Science Fiction"},
                        new Genre(){Id = "2", name="Mystery"},
                        new Genre(){Id = "3", name="Fantasy"}
                    });
                    context.SaveChanges();
                }
                #endregion

                #region Author
                if (!context.Author.Any())
                {
                    context.Author.AddRange(new List<Author>()
                    {
                        new Author(){Id="1",name="Isaac Asimov"},
                        new Author(){Id = "2", name="Agathe Christie"},
                        new Author(){Id = "3", name="J.R.R. Tolkien"}
                    });
                    context.SaveChanges();
                }
                #endregion

                #region Book
                if (!context.Book.Any()) 
                {
                    #region covers
                    string cover1 = "https://strangerthansf.com/scans/asimov-foundation.jpg";
                    string cover2 = "https://i.pinimg.com/originals/2d/1e/94/2d1e94ad9c9f89d4b89a33117f4c9134.jpg";
                    string cover3 = "https://th.bing.com/th/id/R.7e0e06b29187a30474e7f1fb5a9c5a00?rik=dOvbOHrJlio2EQ&pid=ImgRaw&r=0";
                    string cover4 = "https://th.bing.com/th/id/R.b153b1ff86943c88c6e83069209bc60c?rik=5vgr%2bkLgFYbk4g&pid=ImgRaw&r=0";
                    string cover5 = "https://th.bing.com/th/id/OIP.40xWQTKonwflRrgtTLcfZQHaL6?rs=1&pid=ImgDetMain";
                    string cover6 = "https://i.harperapps.com/covers/9780061763403/y648.jpg";
                    string cover7 = "https://th.bing.com/th/id/OIP.u1jvgQh_S5zIR3e4uX5oNwHaLH?rs=1&pid=ImgDetMain";
                    string cover8 = "https://th.bing.com/th/id/OIP.kIfx-8GhAWZxwSpLwjPb_AHaLH?rs=1&pid=ImgDetMain";
                    string cover9 = "https://i.harperapps.com/hcanz/covers/9780008433949/y648.jpg";
                    #endregion
                    #region author
                    Author firstAuthor  = context.Author.FirstOrDefault(a => a.Id == "1");
                    Author secondAuthor  = context.Author.FirstOrDefault(a => a.Id == "2");
                    Author thirdAuthor  = context.Author.FirstOrDefault(a => a.Id == "3");
                    #endregion
                    #region genre
                    Genre firstGenre = context.Genre.FirstOrDefault(g=>g.Id=="1");
                    Genre secondGenre = context.Genre.FirstOrDefault(g=>g.Id=="2");
                    Genre thirdGenre = context.Genre.FirstOrDefault(g=>g.Id=="3");
                    #endregion
                    context.Book.AddRange(new List<Book>()
                    {
                        //lezem tzid rating
                        new Book(){ Id="1",coverUrl=cover1,title = "Foundation", description = "A series of novels set in a future where mathematician Hari Seldon develops a theory of psychohistory to predict the future of human civilization.", dateOfCreation = DateTime.Now, price = 20, author=firstAuthor ,genre= firstGenre},
                        new Book(){ Id="2",coverUrl=cover2,title = "I, Robot", description = "A collection of interconnected short stories exploring the relationships between humans and robots, and the ethical implications of artificial intelligence.", dateOfCreation = DateTime.Now, price = 20, author=firstAuthor ,genre= secondGenre},
                        new Book(){ Id="3",coverUrl=cover3,title = "The Caves of Steel", description = "A science fiction mystery novel featuring detective Elijah Baley and his robot partner, R. Daneel Olivaw, as they investigate a murder on a futuristic Earth.", dateOfCreation = DateTime.Now, price = 20, author=firstAuthor ,genre= thirdGenre},
                        new Book(){ Id="4",coverUrl=cover4,title = "Murder on the Orient Express", description = "One of Christie's most famous novels, featuring the brilliant Belgian detective Hercule Poirot solving a murder mystery aboard the luxurious Orient Express train.", dateOfCreation = DateTime.Now, price = 20, author=secondAuthor ,genre= firstGenre},
                        new Book(){ Id="5",coverUrl=cover5,title = "And Then There Were None", description = "A classic mystery novel where ten strangers are invited to a remote island and are mysteriously killed off one by one, with the remaining guests trying to uncover the identity of the murderer.", dateOfCreation = DateTime.Now, price = 20, author=secondAuthor ,genre= secondGenre},
                        new Book(){ Id="6",coverUrl=cover6,title = "The Murder of Roger Ackroyd", description = "A groundbreaking mystery novel known for its clever twist ending, featuring amateur detective Hercule Poirot investigating the murder of a wealthy man in a small English village.", dateOfCreation = DateTime.Now, price = 20, author=secondAuthor ,genre= thirdGenre},
                        new Book(){ Id="7",coverUrl=cover7,title = "The Hobbit", description = "A beloved fantasy novel following the adventures of Bilbo Baggins, a hobbit who embarks on a quest with a group of dwarves and the wizard Gandalf to reclaim their homeland from the dragon Smaug.", dateOfCreation = DateTime.Now, price = 20, author=thirdAuthor ,genre= firstGenre},
                        new Book(){ Id="8",coverUrl=cover8,title = "The Fellowship of the Ring", description = "The first volume of Tolkien's epic high fantasy series, following the journey of Frodo Baggins and his companions as they seek to destroy the One Ring and defeat the Dark Lord Sauron.", dateOfCreation = DateTime.Now, price = 20, author=thirdAuthor ,genre= secondGenre},
                        new Book(){ Id="9",coverUrl=cover9,title = "The Silmarillion", description = "A collection of mythopoeic works that delve into the mythology and history of Middle-earth, including the creation myth, the deeds of the Valar and Maiar, and the tales of Elves, Men, and Dwarves.", dateOfCreation = DateTime.Now, price = 20, author=thirdAuthor ,genre= thirdGenre}
                    });
                    context.SaveChanges();
                }
                #endregion

                #region BookCopy
                if (!context.BookCopy.Any())
                {
                    List<BookCopy> bookCopies = new List<BookCopy>();
                    int k = 0;
                    for (int i=1; i < 10; i++)
                    {
                        for (int j=1;j<3;j++)
                        {
                            k++;
                            bookCopies.Add(new BookCopy() { Id = k.ToString(),book=context.Book.FirstOrDefault(b=>b.Id==i.ToString()) }) ;
                        }
                    }
                    context.BookCopy.AddRange(bookCopies);
                    context.SaveChanges();
                }
                #endregion

                #region BorrowItemStatus
                if (!context.BorrowItemStatus.Any())
                {
                    context.BorrowItemStatus.AddRange(new List<BorrowItemStatus>()
                    {
                        // Assign objects retrieved from context to navigation properties
                        new BorrowItemStatus { Id = "1",  name="Borrowed"},
                        new BorrowItemStatus { Id = "2",  name="Returned"},
                    });
                    context.SaveChanges();
                }
                #endregion

                #region ReviewRating
                if (!context.ReviewRating.Any())
                {
                    context.ReviewRating.AddRange(new List<ReviewRating>()
                    {
                        new ReviewRating {Id="1", rating=5,review="Had a great time reading this! totally recommend this book, especially the last part, when THAT thing happened..."},
                        new ReviewRating {Id="2", rating=1,review="Not worth it, I was expecting other things, but It can be me"}
                    });
                    context.SaveChanges();
                }
                #endregion

                #region BorrowItem
                if (!context.BorrowItem.Any())
                {
                    // bookCopy
                    var bookCopy1 = context.BookCopy.FirstOrDefault(bc => bc.Id == "1");
                    var bookCopy2 = context.BookCopy.FirstOrDefault(bc => bc.Id == "2");
                    var bookCopy3 = context.BookCopy.FirstOrDefault(bc => bc.Id == "3");
                    // borrowItem
                    var borrowedBorrowItemStatus = context.BorrowItemStatus.FirstOrDefault(bis => bis.Id == "1");
                    var returnedBorrowItemStatus = context.BorrowItemStatus.FirstOrDefault(bis => bis.Id == "2");
                    // reader
                    var reader1 = context.Reader.FirstOrDefault(r => r.Id == "1");
                    var reader2 = context.Reader.FirstOrDefault(r => r.Id == "2");
                    var reader3 = context.Reader.FirstOrDefault(r => r.Id == "3");
                    // review
                    var reviewRatingGood = context.ReviewRating.FirstOrDefault(r => r.Id == "1");
                    var reviewRatingBad = context.ReviewRating.FirstOrDefault(r => r.Id == "2");

                    context.BorrowItem.AddRange(new List<BorrowItem>()
                    {
                        new BorrowItem { Id = "1",  bookCopy = bookCopy1 ,borrowItemStatus=borrowedBorrowItemStatus,reader=reader2},
                        new BorrowItem { Id = "2",  bookCopy = bookCopy2 ,borrowItemStatus=borrowedBorrowItemStatus,reader=reader3},
                        new BorrowItem { Id = "3",  bookCopy = bookCopy3,borrowItemStatus=borrowedBorrowItemStatus,reader=reader1},
                        new BorrowItem { Id = "4",  bookCopy = bookCopy1,borrowItemStatus=returnedBorrowItemStatus,reader=reader1,reviewRating=reviewRatingGood,startDate= new DateTime(2024,1,1),supposedEndDate= new DateTime(2024,3,1),endDate= new DateTime(2024,2,1)},
                        new BorrowItem { Id = "5",  bookCopy = bookCopy1, borrowItemStatus=returnedBorrowItemStatus,reader=reader3,reviewRating=reviewRatingBad,startDate= new DateTime(2024,1,1),supposedEndDate=new DateTime(2024,2,1) ,endDate=  new DateTime(2024,3,1)},
                        // Additional BorrowItems
                        new BorrowItem { Id = "6",  bookCopy = bookCopy2 ,borrowItemStatus=returnedBorrowItemStatus,reader=reader2,reviewRating=reviewRatingGood,startDate= new DateTime(2024,2,1),supposedEndDate= new DateTime(2024,4,1),endDate= new DateTime(2024,3,15)},
                        new BorrowItem { Id = "7",  bookCopy = bookCopy3 ,borrowItemStatus=returnedBorrowItemStatus,reader=reader1,reviewRating=reviewRatingBad,startDate= new DateTime(2024,2,1),supposedEndDate= new DateTime(2024,3,15),endDate= new DateTime(2024,4,1)}
                    });
                    context.SaveChanges();
                }
                #endregion

                #region FineStatus
                if (!context.FineStatus.Any() )
                {
                    context.FineStatus.AddRange(new List<FineStatus>()
                    {
                        new FineStatus { Id = "1",status=false}, //not paid
                        new FineStatus { Id = "2",status=true} //paid
                    });
                    context.SaveChanges();
                }
                #endregion

                #region Fine
                if (!context.Fine.Any())
                {
                    BorrowItem borrowItem5 = context.BorrowItem.FirstOrDefault(bi => bi.Id == "5");
                    FineStatus fineStatusNotPaid = context.FineStatus.FirstOrDefault(fs => fs.Id == "1");
                    context.Fine.AddRange(new List<Fine>()
                    {
                        new Fine { Id = "1", borrowItem=borrowItem5,fineStatus=fineStatusNotPaid },
                        // Additional fines
                        new Fine { Id = "2", borrowItem=context.BorrowItem.FirstOrDefault(bi=>bi.Id=="6"),fineStatus=fineStatusNotPaid },
                        new Fine { Id = "3", borrowItem=context.BorrowItem.FirstOrDefault(bi=>bi.Id=="7"),fineStatus=fineStatusNotPaid }
                    });
                    context.SaveChanges();
                }
                #endregion
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationbuilder)
        {
            using (var serviceScope = applicationbuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                #region roles
                if (!await roleManager.RoleExistsAsync(UserRoles.Reader))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Reader));
                #endregion
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Reader>>();
                #region users
                //reader 1
                string readerEmail = "Reader@library.com";
                var reader = await userManager.FindByEmailAsync(readerEmail);
                if (reader == null)
                {
                    Reader newReader = new Reader() { Id="1",UserName = readerEmail, Email = readerEmail ,ImageUrl= "https://scontent.ftun14-1.fna.fbcdn.net/v/t1.6435-9/120645766_3263034770470903_6610932504759370937_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=5f2048&_nc_ohc=MjAGUgxoQTEAb7cbh1m&_nc_ht=scontent.ftun14-1.fna&oh=00_AfCUhyYkS6raLADU1zRYHzz_eWuO9wkW5gi3Hon2KylK3Q&oe=663BE137",JoinedOn=DateTime.Now,Name="Sami" };
                    await userManager.CreateAsync(newReader,"Reader123@");
                    await userManager.AddToRoleAsync(newReader, UserRoles.Reader);
                }
                //reader 2
                string secondaryreaderEmail = "SecondReader@library.com";
                var secondaryreader = await userManager.FindByEmailAsync(secondaryreaderEmail);
                if (reader == null)
                {
                    Reader newReader = new Reader() { Id = "2", UserName = secondaryreaderEmail, Email = secondaryreaderEmail, ImageUrl= "https://scontent.ftun14-1.fna.fbcdn.net/v/t39.30808-6/289057250_4012267192331843_3921978455873349113_n.jpg?_nc_cat=101&ccb=1-7&_nc_sid=5f2048&_nc_ohc=jxNEaQzp_J4Ab50NQx9&_nc_ht=scontent.ftun14-1.fna&oh=00_AfDbLK4fp7BhyfqQLJZF6xA1_Eb-l3wFmBL3YYzu4VKGWg&oe=661A58A0",JoinedOn=DateTime.Now,Name="Issam" };
                    await userManager.CreateAsync(newReader, "Reader123@");
                    await userManager.AddToRoleAsync(newReader, UserRoles.Reader);
                }
                //reader 3
                string thirdreaderEmail = "ThirdReader@library.com";
                var thirdreader = await userManager.FindByEmailAsync(thirdreaderEmail);
                if (reader == null)
                {
                    Reader newReader = new Reader() { Id = "3", UserName = thirdreaderEmail, Email = thirdreaderEmail,ImageUrl= "https://scontent.ftun14-1.fna.fbcdn.net/v/t1.6435-9/48425571_2673113052706647_5301663967544868864_n.jpg?_nc_cat=103&ccb=1-7&_nc_sid=5f2048&_nc_ohc=zfKDpEP7snsAb7WtfLM&_nc_ht=scontent.ftun14-1.fna&oh=00_AfDYXePutyDaK2AJy6SE7f52odaz30f1jbkl2h3g_TTSAA&oe=663BF816", JoinedOn = DateTime.Now,Name="Hsan" };
                    await userManager.CreateAsync(newReader, "Reader123@");
                    await userManager.AddToRoleAsync(newReader, UserRoles.Reader);
                }
                #endregion
            }
        }
    }
}
