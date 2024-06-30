using DemoQA_Selenium.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoQA.SpecFlow.Service.DataObjects
{
    public class StudentFormattedDTO
    {
        public string FullName { get; set; }
        public string StateAndCity { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Subjects { get; set; }
        public string Hobbies { get; set; }
        public string Picture { get; set; }
        public string CurrentAddress { get; set; }

        public static StudentFormattedDTO FormatStudentDTO(StudentDTO student)
        {
            return new StudentFormattedDTO
            {
                FullName = $"{student.FirstName} {student.LastName}",
                Email = student.Email,
                Gender = student.Gender,
                MobileNumber = student.MobileNumber,
                DateOfBirth = student.DateOfBirth,
                Subjects = student.Subjects,
                Hobbies = student.Hobbies,
                Picture = student.Picture,
                CurrentAddress = student.CurrentAddress,
                StateAndCity = $"{student.State} {student.City}"
            };
        }
    }
}
