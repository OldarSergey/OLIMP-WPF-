using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace OLIMP.Entities
{
    public class Client
    {
        public Guid Id { get; set; }

        public string FistName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PassportAddress { get; set; }
        public string PassportNumber { get; set; }
        public string PassportSeria { get; set; }

        public List<Contract> Contracts { get; set; }

        public Client()
        {
            
        }

        public Client(string fistName, string lastName, string middleName, string passportAddress, string passportNumber, string passportSeria)
        {
            Id = Guid.NewGuid();
            FistName = fistName;
            LastName = lastName;
            MiddleName = middleName;
            PassportAddress = passportAddress;
            PassportNumber = passportNumber;
            PassportSeria = passportSeria;
        }
        public override string ToString()
        {
            return $"{FistName} {LastName} {MiddleName}";
        }
    }
}
