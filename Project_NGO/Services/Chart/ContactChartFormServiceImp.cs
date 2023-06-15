using Microsoft.EntityFrameworkCore;
using Project_NGO.Data;
using Project_NGO.Models;
using Project_NGO.Models.Chart;
using Project_NGO.Repositories.Chart;

namespace Project_NGO.Services.Chart
{
    public class ContactChartFormServiceImp : IContactChartForm
    {
        private readonly DatabaseContext dbcontext;
        public ContactChartFormServiceImp(DatabaseContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }


        public List<ContactFormChartData> GetChartDataForWeek()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = now.AddDays(-7); // ngay bawst dafu laf ngay hien tai tru di 7 ngay
            DateTime endDate = now; // ngay kiet thuc se la hien taji

            // lay danh sach Contact_Form trong khoang thoi gian tu startDate den endDate
            List<Contact_Form> contactForms = GetContactFormsByDateRange(startDate, endDate);

            // tinh toan so luong tin nhan chua doc va da doc trong tuan 
            int countUnread = contactForms.Count(c => !c.IsRead);
            int countRead = contactForms.Count(c => c.IsRead);

            // tao danh sach ContactFormChartData
            var chartData = new List<ContactFormChartData>
            {
                new ContactFormChartData { Label = "Unread", Value = countUnread },
                new ContactFormChartData { Label = "Read", Value = countRead }
            };

            return chartData;
        }



        public List<ContactFormChartData> GetChartDataForDay()
        {
            DateTime now = DateTime.Now;
            DateTime startDate = now.Date; //ngay bat dau la ngay hien tai
            DateTime endDate = now.AddDays(1).Date; // ngay ket thuc la ngay tiep tho

            // lay danh sach Contact_Form ttrong t.gian tu startDate đến endDate
            List<Contact_Form> contactForms = GetContactFormsByDateRange(startDate, endDate);

            // tinh toan so luong tin nhan chua doc va da doc trong tuan ngay
            int countUnread = contactForms.Count(c => !c.IsRead);
            int countRead = contactForms.Count(c => c.IsRead);

            // tao danh sach ContactFormChartData
            var chartData = new List<ContactFormChartData>
    {
        new ContactFormChartData { Label = "Unread", Value = countUnread },
        new ContactFormChartData { Label = "Read", Value = countRead }
    };

            return chartData;
        }
        private List<Contact_Form> GetContactFormsByDateRange(DateTime startDate, DateTime endDate)
        {

            List<Contact_Form> contactForms = dbcontext.Contact_Forms
         .Where(c => c.CreatedAt >= startDate && c.CreatedAt <= endDate)
         .ToList();

            return contactForms;
        }

    }
}
