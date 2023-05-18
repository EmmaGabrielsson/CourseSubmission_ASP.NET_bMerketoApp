﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.Models.Entities;

namespace WebbApp.Services;

public class SeedService
{
    #region Constructors & Private Fields
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly DataContext _context;

    public SeedService(RoleManager<IdentityRole> roleManager, DataContext context)
    {
        _roleManager = roleManager;
        _context = context;
    }
    #endregion

    public async Task InitializeRoles()
    {
        if (!await _roleManager.RoleExistsAsync("admin"))
            await _roleManager.CreateAsync(new IdentityRole("admin"));
        
        if (!await _roleManager.RoleExistsAsync("user"))
            await _roleManager.CreateAsync(new IdentityRole("user"));
    }
    public async Task CreateInitializedDataAsync()
    {
        if (!await _context.Showcases.AnyAsync())
        {
            await _context.AddAsync(new ShowcaseEntity
            {
                Ingress = "WELCOME TO BMERKETO SHOP",
                Title = "Exclusive Chair gold Collection.",
                ImageUrl = "625x647.svg",
                LinkText = "SHOP NOW",
                LinkUrl = "/products"
            });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Tags.AnyAsync())
        {
            await _context.AddAsync(new TagEntity { TagName = "new" });
            await _context.AddAsync(new TagEntity { TagName = "popular" });
            await _context.AddAsync(new TagEntity { TagName = "featured" });
            await _context.AddAsync(new TagEntity { TagName = "summer sale" });
            await _context.AddAsync(new TagEntity { TagName = "outgoing" });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Categories.AnyAsync())
        {
            await _context.AddAsync(new CategoryEntity { CategoryName = "all" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "bag" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "dress" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "decoration" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "essentials" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "interior" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "laptops" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "mobile" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "beauty" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "electronics" });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Products.AnyAsync())
        {
            await _context.AddAsync(new ProductEntity { ArticleNumber = "S1132", ProductName = "Apple watch collection", ImageUrl = "369x310.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "S8812", ProductName = "Apple watch collection", ImageUrl = "369x310.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "25695", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "35685", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "48952", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "52365", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "75214", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "89652", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "96174", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.SaveChangesAsync();

            var popularTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "popular");
            var summerSaleTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "summer sale");
            var allCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "all");
            var beautyCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "beauty");

            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "25695" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "89652" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "96174" });
            await _context.AddAsync(new ProductTagEntity { TagId = summerSaleTag!.Id, ProductId = "S1132" });
            await _context.AddAsync(new ProductTagEntity { TagId = summerSaleTag!.Id, ProductId = "S8812" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "S8812" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "S1132" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "96174" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "89652" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "25695" });

            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "S8812" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "S1132" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "96174" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "89652" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "25695" });
            await _context.SaveChangesAsync();

            if (!await _context.Collections.AnyAsync())
            {
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-25695", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-35685", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-48952", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-52365", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-75214", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-89652", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-96174", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.AddAsync(new ProductEntity { ArticleNumber = "f-06174", ProductName = "Apple watch collection", ImageUrl = "270x295.svg", Ingress = "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", Description = "Discover the Apple Watch Collection—a range of smartwatches that blend technology and style flawlessly. From the flagship Series 7 with its advanced features, to the fashion-forward Apple Watch Hermès and the sporty Apple Watch Nike, there's a perfect fit for everyone. Seamlessly integrated with your iPhone, these watches keep you connected, organized, and empowered. With a wide selection of interchangeable bands and customizable features, personalize your Apple Watch to reflect your unique style and personality. Elevate your daily life with the Apple Watch Collection." });
                await _context.SaveChangesAsync();

                var featuredTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "featured");

                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-25695" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-35685" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-48952" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-52365" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-75214" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-89652" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-96174" });
                await _context.AddAsync(new ProductTagEntity { TagId = featuredTag!.Id, ProductId = "f-06174" });
                await _context.SaveChangesAsync();

                await _context.AddAsync(new CollectionEntity
                {
                    Title = "best collection",
                    ProductIds = await _context.ProductTags.Where(x => x.TagId == featuredTag!.Id).ToListAsync()
                });
            }

            if (!await _context.Stocks.AnyAsync())
            {
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-25695", Price = 30, OnSale = false, Quantity = 0, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-35685", Price = 30, OnSale = false, Quantity = 0, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-52365", Price = 30, OnSale = false, Quantity = 0, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-75214", Price = 30, OnSale = false, Quantity = 25, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-89652", Price = 30, OnSale = false, Quantity = 25, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-96174", Price = 30, OnSale = false, Quantity = 50, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-06174", Price = 30, OnSale = false, Quantity = 50, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "f-48952", Price = 30, OnSale = false, Quantity = 50, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "S1132", Price = 12, OnSale = true, Quantity = 10, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "S8812", Price = 12, OnSale = true, Quantity = 15, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "25695", Price = 30, OnSale = false, Quantity = 125, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "35685", Price = 30, OnSale = false, Quantity = 125, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "48952", Price = 30, OnSale = false, Quantity = 125, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "52365", Price = 30, OnSale = false, Quantity = 105, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "75214", Price = 30, OnSale = false, Quantity = 105, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "89652", Price = 30, OnSale = false, Quantity = 105, StandardCurrency = "USD" });
                await _context.AddAsync(new StockEntity { ArticleNumber = "96174", Price = 30, OnSale = false, Quantity = 125, StandardCurrency = "USD" });
                await _context.SaveChangesAsync();
            }

            if (!await _context.ProductReviews.AnyAsync())
            {
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-25695", Rating = 4, Comment = "Apple Watch Series 6: \n\"The Apple Watch Series 6 is a game-changer! The blood oxygen level monitoring, ECG feature, and always-on display are amazing. It seamlessly integrates with my iPhone and keeps me connected throughout the day.\" \n - Sarah" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-35685", Rating = 5, Comment = "Apple Watch SE: \n\"The Apple Watch SE offers excellent value for money. It has all the essential features like fitness tracking, heart rate monitoring, and notifications. The display is vibrant, and the battery life is impressive. Perfect for someone looking to enter the world of smartwatches.\" \n - Mark" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-52365", Rating = 5, Comment = "Apple Watch Series 5:\n\"I love my Apple Watch Series 5! The always-on display is a game-changer, and the ECG function gives me peace of mind. The build quality and design are top-notch. It's become an indispensable part of my daily routine.\" \n - Emily" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-75214", Rating = 5, Comment = "Apple Watch Series 4:\n\"The Apple Watch Series 4 is a fantastic smartwatch. The larger display, faster performance, and improved heart rate monitoring are remarkable. It looks stylish, and the battery life easily gets me through the day.\" \n - Jason" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-89652", Rating = 4, Comment = "Apple Watch Nike Edition:\n\"The Apple Watch Nike Edition is perfect for fitness enthusiasts like me. The exclusive Nike watch faces, breathable bands, and preloaded Nike Run Club app motivate me to achieve my fitness goals. It's a fantastic combination of style and fitness tracking.\" \n - Michelle" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-96174", Rating = 4, Comment = "The Apple Watch Collection offers an excellent blend of style and functionality. The variety of watch faces and interchangeable bands allow me to customize it to suit my preferences and occasions. The integration with my iPhone is seamless, enabling me to receive notifications, answer calls, and even use Siri directly from my wrist. The watch's performance is snappy, and the display is vibrant and easy to read. However, I would appreciate more third-party app support and improved battery life for longer usage without frequent charging. Overall, the Apple Watch Collection is a solid smartwatch choice for Apple ecosystem users. \n - Jim" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-06174", Rating = 5, Comment = "The Apple Watch Collection is a game-changer! The sleek design and premium materials make it a stylish accessory that complements any outfit. The functionality is outstanding, with a wide range of features like heart rate monitoring, activity tracking, and notifications. The watch face is customizable, allowing me to personalize it to my liking. The battery life is impressive, lasting me through a full day of use. Overall, I highly recommend the Apple Watch Collection to anyone looking for a versatile and reliable smartwatch.\n - Linn" });
                await _context.AddAsync(new ProductReviewEntity { ProductArticleNumber = "f-48952", Rating = 4, Comment = "The Apple Watch Collection is a fantastic fitness companion. The built-in workout tracking features are accurate and comprehensive, helping me monitor my progress and set new fitness goals. The watch's heart rate monitor is incredibly useful during workouts, ensuring that I stay in my target heart rate zone. The GPS functionality allows me to track my outdoor runs accurately. The only downside is that the battery life could be better, especially during extended workouts. Nonetheless, I consider the Apple Watch Collection an essential tool for fitness enthusiasts.\n - Leah" });
                await _context.SaveChangesAsync();
            }
        }
    }

}
