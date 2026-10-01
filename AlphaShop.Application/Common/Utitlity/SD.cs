using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace AlphaShop.Application.Common.Utitlity
{
    //static detail
    public static class SD
    {
        public const string BlogLocation = "blog";
        public const string ProductLocation = "product";

        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";
        public const string LockedOut = nameof(LockedOut);


        public const string StatusPending = "در انتظار";
        public const string StatusApproved = "تایید شده";
        public const string StatusInProcess = "درحال آماده سازی";
        public const string StatusShipped = "ارسال شده";
        public const string StatusCancelled = "لغو شده";
        public const string StatusRefunded = "برگشت داده شده";

        public static List<string> Stutuses { get; set; } = new List<string>()
        { 
            StatusApproved, 
            StatusCancelled,
            StatusInProcess,
            StatusPending,
            StatusRefunded
        };

        public const string StatusNotPaied = "پرداخت نشده";
        public const string StatusPaied = "پرداخت شده";


        public const string ZarinPalGateway = "زرین پال";
        public const string ZarinPalMerchantId = "ZarinPal:MerchantId";
        public const string ZarinPalPaymentVerificationUrl = "ZarinPal:PaymentVerificationUrl";
        public const string ZarinPalPaymentGatewayUrl = "ZarinPal:PaymentGatewayUrl";
        public const string ZarinPalPaymentRequestUrl = "ZarinPal:PaymentRequestUrl";


        /// <summary>
        /// تبدیل تاریخ به تاریخ شمسی 
        /// </summary>
        public static string ToPersianDateString(this DateTime date, bool includeTime = false)
        {
            var persianCalendar = new PersianCalendar();
            string persianDate = $"{persianCalendar.GetYear(date):0000}/{persianCalendar.GetMonth(date):00}/{persianCalendar.GetDayOfMonth(date):00}";

            if (includeTime)
                persianDate += $" {date.ToString("HH:mm")}";

            return persianDate;
        }

        /// <summary>
        /// تبدیل تاریخ UTC دیتابیس به تاریخ و زمان محلی ایران و سپس تبدیل به شمسی
        /// </summary>
        public static string ToIranPersianDateString(this DateTime utcDate, bool includeTime = false)
        {
            // اگر تاریخ پاس داده شده از نوع UTC نباشد، یک خطا پرتاب می‌شود یا می‌توان آن را هندل کرد
            if (utcDate.Kind != DateTimeKind.Utc)
            {
                // در صورت نیاز می‌توانید اینجا آن را به UTC تبدیل کنید یا مستقیما متد اول را صدا بزنید
                // utcDate = utcDate.ToUniversalTime();
            }

            // پیدا کردن تایم زون ایران
            // نکته: در ویندوز نام تایم زون "Iran Standard Time" است، در لینوکس/داکر معمولا "Asia/Tehran"
            TimeZoneInfo iranTimeZone;
            try
            {
                iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"); // برای ویندوز
            }
            catch (TimeZoneNotFoundException)
            {
                iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tehran"); // برای لینوکس و داکر
            }

            // تبدیل زمان جهانی به زمان ایران
            DateTime iranLocalTime = TimeZoneInfo.ConvertTimeFromUtc(utcDate, iranTimeZone);

            // حالا زمانِ تبدیل شده را به متد اصلی پاس می‌دهیم
            return iranLocalTime.ToPersianDateString(includeTime);
        }

    }
}
