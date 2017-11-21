using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FCRM.Web.Requests
{
    public class OrderRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string Information { get; set; }
        public IEnumerable<string> Validate()
        {
            if (string.IsNullOrEmpty(Name))
                yield return "Имя не должно быть пустым";
            if (!Regex.IsMatch(Phone, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
                yield return "Неверный номер телефона";
            if (!Regex.IsMatch(Email, @"^[-\w.]+@([A-z0-9][-A-z0-9]+\.)+[A-z]{2,4}$"))
                yield return "Неверный email";
            if (string.IsNullOrEmpty(CompanyName))
                yield return "Неверное название компании";
            if (string.IsNullOrEmpty(Information))
                yield return "Информация о заказе не может быть пустой";
        }
    }
}