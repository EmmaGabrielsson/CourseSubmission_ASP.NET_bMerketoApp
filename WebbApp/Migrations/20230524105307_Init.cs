using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebbApp.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    CheckOut = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Ingress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ArticleNumber);
                });

            migrationBuilder.CreateTable(
                name: "Showcases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ingress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showcases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "money", nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRows", x => new { x.OrderId, x.ProductArticleNumber });
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ArticleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductArticleNumber",
                        column: x => x.ProductArticleNumber,
                        principalTable: "Products",
                        principalColumn: "ArticleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ProductArticleNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    OnSale = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<decimal>(type: "money", nullable: false),
                    StandardCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductArticleNumber);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductArticleNumber",
                        column: x => x.ProductArticleNumber,
                        principalTable: "Products",
                        principalColumn: "ArticleNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => new { x.ProductId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ProductTags_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ArticleNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "all" },
                    { 2, "bag" },
                    { 3, "dress" },
                    { 4, "decoration" },
                    { 5, "essentials" },
                    { 6, "interior" },
                    { 7, "laptops" },
                    { 8, "mobile" },
                    { 9, "beauty" },
                    { 10, "shoes" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ArticleNumber", "Description", "ImageUrl", "Ingress", "ProductName", "VendorName" },
                values: new object[,]
                {
                    { "35685", null, "270x295.svg", "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you.", "Beauty collection", null },
                    { "B-45678", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "pexels-backpack.jpg", "Experience rugged elegance with our Forest-themed Backpack. Combining durability and style, this backpack features earthy tones inspired by the enchanting colors of the woods. Prepare for your outdoor adventures with confidence, as this backpack offers ample storage and ergonomic comfort. Embrace the wilderness in style with our powerful Forest-colored backpack.", "Nature's Trail Backpack", null },
                    { "B-67898", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "pexels-bluebag.jpg", "Unleash your style with our Blue Horizon Backpack. This vibrant and versatile accessory combines sleek aesthetics with ample storage space and ergonomic comfort. Stand out from the crowd with the captivating blue hue, perfect for urban exploration or outdoor adventures. Elevate your everyday carry with the Blue Horizon Backpack and make a bold statement wherever you go.", "Skyline Explorer Backpack", null },
                    { "BE-75214", null, "pexels-diamondheels.jpg", "Elevate your style to new heights with our White Strass Small High Heels. Designed for weddings and special events, these heels exude a glamorous charm. The dazzling strass embellishments add a touch of sparkle and elegance, while the small heel offers both comfort and sophistication. Step into a world of blissful elegance and make a statement at your special occasion with our glamorous White Strass Small High Heels.", "White Strass Small High Heels", null },
                    { "BE-89214", null, "pexels-greenheels.jpg", " Step into a world of refined beauty with our Verdant Elegance: Green Small High Heels. These captivating heels in a stunning green shade are perfect for adding a touch of sophistication to your wedding or special event attire. The sleek and stylish design complements a variety of outfits, while the small heel offers both comfort and grace. Elevate your look with the natural allure of our Green Small High Heels and make a statement of elegant charm.", "Green Small High Heels", null },
                    { "DE-15963", null, "pexels-mirror.jpg", "Introduce a touch of contemporary elegance with our Round Mirror. This sleek and stylish mirror enhances the aesthetic of any space with its perfectly rounded shape. Its smooth edges and minimalist design create a modern and sophisticated look, making it a versatile addition to any room. Hang it on your wall to reflect light and open up your space, or use it as a chic decorative piece. Elevate your decor with the timeless appeal of our Round Mirror, adding a touch of elegance and functionality to your surroundings.", "Sleek and Stylish Mirror", null },
                    { "DE-45965", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "pexels-decostar.jpg", "Elevate your space with the ethereal beauty of our Blue, Gray, and White Paper Stars. These exquisite decorations effortlessly infuse a sense of celestial elegance into any setting. Hang them, dangle them, or scatter them for a mesmerizing effect. With their delicate craftsmanship and a palette of soothing blue, elegant gray, and pristine white tones, these paper stars create a serene and captivating atmosphere. Let your imagination take flight as you adorn your surroundings with the Stellar Elegance of our Blue, Gray, and White Paper Stars.", "Blue/Gray/White Paper Stars", null },
                    { "DE-52365", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "pexels-balloons.jpg", "Experience the vibrant fusion of colors with our Yellow and Black Balloon Decorations. These captivating decorations effortlessly add a pop of energy and style to your event. Whether it's a birthday, wedding, or festive celebration, the dynamic combination of yellow and black creates an atmosphere of excitement and elegance. Crafted to impress, our high-quality balloons are designed to make a lasting impact. Elevate your decor with the lively charm of our Yellow and Black Balloon Decorations and let the festivities begin.", "Yellow and Black Balloon Decorations", null },
                    { "DR-22896", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-yellowdress.jpg", "This delightful dress radiates joy and positivity, perfect for brightening up any occasion. The vibrant yellow hue captures the essence of sunny days, while the lightweight and flowy fabric ensure comfort and ease. Whether you're strolling on the beach or attending a summer gathering, this dress is sure to turn heads and spread cheer. Experience the happiness of summer with our Sunshine Delight: Happy Yellow Summer Dress and let your style shine.", "Sunshine Summer Dress", null },
                    { "DR-45875", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-whitedress.jpg", "This elegant and timeless piece exudes pure elegance, perfect for warm-weather occasions. The crisp white color adds a touch of sophistication, while the lightweight fabric keeps you cool and comfortable. Whether you're attending a beach party or enjoying a picnic in the park, our White Summer Dress is a must-have for effortless style. Step into summer with pure elegance in our White Summer Dress.", "White Summer Dress", null },
                    { "FE-12345", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "Apple_watch-series-6-blue.jpg", "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", "Apple Watch Series 6 (blue)", null },
                    { "FE-23456", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "Apple_watch-series-6.jpg", "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", "Apple Watch Series 6 (white)", null },
                    { "FE-34567", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "Apple_watch-series-6-coral.jpg", "Discover the Apple Watch Collection – a fusion of innovation and style. Stay connected, track your fitness, stream music, make payments, and more, all from your wrist. Personalize your watch with a variety of bands and enjoy a stunning Retina display. Seamlessly integrated with your iPhone, the Apple Watch Collection is the ultimate accessory for convenience and fashion-forward individuals. Elevate your wrist today.", "Apple Watch Series 6 (coral)", null },
                    { "GC-58966", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.\n\nSed efficitur lobortis luctus. Quisque non purus justo. Nunc convallis hendrerit elit, id cursus lacus laoreet ut. Fusce tempus libero eu dui interdum, a posuere velit congue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nullam at mi lorem. Suspendisse varius nulla quis ligula tristique, in aliquet mauris interdum. Curabitur eget metus id ex euismod pellentesque. Fusce ut finibus nunc.", "pexels-goldchair.jpg", "Discover pure elegance with our White Exclusive Chair. Featuring exquisite wooden \"legs\" and a pristine white upholstery, this chair adds a touch of sophistication to any space. Experience unparalleled style and comfort in one stunning piece of furniture.", "Exclusive Chair", null },
                    { "IN-85875", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-tableandchairs.jpg", "Enhance your dining area with our Modern Dining Set featuring a sleek white tabletop and sturdy wooden legs. This contemporary design effortlessly combines style and functionality, creating a chic and inviting space for gatherings. With six chairs included, this dining set offers ample seating for family and friends. Experience the perfect blend of modern aesthetics and timeless charm with our Modern Dining Set: White Top, Wooden Legs.", "Modern Dining Set: Table with 6 chairs", null },
                    { "IN-89652", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-interior.jpg", "Transform your space with our Hexagonal Shelves Trio in Forest Green, Gray, and White. These three stylish shelves offer a modern and versatile storage solution for your interior. With their unique hexagonal design and varying sizes, they create an eye-catching display for your favorite items. The forest green, gray, and white colors add a touch of sophistication and complement any decor style. Enhance your organization and aesthetic appeal with our Hexagonal Shelves Trio, a perfect addition to elevate your interior space.", "Trio of Hexagonal Shelves", null },
                    { "L-96174", null, "pexels-laptop.jpg", "Unleash the power of productivity with the ThinkPad Lenovo laptop. Engineered for performance and reliability, this sleek device is designed to elevate your computing experience. With its cutting-edge features and robust build quality, the ThinkPad Lenovo delivers exceptional speed, efficiency, and durability. Whether you're tackling demanding work tasks or enjoying multimedia entertainment, this laptop is your ultimate companion. Experience the perfect blend of power and performance with the ThinkPad Lenovo and stay ahead of the game.", "Laptop Thinkpad Lenovo", null },
                    { "LA-12856", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-tablelamp.jpg", " Illuminate your space with our sleek and stylish Black Table Lamp. This elegant lamp combines contemporary design with functional lighting, making it a perfect addition to any room. The sleek black finish adds a touch of sophistication, while the clean lines and minimalist aesthetic create a modern and chic look. Whether placed on a desk, nightstand, or side table, this black table lamp effortlessly enhances the ambiance of your space.", "Stylish Black Table Lamp", null },
                    { "M-66124", null, "pexels-samsung.jpg", "Experience the future with the Samsung S22. This innovative smartphone combines advanced features, powerful performance, and a sleek design that's bound to impress. With its immersive display, lightning-fast speed, and cutting-edge camera technology, the Samsung S22 takes mobile excellence to a whole new level. Stay ahead of the curve and embrace the next generation of innovation with the Samsung S22.", "Samsung S22: Next-Gen Innovation", null },
                    { "M-77124", null, "pexels-whiteappleserie.jpg", "Experience the epitome of seamless connectivity with Apple Mobile & Watch. These cutting-edge devices combine innovation and style to keep you effortlessly connected throughout your day. The Apple Mobile offers a sleek and intuitive interface, while the Apple Watch provides convenience and functionality right on your wrist. Stay connected, track your fitness, and enjoy the best of Apple's innovation with this dynamic duo. Elevate your digital lifestyle with Apple Mobile & Watch and discover a new level of connected innovation.", "Apple Mobile & Watch: Connected Innovation", null },
                    { "SH-77124", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-shoes.jpg", "Elevate your footwear game with Nike Shoes. Combining style and performance, these shoes offer a perfect blend of fashion and function. From running to training, Nike Shoes provide unparalleled comfort and support for all your active pursuits. With a wide selection of designs and colors, you can find the perfect pair to express your personal style. Step into the world of Nike Shoes and experience the ultimate combination of style and performance.", "Nike Shoes", null },
                    { "SH-89514", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-nikeshoes.jpg", "Elevate your footwear game with Nike Shoes. Combining style and performance, these shoes offer a perfect blend of fashion and function. From running to training, Nike Shoes provide unparalleled comfort and support for all your active pursuits. With a wide selection of designs and colors, you can find the perfect pair to express your personal style. Step into the world of Nike Shoes and experience the ultimate combination of style and performance.", "Nike Shoes: Style and Performance", null },
                    { "SH-96315", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-whiteshoes.jpg", "Experience pure performance with our White Running Shoe. This sleek and stylish shoe is designed to enhance your running experience with its optimal comfort and support. The crisp white color adds a touch of timeless elegance to your athletic ensemble. Whether you're sprinting on the track or hitting the trails, our White Running Shoe delivers the perfect combination of functionality and style. Step up your running game with pure confidence in our White Running Shoe.", "White Running Shoe", null },
                    { "SH-99875", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque rutrum mattis ante, eu cursus turpis volutpat vitae. Nullam consectetur pellentesque nibh, eu aliquet mauris bibendum in. Phasellus id diam nisl. Mauris sollicitudin, tortor sed rutrum tincidunt, turpis ligula pellentesque lorem, eu pulvinar mauris purus eu nisl. Sed non enim felis. Nullam non varius ipsum. Aliquam at tristique ex. Pellentesque pulvinar finibus tristique. Morbi a dui id lorem tincidunt aliquam ac vel erat.", "pexels-summer.jpg", "Step into summer style with our Blue Beach Slippers. These essential footwear options are perfect for your beach excursions. The vibrant blue color adds a pop of freshness, while the comfortable design ensures a relaxed and enjoyable experience. Whether you're walking along sandy shores or lounging by the water, our Blue Beach Slippers are the perfect companion for your beach adventures. Embrace the summer vibes with our must-have Blue Beach Slippers.", "Blue Beach Slippers: Summer Essentials", null },
                    { "T-44183", null, "pexels-Lamp.jpg", " Elevate your space with our Large Ceiling Lamp featuring a captivating beige straw-like texture. This stunning fixture combines style and sophistication, adding a touch of natural elegance to any room. With its expansive size and unique texture, this ceiling lamp becomes a focal point, enhancing the overall aesthetic of your space. Experience the charm of our Beige Straw-Like Texture: Large Ceiling Lamp and illuminate your surroundings with exquisite beauty.", "Straw-Like Ceiling Lamp", null }
                });

            migrationBuilder.InsertData(
                table: "Showcases",
                columns: new[] { "Id", "CreatedDate", "ImageUrl", "Ingress", "LinkText", "LinkUrl", "Title" },
                values: new object[] { 1, new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(275), "625x647.svg", "WELCOME TO BMERKETO SHOP", "SHOP NOW", "/products", "Exclusive Chair gold Collection." });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "TagName" },
                values: new object[,]
                {
                    { 1, "new" },
                    { 2, "popular" },
                    { 3, "featured" },
                    { 4, "summer sale" },
                    { 5, "outgoing" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 9, "35685" },
                    { 2, "B-45678" },
                    { 2, "B-67898" },
                    { 10, "BE-75214" },
                    { 10, "BE-89214" },
                    { 6, "DE-15963" },
                    { 4, "DE-45965" },
                    { 4, "DE-52365" },
                    { 3, "DR-22896" },
                    { 3, "DR-45875" },
                    { 8, "FE-12345" },
                    { 8, "FE-23456" },
                    { 8, "FE-34567" },
                    { 6, "GC-58966" },
                    { 6, "IN-85875" },
                    { 6, "IN-89652" },
                    { 7, "L-96174" },
                    { 6, "LA-12856" },
                    { 8, "M-66124" },
                    { 8, "M-77124" },
                    { 10, "SH-77124" },
                    { 10, "SH-89514" },
                    { 10, "SH-96315" },
                    { 5, "SH-99875" },
                    { 6, "T-44183" }
                });

            migrationBuilder.InsertData(
                table: "ProductReviews",
                columns: new[] { "Id", "Comment", "Created", "ProductArticleNumber", "Rating" },
                values: new object[,]
                {
                    { new Guid("0f7e5a7c-2e8b-4d7e-9b4a-f3f1eae8435a"), "This backpack is a must-have! It offers ample storage space, durability, and a stylish design that suits any adventure", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(671), "B-45678", 4 },
                    { new Guid("13e21e76-bff3-4e20-a2b8-13e8c6c452b3"), "The Apple Watch Collection is a game-changer! The sleek design and premium materials make it a stylish accessory that complements any outfit. The functionality is outstanding, with a wide range of features like heart rate monitoring, activity tracking, and notifications. The watch face is customizable, allowing me to personalize it to my liking. The battery life is impressive, lasting me through a full day of use. Overall, I highly recommend the Apple Watch Collection to anyone looking for a versatile and reliable smartwatch.\n - Linn", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(659), "FE-34567", 5 },
                    { new Guid("2b5e1f16-5c18-4f76-8f13-34e4055cfc99"), "The Apple Watch Series 6 is a game-changer! The blood oxygen level monitoring, ECG feature, and always-on display are amazing. It seamlessly integrates with my iPhone and keeps me connected throughout the day.\" \n - Sarah", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(625), "GC-58966", 4 },
                    { new Guid("356b83b2-8a44-4e62-a1db-48f7a62e3518"), "This running shoe is a game-changer! The cushioning and support provided are exceptional, making my runs comfortable and enjoyable. - Nicki", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(683), "SH-96315", 4 },
                    { new Guid("48dc2404-39d3-4d9a-ae22-18c01c871e18"), "This table lamp is a true gem! Its modern design and soft glow create a cozy ambiance in any room. The quality craftsmanship is evident, and it adds a stylish touch to my decor. - Laura", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(674), "LA-12856", 5 },
                    { new Guid("63885324-214a-4a41-a8d9-209209a303e2"), "This backpack is a reliable companion! It provides sufficient storage for all my essentials, and its sturdy construction ensures durability. While I found the design to be simple, it serves its purpose well. - Tim", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(670), "B-67898", 3 },
                    { new Guid("76b369f8-7c02-47ca-9d7b-61f983b8f5e5"), "I love my Apple Watch Series 6! The always-on display is a game-changer, and the ECG function gives me peace of mind. The build quality and design are top-notch. It's become an indispensable part of my daily routine.\" \n - Emily", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(648), "GC-58966", 5 },
                    { new Guid("8eaa720c-1d2b-42cc-b8b9-11de0b37c708"), "The Apple Watch Collection is a fantastic fitness companion. The built-in workout tracking features are accurate and comprehensive, helping me monitor my progress and set new fitness goals. The watch's heart rate monitor is incredibly useful during workouts, ensuring that I stay in my target heart rate zone. The GPS functionality allows me to track my outdoor runs accurately. The only downside is that the battery life could be better, especially during extended workouts. Nonetheless, I consider the Apple Watch Collection an essential tool for fitness enthusiasts.\n - Leah", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(661), "FE-34567", 4 },
                    { new Guid("9aeeea0d-4f26-4167-8b3a-43236701feda"), "These shoes exceeded my expectations! With their comfortable fit and stylish design, they're a great choice for any occasion. - Jim", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(679), "SH-77124", 4 },
                    { new Guid("9c49744f-7ef7-4c1e-9a46-6b81b19fc7c5"), "These beach slippers are a summer essential! They offer a comfortable fit, great traction, and are perfect for sandy beaches and poolside lounging. I rate them 3/5 for their decent comfort and practicality. - Cindy", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(688), "SH-99875", 3 },
                    { new Guid("9d352e0b-8ad0-4767-98cb-b87f871f7380"), "The Apple Watch Collection is a game-changer! The sleek design and premium materials make it a stylish accessory that complements any outfit. The functionality is outstanding, with a wide range of features like heart rate monitoring, activity tracking, and notifications. The watch face is customizable, allowing me to personalize it to my liking. The battery life is impressive, lasting me through a full day of use. Overall, I highly recommend the Apple Watch Collection to anyone looking for a versatile and reliable smartwatch.\n - Michelle", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(653), "FE-23456", 4 },
                    { new Guid("a6373522-648a-4cbb-b6ea-4a5fba0ae001"), "Stunning white dress! Elegant, versatile, and comfortable. Perfect for any occasion. Highly recommended! - Linn", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(667), "DR-45875", 5 },
                    { new Guid("b45fc2df-853c-4d53-8ef0-ade4a64b536e"), "These shoes are amazing! Comfortable, stylish, and perfect for any occasion. Highly recommended! - Jake", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(677), "SH-77124", 5 },
                    { new Guid("c5af6803-4e94-4e0e-b7a2-1be02f77920e"), "The Apple Watch Series 6 is a fantastic smartwatch. The larger display, faster performance, and improved heart rate monitoring are remarkable. It looks stylish, and the battery life easily gets me through the day.\" \n - Jason", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(650), "FE-23456", 5 },
                    { new Guid("d00f5cbf-ded4-4aa9-b19a-6363de249c85"), "Love this dress! The yellow color is vibrant and perfect for summer. It's comfortable, flattering, and the ruffled hem adds a fun touch. Received lots of compliments. Highly recommended! - Leah", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(664), "DR-22896", 5 },
                    { new Guid("d7e169c1-3f3f-4cb1-8975-942fa593f2d1"), "I think this mirror is simply stunning! Its sleek design and impeccable craftsmanship add a touch of elegance to any space. - Laura", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(681), "DE-15963", 4 },
                    { new Guid("e826087d-9f5a-4dab-a1c2-97d8de8a04e2"), "The Apple Watch Series 6 is a fantastic smartwatch. The larger display, faster performance, and improved heart rate monitoring are remarkable. It looks stylish, and the battery life easily gets me through the day.\" \n - Mark", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(644), "GC-58966", 5 },
                    { new Guid("e853b69c-7a3e-4a97-9a4e-8157e9b44c47"), "These shoes are amazing! Highly recommended! - Sara", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(686), "BE-89214", 5 },
                    { new Guid("f7a2b4c9-5f96-4be0-8768-8029a2dbbdd2"), "The Apple Watch Collection offers an excellent blend of style and functionality. The variety of watch faces and interchangeable bands allow me to customize it to suit my preferences and occasions. The integration with my iPhone is seamless, enabling me to receive notifications, answer calls, and even use Siri directly from my wrist. The watch's performance is snappy, and the display is vibrant and easy to read. However, I would appreciate more third-party app support and improved battery life for longer usage without frequent charging. Overall, the Apple Watch Collection is a solid smartwatch choice for Apple ecosystem users. \n - Jim", new DateTime(2023, 5, 24, 12, 53, 7, 468, DateTimeKind.Local).AddTicks(656), "FE-34567", 4 }
                });

            migrationBuilder.InsertData(
                table: "ProductTags",
                columns: new[] { "ProductId", "TagId", "CollectionId" },
                values: new object[,]
                {
                    { "B-45678", 3, null },
                    { "B-67898", 1, null },
                    { "BE-89214", 2, null },
                    { "DE-15963", 3, null },
                    { "DE-52365", 5, null },
                    { "DR-45875", 3, null },
                    { "FE-12345", 2, null },
                    { "FE-23456", 2, null },
                    { "FE-23456", 3, null },
                    { "FE-34567", 2, null },
                    { "GC-58966", 1, null },
                    { "GC-58966", 2, null },
                    { "IN-85875", 3, null },
                    { "IN-89652", 1, null },
                    { "L-96174", 3, null },
                    { "LA-12856", 3, null },
                    { "M-77124", 1, null },
                    { "SH-77124", 3, null },
                    { "SH-99875", 2, null },
                    { "SH-99875", 4, null },
                    { "T-44183", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "ProductArticleNumber", "Discount", "OnSale", "Price", "Quantity", "StandardCurrency" },
                values: new object[,]
                {
                    { "35685", 0m, false, 30m, 0, "USD" },
                    { "B-45678", 0m, false, 30m, 50, "USD" },
                    { "B-67898", 0m, false, 30m, 50, "USD" },
                    { "BE-75214", 0m, false, 12m, 15, "USD" },
                    { "BE-89214", 0m, false, 30m, 125, "USD" },
                    { "DE-15963", 0m, false, 30m, 125, "USD" },
                    { "DE-45965", 0m, false, 12m, 10, "USD" },
                    { "DE-52365", 0m, false, 30m, 50, "USD" },
                    { "DR-22896", 0m, false, 30m, 125, "USD" },
                    { "DR-45875", 0m, false, 30m, 125, "USD" },
                    { "FE-12345", 0m, false, 30m, 25, "USD" },
                    { "FE-23456", 0m, false, 30m, 0, "USD" },
                    { "FE-34567", 0.85m, true, 30m, 25, "USD" },
                    { "GC-58966", 0m, false, 30m, 0, "USD" },
                    { "IN-85875", 0m, false, 30m, 125, "USD" },
                    { "IN-89652", 0m, false, 30m, 125, "USD" },
                    { "L-96174", 0m, false, 30m, 125, "USD" },
                    { "LA-12856", 0m, false, 30m, 125, "USD" },
                    { "M-66124", 0m, false, 30m, 105, "USD" },
                    { "M-77124", 0m, false, 30m, 105, "USD" },
                    { "SH-77124", 0m, false, 30m, 125, "USD" },
                    { "SH-89514", 0m, false, 30m, 125, "USD" },
                    { "SH-96315", 0m, false, 30m, 125, "USD" },
                    { "SH-99875", 0.5m, true, 30m, 15, "USD" },
                    { "T-44183", 0m, false, 30m, 105, "USD" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductArticleNumber",
                table: "ProductReviews",
                column: "ProductArticleNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_CollectionId",
                table: "ProductTags",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRows");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Showcases");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
