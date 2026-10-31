🛒 AlphaShop

[Persian (فارسی)](#-فارسی) | [English](#-english)

---

## 🇬🇧 English

A comprehensive, standard, and scalable E-Commerce platform built with ASP.NET Core MVC and Clean Architecture. This project fulfills all the needs of a modern online store, fully customized and localized for Iranian users.

🚀 Key Features

Full Localization: Implementation of a Persian error alert system for Identity (PersianIdentityErrorDescriber), Persian date picker (persian-datepicker), and standard Persian fonts (Vazirmatn and Peyda).

Online Payment Integration: Fully integrated with the ZarinPal payment gateway for secure transaction processing (ZarinPalResponseDto and Payment services).

Dedicated Admin Panel: A powerful and completely separated admin dashboard using ASP.NET Core Areas to manage categories, products, image galleries, users, and orders.

Order & Cart Management: Complete implementation of shopping cart logic (ShoppingCart), order detail registration (OrderDetail), and invoice management (OrderHeader).

Identity & Security: Secure user registration, authentication, and authorization using ASP.NET Core Identity.

🏗️ Architecture & Design Patterns

The project strictly adheres to Clean Architecture (N-Tier) principles to ensure separation of concerns (Loose Coupling), high readability, and maintainability. It is divided into four main layers:

Domain Layer (AlphaShop.Domain): The beating heart of the project, containing the core database entities (Product, Category, Order, ApplicationUser, Gallery).

Application Layer (AlphaShop.Application): Contains business logic, DTOs, Service Interfaces, and application methods (IRepository, IUnitOfWork).

Infrastructure Layer (AlphaShop.Infrastructure): Handles database communication and external services, including ApplicationDbContext, Repository implementations, Migrations, and payment services.

Presentation Layer (AlphaShop.Web): The entry point of the application containing Controllers, Views, configurations, and static files.

Implemented Patterns

Repository Pattern: Abstracts data access logic for cleaner code.

Unit of Work Pattern: Manages database transactions efficiently across multiple repositories for unified data handling.

Data Transfer Objects (DTOs): Extensive use in the Application layer to prevent database models from leaking to the Presentation layer.

💻 Tech Stack

Backend: C#, ASP.NET Core MVC

Database: SQL Server, Entity Framework Core (Code-First Migrations)

Frontend: HTML5, CSS3, JavaScript, Bootstrap, jQuery

UI Tools: Simple DataTables, SweetAlert (for custom notifications)

---

## 🇮🇷 فارسی


یک فروشگاه اینترنتی جامع، استاندارد و توسعه‌یافته با استفاده از فریم‌ورک ASP.NET Core MVC و Clean Architecture (معماری تمیز). این پروژه تمامی نیازهای یک سیستم تجارت الکترونیک (E-Commerce) مدرن و بومی‌سازی‌شده برای کاربران ایرانی را برآورده می‌کند.

🚀 ویژگی‌های کلیدی

بومی‌سازی کامل برای کاربران ایرانی: پیاده‌سازی سیستم هشدارهای خطای فارسی برای Identity (PersianIdentityErrorDescriber)، استفاده از تقویم و انتخاب‌گر تاریخ شمسی (persian-datepicker) و بهره‌گیری از فونت‌های استاندارد فارسی (وزیرمتن و پیدا).

اتصال به درگاه پرداخت آنلاین: پیاده‌سازی کامل سرویس پرداخت و اتصال به درگاه زرین‌پال جهت پردازش امن تراکنش‌ها (ZarinPalResponseDto و سرویس‌های Payment).

پنل مدیریت اختصاصی و قدرتمند: استفاده از قابلیت Areas در ASP.NET Core برای جداسازی کامل پنل ادمین از بخش کاربری جهت مدیریت دسته‌بندی‌ها، محصولات، گالری تصاویر، کاربران و سفارشات.

مدیریت سبد خرید و سفارشات: پیاده‌سازی کامل منطق سبد خرید (ShoppingCart)، ثبت جزئیات سفارش (OrderDetail) و مدیریت فاکتورها (OrderHeader).

هویت و امنیت: ثبت‌نام، احراز هویت و سطح‌بندی دسترسی کاربران با استفاده از ASP.NET Core Identity.

🏗️ معماری و الگوهای طراحی

این پروژه به صورت کاملاً استاندارد از اصول معماری کلین (Clean Architecture) پیروی می‌کند تا باعث کاهش وابستگی‌ها (Loose Coupling) و افزایش خوانایی و قابلیت نگهداری کد شود. پروژه به چهار لایه مجزا تقسیم شده است:

لایه Domain (AlphaShop.Domain): قلب تپنده پروژه که شامل موجودیت‌های پایه دیتابیس (Entities) مانند محصول، دسته‌بندی، سفارش، کاربر و گالری است.

لایه Application (AlphaShop.Application): شامل منطق تجاری برنامه (Business Logic)، DTOها، اینترفیس‌ها (IRepository و IUnitOfWork) و متدهای کاربردی.

لایه Infrastructure (AlphaShop.Infrastructure): مسئول ارتباط با دیتابیس و سرویس‌های خارجی. شامل ApplicationDbContext، پیاده‌سازی ریپازیتوری‌ها، فایل‌های Migration دیتابیس و سرویس‌های پرداخت.

لایه Presentation (AlphaShop.Web): نقطه ورود برنامه شامل کنترلرها، Viewها، تنظیمات و فایل‌های استاتیک.

الگوهای استفاده شده (Patterns)

الگوی Repository: برای انتزاع منطق دسترسی به داده‌ها از هسته برنامه.

الگوی Unit of Work: برای مدیریت یکپارچه تراکنش‌های دیتابیس در چندین ریپازیتوری.

انتقال داده ایمن (DTOs): استفاده گسترده از DTOها در لایه Application برای جلوگیری از نشت اطلاعات مدل‌های دیتابیس به لایه نمایش.

💻 تکنولوژی‌های استفاده شده

بک‌اند: C#, ASP.NET Core MVC

دیتابیس: SQL Server, Entity Framework Core (رویکرد Code-First)

فرانت‌اند: HTML5, CSS3, JavaScript, Bootstrap, jQuery

ابزارهای رابط کاربری: Simple DataTables, SweetAlert (برای نوتیفیکیشن‌های اختصاصی)
